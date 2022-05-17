using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, BattleSystem
{
    

    public enum STATE
    {
        NORMAL,
        ROAMING,
        BATTLE,
        ESCAPE
    }

    STATE myState = STATE.NORMAL;

    public Animator myAnim;
    public NavMeshAgent myNavAgent; // 네

    float Playtime = 0f;

    Vector3 OrgPos = Vector3.zero;

    public RangeSystem myRangeSys = null;
    public float AttackDelay = 1.0f;
    float OrgAttackDelay = 0.0f;

    public AnimEvent myAnimEvent = null;

    void Awake()
    {
        myNavAgent = GetComponent<NavMeshAgent>();      
        myAnim = GetComponentInChildren<Animator>();
        myRangeSys = GetComponentInChildren<RangeSystem>();
        myRangeSys.battle = OnBattle;   // 함수포인터임
        //myRangeSys.battle += Test1;
        //myRangeSys.battle += Test2;
        //myRangeSys.battle -= Test2;   
        // 딜리게이트에 더하기 빼기가 가능하다, 대입을 하는경우 앞에꺼가 다 지워지고 대입할걸로 대체된다
        // 더하기(동시호출), 빼기(동시호출 하는거에서 빼버린다)

        // <람다식>
        //myRangeSys.battle = /*void or float 그리고 파라미터*/() => { ChangeState(STATE.BATTLE); };
        //myRangeSys.battle = () => { ChangeState(STATE.BATTLE); };
        // 장점은 따로 함수로 안만들어도 넣어버릴수있고 
        // 단점은 재활용이 안된다, 특정 장소에서 써야겠다 하는것만 쓰면댐
        // 안써도 되긴하는데 익숙해지는게 좋을듯

        //myRangeSys.DelAction += OnBattle; 
        // 이벤트는 DelAction?.Invoke(); 외부 클래스에서 실행 불가능
        // battle?.Invoke(); 딜리게이트는 가능 
        // 이벤트는 += -=만 가능하다

        myAnimEvent = GetComponentInChildren<AnimEvent>();
        myAnimEvent.Attack += OnAttackTarget;

        OrgPos = transform.position;
        OrgAttackDelay = AttackDelay;

        // Create MiniMap
        GameObject obj = Instantiate(Resources.Load("EnemyMiniMapIcon")) as GameObject;
        obj.GetComponent<MinimapIcon>().FollowTransform(transform);
    }

    void Update()
    {
        StateProcess();
    }

    //void Test1()
    //{
    //    Debug.Log("Test1");
    //}

    //void Test2()
    //{
    //    Debug.Log("Test2");
    //}

    void OnBattle()
    {
        ChangeState(STATE.BATTLE);
    }

    public void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case STATE.NORMAL:
                Playtime = 0f;
                myAnim.SetFloat("Speed", 0f);
                myNavAgent.stoppingDistance = 0.5f;
                break;
            case STATE.ROAMING:
                Vector3 dir = Vector3.zero;
                dir.x = Random.Range(-1.0f, 1.0f);
                dir.z = Random.Range(-1.0f, 1.0f);
                dir.Normalize();
                myNavAgent.SetDestination(OrgPos + dir * Random.Range(1.0f, 3.0f));
                break;
            case STATE.BATTLE:
                myNavAgent.stoppingDistance = 1.5f;
                break;
            case STATE.ESCAPE:
                break;
        }
    }

    void StateProcess()
    {
        switch(myState)
        {
            case STATE.NORMAL:
                Playtime += Time.deltaTime;
                if(Playtime > 2.0f)
                {
                    ChangeState(STATE.ROAMING);
                }
                break;
            case STATE.ROAMING:
                myAnim.SetFloat("Speed", myNavAgent.velocity.magnitude / myNavAgent.speed);
                // myNavAgent.speed를 나눈이유는 0 ~ 1.0으로 값을 맞추기 위해 나눴다, speed - 3.5이다
                if (myNavAgent.remainingDistance < myNavAgent.stoppingDistance)
                {
                    //myNavAgent.SetDestination()
                    ChangeState(STATE.NORMAL);
                }
                break;
            case STATE.BATTLE:
                //transform.LookAt(myRangeSys.Target.position);
                Vector3 dir = myRangeSys.Target.position - transform.position;
                dir.y = 0;  // 평면상으로만 이동하려고 y = 0 했다
                dir.Normalize();
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.smoothDeltaTime * 3.0f);
                myAnim.SetFloat("Speed", myNavAgent.velocity.magnitude / myNavAgent.speed);
                myNavAgent.SetDestination(myRangeSys.Target.position);
 
                if(AttackDelay > Mathf.Epsilon)
                {
                    AttackDelay -= Time.deltaTime;
                }
                else
                {
                    OnAttack();
                }
                break;
            case STATE.ESCAPE:
                break;
        }
    }

    void OnAttack()
    {
        if (myNavAgent.remainingDistance <= myNavAgent.stoppingDistance)
        {
            myAnim.SetTrigger("Attack");
            AttackDelay = OrgAttackDelay;
        }    
    }

    void OnAttackTarget()
    {
        //myRangeSys.Target.GetComponent<NavAgentTest>().OnDamage();
        myRangeSys.Target.GetComponent<BattleSystem>()?.OnDamage();
    }

    public void OnDamage()
    {
        Debug.Log("MOnDamage");
        myAnim.SetTrigger("Damage");
    }
}
