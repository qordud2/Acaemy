using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLerpTest : MonoBehaviour
{
    public Vector3 StartRotation = Vector3.zero;
    public Vector3 EndRotation = new Vector3(0, 180, 0);
    public float SlerpValue = 0.0f;

    void Start()
    {
        
    }


    void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.Slerp(StartRotation, EndRotation, SlerpValue));    
        // 오일러를 쿼터니언으로 변경
    }
}
