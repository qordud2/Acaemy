using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public int JumpPower;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Jump") > 0.0f)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            // ForceMode.Impulse - rigidBody 컴포넌트 요소중에 Mass(질량)의 영향을 받으며 짥은 순간에 힘을 가하는 모드
            //Force rigidbody에 지속적인 힘 추가, 질량 사용.
            //Acceleration rigidbody에 지속적인 가속 추가, 질량 무시.
            //Impulse rigidbody에 순간적으로 충격 추가, 질량 사용.
            //VelocityChange rigidbody에 순간속도 변경, 질량 무시. 
        }

        float speed = 3.0f;
        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");

        Vector3 dir = Vector3.forward * forward + Vector3.right * right;

        //transform.Translate(dir * speed * Time.deltaTime);    // 물리적인 이동은 강제로 Transform하면 문제가 생기니 물리(Rigidbody)를 이용해서 이동시킨다
        Vector3 pos = transform.position;
        pos += dir * speed * Time.deltaTime;
        GetComponent<Rigidbody>().MovePosition(pos);
    }
}
