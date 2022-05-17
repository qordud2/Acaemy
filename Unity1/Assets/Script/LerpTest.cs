using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    public float myT = 0.0f;
    public Vector3 UpPos = new Vector3(0.0f, 3.0f, 0.0f);
    Vector3 OrgPos;
    float playtime = 0.0f;
    public float MoveTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        OrgPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //0.5f = Mathf.Lerp(0, 1.0f, 0.5f);
        //1.0f = Mathf.Lerp(0, 2.0f, 0.5f);

        //Vector3 a = transform.position;
        //a.x = -3.0f;
        //Vector3 b = a;
        //b.x += 3.0f;

        //transform.position = Vector3.Lerp(a, b, myT);S

        playtime += Time.deltaTime;
        //transform.position = Vector3.Lerp(OrgPos, UpPos, playtime / MoveTime);
        transform.position = Vector3.Lerp(OrgPos, UpPos, playtime / MoveTime);
        // 프레임마다 Time.deltaTime만큼 보간되고 점점 시작지점과 도착지점의 거리가 짧아지고
        // 도착지점으로 도착할수록 점점 짧은 거리가 보간되어서 갈수록 느려진다
        // 감속구현
    }
}
