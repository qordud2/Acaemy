using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InOut : MonoBehaviour
{
    public LayerMask lmask;
    public float speed = 10.0f;
    Vector3 dir;
    float dist;

    Vector3 RightDown = new Vector3(0.5f, -0.5f, 0);
    Vector3 RightUp = new Vector3(0.5f, 0.5f, 0);
    Vector3 LeftUp = new Vector3(-0.5f, 0.5f, 0);
    Vector3 LeftDown = new Vector3(-0.5f, -0.5f, 0);

    void Start()
    {
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 1000.0f, lmask))
            {
                dir = hit.point - transform.position;
                dir.Normalize();    // 거리가 1인 방향벡터로 만든다
                dist = Vector3.Distance(hit.point, transform.position);
            }
        }
        Moving();
    }

    void Moving()
    {
        Vector3 Left = Vector3.Cross(RightDown, RightUp);   // 왼쪽 방향
        //Left.Normalize();
        Vector3 Down = Vector3.Cross(RightUp, LeftUp);      // 아래 방향
        //Down.Normalize();
        Vector3 Right = Vector3.Cross(LeftUp, LeftDown);    // 오른쪽 방향
        //Right.Normalize();
        Vector3 Up = Vector3.Cross(LeftDown, RightDown);    // 위에 방향
        //Up.Normalize();

        Debug.Log("Left : " + Left);
        Debug.Log("Down : " + Down);
        Debug.Log("Right : " + Right);
        Debug.Log("Up : " + Up);


        //{
        //    float delta = Time.deltaTime;
        //    if (dist - delta < Mathf.Epsilon)    // 해당 위치를 넘을까봐 적음
        //    {
        //        delta = dist;
        //    }
        //    dist -= delta;

        //    Vector3 pos = transform.position;
        //    pos += dir * delta;
        //    transform.position = pos;
        //    Debug.Log("IN");
        //} 
        //else
        //    Debug.Log("OUT");
    }
}

