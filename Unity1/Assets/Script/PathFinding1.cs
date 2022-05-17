using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinding1 : MonoBehaviour
{
    public LayerMask ClickMask;
    public Animator myAnim;
    NavMeshPath path;
    Coroutine move = null;
    Coroutine rotate = null;

    float rot = 0.0f;
    Vector3 TargetRot = Vector3.zero;
    public float SmoothRotSpeed = 20.0f;

    void Start()
    {
        path = new NavMeshPath();
        myAnim = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 999.0f, ClickMask))
            {
                NavMesh.CalculatePath(transform.position, hit.point, NavMesh.AllAreas, path);
                {
                    if (move != null) StopCoroutine(move);
                    move = StartCoroutine(Moving(path));
                    if (rotate != null) StopCoroutine(rotate);
                    rotate = StartCoroutine(Rotating(path));
                }
            }
        }
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(TargetRot), Time.smoothDeltaTime * SmoothRotSpeed);
    }

    IEnumerator Moving(NavMeshPath path)
    {
        // 갈수 있는길
        //if (path.status == NavMeshPathStatus.PathComplete) ;
        // 도착지점까지 가는데 문제가 있다
        //if (path.status == NavMeshPathStatus.PathPartial) ;
        // 길이 없다
        //if (path.status == NavMeshPathStatus.PathInvalid) ;
        float movespeed = 0.0f;
        float speed = 3.0f;
        int curpos = 1;
        Vector3 target = path.corners[curpos];      // world상의 좌표임
        Vector3 dir = target - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        
        while(curpos < path.corners.Length)
        {
            myAnim.SetFloat("Speed", movespeed);
            movespeed = Mathf.Clamp(movespeed + Time.deltaTime * speed, 0.0f, 3.0f);
            Vector3 pos = transform.position;
            float delta = Time.deltaTime * movespeed;
            if(dist - delta <= Mathf.Epsilon)
            {
                delta = dist;   // 이동할 거리는 남은 거리로 대처
                ++curpos;
                if(curpos == path.corners.Length)
                {
                    transform.Translate(dir * delta, Space.World);
                    continue;   // 바로 while문으로 올라가고 매개변수 조건이 달라지게되어 Courtine이 종료된다
                }
                dir = path.corners[curpos] - path.corners[curpos - 1];
                dist = dir.magnitude;
                dir.Normalize();
            }
            dist -= delta;
            transform.Translate(dir * delta, Space.World);

            yield return null;
        }

        while (movespeed > 0.0f)
        {
            movespeed = Mathf.Clamp(movespeed - Time.deltaTime * speed, 0.0f, 3.0f);
            myAnim.SetFloat("Speed", movespeed);
            yield return null;
        }

    }

    IEnumerator Rotating(NavMeshPath path)
    {
        float rotspeed = 100.0f;
        int curpos = 1;
        Vector3 target = path.corners[curpos];      // world상의 좌표임
        Vector3 dir = target - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        rot = Vector3.Dot(dir, transform.forward);
        rot = Mathf.Acos(rot);
        rot = rot * 180.0f / Mathf.PI;

        if (Vector3.Dot(Vector3.right, dir) < 0.0f)
        {
            rot *= -1.0f;
        }
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(TargetRot), Time.smoothDeltaTime * SmoothRotSpeed);
        while (curpos < path.corners.Length)
        {
            if(rot > Mathf.Epsilon || rot < Mathf.Epsilon)
            {
                if(rot > Mathf.Epsilon)
                {
                    float delta = Time.deltaTime * rotspeed;
                    if(rot - delta <= Mathf.Epsilon)
                    {
                        delta = rot;
                        TargetRot.y += delta;
                        ++curpos;
                        if (curpos == path.corners.Length)
                        {
                            transform.Rotate(Vector3.up * delta);
                            continue;   // 바로 while문으로 올라가고 매개변수 조건이 달라지게되어 Courtine이 종료된다
                        }

                    }
                    TargetRot.y += delta;
                    rot -= delta;
                    //transform.Rotate(Vector3.up * delta);
                }
                else if(rot < -Mathf.Epsilon)
                {
                    float delta = Time.deltaTime * rotspeed;
                    if (rot + delta >= Mathf.Epsilon)
                    {
                        delta = -rot;
                        TargetRot.y -= delta;
                        ++curpos;
                        if (curpos == path.corners.Length)
                        {
                            transform.Rotate(Vector3.up * -delta);
                            continue;   // 바로 while문으로 올라가고 매개변수 조건이 달라지게되어 Courtine이 종료된다
                        }
                    }
                    rot += delta;
                    TargetRot.y -= delta;
                    //transform.Rotate(Vector3.up * -delta);
                }
                Debug.Log("curpos : " + curpos);
            }
            yield return null;
        }
    }
}
