using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentTest : MonoBehaviour, BattleSystem
{
    public enum STATE
    {
        NORMAL, BATTLE
    }
    public STATE myState = STATE.NORMAL;
    public Animator myAnim;
    public NavMeshAgent myNavAgent;
    public LayerMask ClickMask;

    public float AttackDelay = 1.0f;
    float OrgAttackDelay = 0.0f;

    public AnimEvent myAnimEvent = null;
    public Transform myTarget = null;

    public bool IsAir { get; private set; }
    bool JumpStart = false;

    public GameObject Prefab;
    GameObject Target;

    void Start()
    {
        myNavAgent = GetComponent<NavMeshAgent>();
        myAnim = GetComponentInChildren<Animator>();
        myAnimEvent = GetComponentInChildren<AnimEvent>();
        myAnimEvent.Attack += OnAttack;
        myAnimEvent.JumpStart += () => { JumpStart = true; };   // 간단한거는 람다식으로 구현
        myAnimEvent.JumpEnd += () => { JumpStart = false; };
        OrgAttackDelay = AttackDelay;
        IsAir = false;

        // Create MiniMap
        GameObject obj = Instantiate(Resources.Load("PlayerMiniMapIcon")) as GameObject;
        obj.GetComponent<MinimapIcon>().FollowTransform(transform);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999.0f, ClickMask))
            {
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Monster"))
                {
                    myTarget = hit.transform;
                    ChangeState(STATE.BATTLE);
                }
                else
                {
                    ChangeState(STATE.NORMAL);
                    //myNavAgent.SetDestination(hit.point);
                    StartCoroutine(Targetting(hit.point));                    
                    //Target = Instantiate(Resources.Load("Target")) as GameObject;
                }               
            } 
        }

        if (myNavAgent.isOnOffMeshLink && IsAir == false)  // 현재 NavMesh가 OffMeshLink에 진입을 했다면
        {
            IsAir = true;
            myNavAgent.isStopped = true;    // 해당 Agent가 멈추게 된다
            StartCoroutine(Jumping());
        }
        myAnim.SetBool("IsAir", IsAir);
        StateProcess();
    }

    IEnumerator Targetting(Vector3 pos)
    {
        myNavAgent.SetDestination(pos);
        yield return null;
        yield return new WaitForEndOfFrame();
        if (Target != null) Destroy(Target);
        Target = Instantiate(Prefab);
        Target.transform.position = pos;

        while (myNavAgent.remainingDistance > 0.1f) yield return null;

        if (Target != null)  Destroy(Target);

    }

    void OnEnterTrigger(Collider other)
    {
        if(other.gameObject.name == "Target")
        {
            //Destroy(prefab, 3.0f);
        }
    }


    IEnumerator Jumping()
    {
        while (!JumpStart) yield return null;
        // => JumpStart가 true가 될때까지 계속 돈다

        Vector3 startpos = transform.position;
        Vector3 endpos = myNavAgent.currentOffMeshLinkData.endPos;  // Link Mesh의 데이터를 받아온다

        float jumptime = 0f;
        float jumpHeight = 0f;

        while(jumptime < 1.0f)
        {
            jumptime += Time.deltaTime;
            // 1초동안 점프를 한다
            Vector3 pos = Vector3.Lerp(startpos, endpos, jumptime);
            // 현재 위치 계속 갱신

            jumpHeight = Mathf.Sin(jumptime * Mathf.PI) * 3.0f;
            if (jumpHeight < 0)
                jumpHeight = 0;
            Debug.Log("jumpHeight : " + jumpHeight);
            pos.y += jumpHeight;
            transform.position = pos;
            yield return null;
        }

        // 1초가 흐른 뒤
        //transform.position = endpos;
        IsAir = false;
        //JumpStart = false;
        myNavAgent.CompleteOffMeshLink();
        while (JumpStart) yield return null;
        // => JumpStart가 false가 될때까지 계속 돈다

        myNavAgent.isStopped = false;

        //while (myNavAgent.isOnOffMeshLink)
        //{
        //    yield return null;
        //}   // => isOnOffMeshLink가 false가 될때까지 계속 돈다

        //IsAir = false;
        //myAnim.SetBool("IsAir", IsAir);
        yield return null;
    }

    public void OnDamage() // override가 생략되어있다
    {
        Debug.Log("POnDamage");
        myAnim.SetTrigger("Damage");
    }

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case STATE.NORMAL:
                myNavAgent.stoppingDistance = 0.5f;
                break;
            case STATE.BATTLE:
                myNavAgent.stoppingDistance = 1.5f;
                break;
        }

    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.NORMAL:
                myAnim.SetFloat("Speed", myNavAgent.velocity.magnitude / myNavAgent.speed);
                break;
            case STATE.BATTLE:
                //transform.LookAt(myTarget);
                Vector3 dir = myTarget.position - transform.position;
                dir.y = 0;  // 평면상으로만 이동하려고 y = 0 했다
                dir.Normalize();
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.smoothDeltaTime * 3.0f);

                myAnim.SetFloat("Speed", myNavAgent.velocity.magnitude / myNavAgent.speed);
                myNavAgent.SetDestination(myTarget.position);
                if (AttackDelay > Mathf.Epsilon)
                {
                    AttackDelay -= Time.deltaTime;
                }
                else
                {
                    myAnim.SetTrigger("Attack");
                    AttackDelay = OrgAttackDelay;
                }
                break;
        }
    }

    void OnAttack()
    {
        myTarget.GetComponent<BattleSystem>()?.OnDamage();
    }
}
