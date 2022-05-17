using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assignment : MonoBehaviour
{
    public LayerMask lmask;
    Vector3 dir = new Vector3(0, 0, 0);
    Vector3 StartPoint;
    Vector3 dest;
    float dist = 0;

    public float RotateSpeed = 0.0f;
    float rot = 0;
    float In = 0;
    float Out = 0;
    Vector3 Sucess;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;         // 충돌 지점 확인
            if (Physics.Raycast(ray, out hit, 1000.0f, lmask))   // 충돌 여부확인, lmask에 포함된 레이어만 피킹한다
            {
                StartPoint = transform.position;
                dest = hit.point;
                dist = Vector3.Distance(dest, StartPoint);
                dir = dest - StartPoint;
                dir.Normalize();

                rot = Vector3.Dot(dir, transform.forward);  // 내적값 (-1 ~ 1)값이 나옴
                rot = Mathf.Acos(rot);  // Acos: y축 값을 통해서 각도를 아는거고, Cos: x축 기준으로 각도를 구하는 것
                                        // 원래는 Vector3.Dot(A, B) = Vector3.Magnitude(A) * Vector3.Magnitude(B) * Mathf.Cos(angle);
                                        // Magnitude는 벡터의 길이, 여기서는 크기는 1로 고정되니 제외했다
                                        // angle : pi = x : 180
                rot = (rot * 180.0f) / Mathf.PI;
                Vector3 right = Vector3.Cross(Vector3.up, transform.forward);   // 위와 앞을 외적하면 오른쪽 벡터가 나온다
                                                                                // 외적은 순서가 중요 (왼쪽 손가락 법칙 생각하기, 첫번째에서 두번째를 감싸는 방향의 엄지)
                right.Normalize();
                if (Vector3.Dot(right, dir) < 0.0f)
                {
                    rot *= -1.0f;
                }
                transform.Rotate(Vector3.up * rot);
            }
        }
        Moving();
        Rotating();
        Inout();
    }

    void Moving()
    {
        if (dist > Mathf.Epsilon) // Epsilon - float을 빼거나 계산할때 보통 뒤에 소수점 숫자들이 남는 경우가 있기 때문에 정확하게 하기위해 Epsilon을 넣는다, 근사값에 가장 가까운 값
        {
            float delta = Time.deltaTime;
            if (dist - delta < Mathf.Epsilon)    // 해당 위치를 넘을까봐 적음
            {
                delta = dist;
                dist = 0.0f;
            }
            else
            {
                dist -= delta;
            }
            Vector3 pos = transform.position;
            pos += dir * delta;
            transform.position = pos;
        }
    }

    void Rotating()
    {
        if (rot > Mathf.Epsilon)
        {
            float delta = RotateSpeed * Time.deltaTime;
            if (rot - delta <= Mathf.Epsilon)
            {
                delta = rot;
            }
            rot -= delta;
            transform.Rotate(Vector3.up * delta);
        }
        else if (rot < -Mathf.Epsilon)
        {
            float delta = RotateSpeed * Time.deltaTime;
            if (rot + delta >= Mathf.Epsilon)
            {
                delta = -rot;
            }
            rot += delta;
            transform.Rotate(Vector3.up * -delta);
        }
    }
    void Inout()
    {
        // forward, right 내적을 벗어나면 out 아니면 in
        In = Vector3.Dot(transform.forward, transform.right);
        In = Mathf.Cos(In);

        Out = Vector3.Dot(dir, transform.forward);
        Out = Mathf.Cos(Out);

        if (Out <= In)
        {
            Debug.Log("In");
        }
        else
            Debug.Log("Out");
        
    }
}
