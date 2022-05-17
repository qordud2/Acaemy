using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public GameObject Wall;         // 벽
    public Rigidbody rigid;         // rigidbody
    public float speed = 8.0f;      // 공 스피드
    bool bound = false;             // 부딪혔는지
    public float power = 10.0f;     // Addforce 파워
    SphereCollider Sphere;          // Spher 콜라이더
    public Vector3 radius = Vector3.zero;       // 공의 반지름
    public LayerMask lmask;


    public enum STATE
    {
        NORMAL, FIRE
    }
    public STATE myState = STATE.NORMAL;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.Sleep();
        Sphere = GetComponent<SphereCollider>();
        radius = Sphere.bounds.size;
        Sphere.enabled = false;
        Destroy(gameObject, 5.0f);     
    }

    void Update()
    {
        ChangeState(STATE.FIRE);
        StateProcess();        
    }

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch (myState)
        {
            case STATE.NORMAL:
                break;
            case STATE.FIRE:
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.NORMAL:
                break;
            case STATE.FIRE:
                Vector3 Perv = transform.position;
                Vector3 Next = transform.position + transform.forward * Time.deltaTime * speed;
                
                Ray ray = new Ray();
                ray.origin = Perv;
                ray.direction = Vector3.Normalize(Next - Perv);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Vector3.Distance(Perv, Next - radius), lmask))
                {
                    Vector3 dir = Vector3.Normalize(hit.point - transform.position);
                    Sphere.enabled = true;
                    bound = true;
                    rigid.WakeUp();
                    Vector3 addforce = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-0.5f, -2.0f), Random.Range(-0.5f, -2.0f));
                    rigid.AddForce(-dir * power, ForceMode.Impulse);                  
                    StartCoroutine(Gravity());
                    //break;
                }
                if(!bound)
                {
                    transform.position = Next;
                }
                
                break;
        }
    }

    IEnumerator Gravity()
    {       
        rigid.useGravity = true;
        Debug.Log("cc");
        yield return new WaitForSeconds(0.1f);
    }

}
    


