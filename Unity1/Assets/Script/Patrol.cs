using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Animator myAnim;
    public NavMeshAgent myNavAgent;
    Vector3 TargetPoint = Vector3.zero;     // 이동지점

    public float timeMoveMin = 0.25f;       // 이동시간 Min
    public float timeMoveMax = 3.25f;       // 이동시간 Max
    float timeMove;                         // 패트롤 이동 시간

    public float MovePoint = 10.5f;         // 랜덤 이동거리

    Vector3 RadiusPoint = Vector3.zero;     // 원의 표면상의 임의의 점
    Vector3 StartPoint = Vector3.zero;      // 시작점  
    Vector3 EndPoint = Vector3.zero;        // 원의 최고 끝점

    public float RadiusLength = 10.0f;      // 원의 길이를 조절하는 변수
    public float EXdist = 0f;               // 원의 최고 길이

    void Awake()
    {
        myNavAgent = GetComponent<NavMeshAgent>();
        myAnim = GetComponentInChildren<Animator>();
        timeMove = 0.0f;
        RadiusPoint = Random.onUnitSphere;  // 반경1을 갖는 원의 표면상의 임의의 지점을 반환한다
        RadiusPoint.y = 0.0f;
        StartPoint = transform.position; 
    }

    void FixedUpdate()
    {
        timeMove += Time.deltaTime;

        if (timeMove > 5.0f) // 원형으로 구하기     
        {
            EndPoint.x = RadiusPoint.x * RadiusLength;      
            EndPoint.z = RadiusPoint.z * RadiusLength;
            EXdist = Vector3.Distance(StartPoint, EndPoint);

            timeMove = Random.Range(timeMoveMin, timeMoveMax);  // 시간값을 랜덤으로 부여

            TargetPoint.x = Random.Range(0.0f, MovePoint);
            TargetPoint.z = Random.Range(0.0f, MovePoint);      // 타겟 포인트를 랜덤으로 부여

            float dist = Vector3.Distance(StartPoint, TargetPoint); 
            if(dist > EXdist)
            {
                TargetPoint = StartPoint;
            }
            
            myNavAgent.SetDestination(TargetPoint);
        }
        

        myAnim.SetFloat("Speed", myNavAgent.velocity.magnitude);    
    }
    
}
