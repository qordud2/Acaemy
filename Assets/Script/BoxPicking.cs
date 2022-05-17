
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPicking : MonoBehaviour
{
    public LayerMask lmask;

    // 방향과 거리로 이동
    public float speed = 1.0f;
    Vector3 dir;
    //float dist = 0;
    // 회전
    public float RotateSpeed = 360.0f;
    float rot = 0.0f;

    // 보간
    public bool bLerp = true;
    public bool SLerp = true;
    Vector3 targetpos = Vector3.zero;
    Vector3 TargetRot = Vector3.zero;

    public float SmoothSpeed = 0.0f;
    public float SmoothRotSpeed = 10.0f;

    void Start()
    {
        targetpos = transform.position;
        TargetRot = transform.rotation.eulerAngles;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;         // 충돌 지점 확인
            if (Physics.Raycast(ray, out hit, 1000.0f, lmask))   // 충돌 여부확인, lmask에 포함된 레이어만 피킹한다
            {   // out은 &랑 비슷하다고 보면댐  &(ref)는 받는쪽, out은 보내는쪽
                StopAllCoroutines();
                dir = hit.point - transform.position;
                dir.Normalize();    // 거리가 1인 방향벡터로 만든다
                // dist = Vector3.Distance(hit.point, transform.position);
                StartCoroutine(Moving(hit.point, Vector3.Distance(hit.point, transform.position)));

                // 회전
                rot = Vector3.Dot(dir, transform.forward);  // 내적값 (-1 ~ 1)값이 나옴
                rot = Mathf.Acos(rot);  // Acos: y축 값을 통해서 각도를 아는거고, Cos: x축 기준으로 각도를 구하는 것
                                        // 원래는 Vector3.Dot(A, B) = Vector3.Magnitude(A) * Vector3.Magnitude(B) * Mathf.Cos(angle);
                                        // Magnitude는 벡터의 길이, 여기서는 크기는 1로 고정되니 제외했다
                                        // angle : pi = x : 180
                rot = rot * 180.0f / Mathf.PI;

                Vector3 right = Vector3.Cross(Vector3.up, transform.forward);   // 위와 앞을 외적하면 오른쪽 벡터가 나온다
                // 외적은 순서가 중요 (왼쪽 손가락 법칙 생각하기, 첫번째에서 두번째를 감싸는 방향의 엄지)
                right.Normalize();
                if (Vector3.Dot(right, dir) < 0.0f)
                {
                    rot *= -1.0f;
                }
                StartCoroutine(Rotating(rot));
            }
        }
        if(bLerp) transform.position = Vector3.Lerp(transform.position, targetpos, 1.0f);
        if(SLerp) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(TargetRot), Time.smoothDeltaTime * SmoothRotSpeed);
    }
        

    IEnumerator Moving(Vector3 target, float dist)
    {
        targetpos = transform.position;

        while (dist > Mathf.Epsilon) // Epsilon - float을 빼거나 계산할때 보통 뒤에 소수점 숫자들이 남는 경우가 있기 때문에 정확하게 하기위해 Epsilon을 넣는다, 근사값에 가장 가까운 값
        {
            float delta = Time.deltaTime;
            if (dist - delta < Mathf.Epsilon)    // 해당 위치를 넘을까봐 적음
            {
                delta = dist;
            }
            dist -= delta;
            if(bLerp)
            {
                targetpos += dir * delta;
            }
            else
            {
                transform.Translate(dir * delta, Space.World);
            }
            yield return null;
        }
    }

    IEnumerator Rotating(float rot)
    {
        while (rot > Mathf.Epsilon || rot < -Mathf.Epsilon)
        {
            if (rot > Mathf.Epsilon)
            {
                float delta = RotateSpeed * Time.deltaTime;
                if (rot - delta <= Mathf.Epsilon)
                {
                    delta = rot;
                }
                rot -= delta;
                //this.transform.Rotate(Vector3.up * delta);

                if(SLerp)
                {
                    TargetRot.y += delta;
                }
                else
                {
                    transform.Rotate(Vector3.up * delta);
                }
                
            }
            else if (rot < -Mathf.Epsilon)
            {
                float delta = RotateSpeed * Time.deltaTime;
                if (rot + delta >= Mathf.Epsilon)
                {
                    delta = -rot;
                }
                rot += delta;
                //this.transform.Rotate(Vector3.up * -delta);
                
                if (SLerp)
                {
                    TargetRot.y -= delta;
                }
                else
                {
                    transform.Rotate(Vector3.up * -delta);
                }
            }
            yield return null;
        }
    }
}
