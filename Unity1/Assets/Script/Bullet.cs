using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Wall;
    //public Rigidbody rigid;
    public float speed = 8.0f;
    public LayerMask lmask;

    public enum STATE
    {
        NORMAL, FIRE
    }
    public STATE myState = STATE.NORMAL;
    // Start is called before the first frame update
    void Start()
    {
        //rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //transform.LookAt(Wall.transform.position);
            //rigid.velocity = transform.forward * speed;
            ChangeState(STATE.FIRE);
        }
        StateProcess();

        

    }

    void FixedUpdate()
    {


        
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Wall")
    //    {
    //        //Destroy(Wall, 1.0f);
    //        //Destroy(gameObject);
    //        Debug.Log("Destroy");
    //    }
    //}

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
                Vector3 Perv = this.transform.position;
                Vector3 Next = this.transform.position + Vector3.forward * Time.deltaTime * speed;

                Ray ray = new Ray();
                ray.origin = Perv;
                ray.direction = Vector3.Normalize(Next - Perv);
                RaycastHit hit;
                //Vector3.Distance(Perv, Next)
                if (Physics.Raycast(ray, out hit, Vector3.Distance(Perv, Next)))
                {
                    Destroy(gameObject);
                    Destroy(hit.transform.gameObject);
                }
                transform.position = Next;

                break;

        }
    }
}
