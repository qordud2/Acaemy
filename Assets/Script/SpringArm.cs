using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    public LayerMask CrashMask;
    public Transform myCam;

    public float RotSpeed = 1.0f;
    public float RotSmoothSpeed = 10.0f;
    public Vector3 TargetRot;

    public float ZoomSpeed = 3.0f;
    public float ZoomSmoothSpeed = 10.0f;
    public float CurDist = 0.0f;
    public float TargetDist = 0.0f;
    public float CollisionOffset = 1.0f;

    public Vector2 LookUpArea;
    public Vector2 ZoomArea;

    bool move = false;

    // Start is called before the first frame update

    void Awake()
    {
        myCam = Camera.main.transform;
        CurDist = TargetDist = Vector3.Distance(transform.position, myCam.position);
        TargetRot = transform.rotation.eulerAngles;
    }
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            // LookUp
            //Vector3 rot = transform.localRotation.eulerAngles;   // 현재 회전값
            TargetRot.x += -Input.GetAxis("Mouse Y") * RotSpeed;
            if(TargetRot.x > 180.0f)
                TargetRot.x -= 360.0f;
            TargetRot.x = Mathf.Clamp(TargetRot.x, LookUpArea.x, LookUpArea.y);
            //transform.localRotation = Quaternion.Euler(TargetRot);

            //transform.Rotate(Vector3.right * -Input.GetAxis("Mouse Y") * RotSpeed, Space.Self);

            // TurnRight
            TargetRot.y += Input.GetAxis("Mouse X") * RotSpeed;
            //transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * RotSpeed, Space.World);
            move = false;
        }
        // TargetRot.x, TargetRot.y를 Slerp한다
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(TargetRot), Time.smoothDeltaTime * RotSmoothSpeed);

        // 1 ~ 7
        if(Input.GetAxis("Mouse ScrollWheel") > Mathf.Epsilon || Input.GetAxis("Mouse ScrollWheel") < -Mathf.Epsilon)
        {
            TargetDist += -Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
            TargetDist = Mathf.Clamp(TargetDist, ZoomArea.x, ZoomArea.y);
            move = true;
        }
        CurDist = Mathf.Lerp(CurDist, TargetDist, Time.smoothDeltaTime * ZoomSmoothSpeed);
        //myCam.position = transform.position + (-transform.forward * CurDist);
        

        Ray ray = new Ray();
        ray.origin = transform.position;    // ray 시작지점
        ray.direction = -transform.forward;
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, CurDist + CollisionOffset, CrashMask) && move == false)
        {            
            myCam.position = hit.point - ray.direction * CollisionOffset;
            // 벽보다 CollisionOffset아래를 체크하고 반대방향으로 CollisionOffset만큼 카메라를 이동시킨다
            //float RayDist = (CurDist > TargetDist) ? TargetDist : CurDist;
            //조건 : if (CurDist > Vector3.Distance(this.transform.position, hit.point))

                CurDist = Vector3.Distance(transform.position, hit.point);
        }
        else
        {
            myCam.position = transform.position + (-transform.forward * CurDist);
        }
    }
}
