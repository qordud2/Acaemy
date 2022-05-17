using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
    Transform TargetTrasnform;
    public GameObject EffectResource;
    Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetFloat("x", Input.GetAxis("Horizontal"));
        myAnim.SetFloat("y", Input.GetAxis("Vertical"));

        if(Input.GetKeyDown(KeyCode.F))
        {
            myAnim.SetTrigger("Attack");
        }
    }

    void OnAnimatorMove()
    {
        //transform.Translate(myAnim.deltaPosition);    // 현재 프레임에서 이동한 값
        transform.parent.position = transform.parent.position + myAnim.deltaPosition;
        Vector3 rot = transform.rotation.eulerAngles;       // eulerAngles - 오일러 값(transform에서는 오일러 값으로 되어있다)
        Vector3 anirot = myAnim.deltaRotation.eulerAngles;  // 이것도 오일러 값으로 변환
        rot += anirot;  // 오일러 각도를 더해서 이 각도만큼 더 회전했다
        transform.rotation = Quaternion.Euler(rot);   // 회전 한 값을 쿼터니언으로 처리했다, 회전의 의한 애니메이션 처리 
        //transform.Rotate(anirot);   // Rotate에는 쿼터니언으로 회전시키기 때문이 이렇게 해도 동일하다

        // 짐벌락현상 - 회전값은 x, y, z같은 회전값인데 xyz순서에 따라 회전값이 다 다르다
        // (회전을 하다보면 축이 겹쳐져서 컴퓨터 입장에서 혼돈이 일어난다 - 어느축으로 해야대나)
        // 그래서 쿼터니언을 사용한다.(완벽히 막는것은 아니지만 거의 막아진다)
        // 쿼터니언의 + 는 우리가 생각하는 각도 더하기가 아니라서 오일러로 더하고 쿼터니언으로 변환시킨다
        // * 는 된다고함(x = 50, y = 120 - qx * qy 하면 x로 50도 y로 120도 회전한다는 뜻)

    }

    void OnAttack()
    {
        
        GameObject obj = Instantiate(EffectResource) as GameObject;
        obj.transform.position = TargetTrasnform.position;
        //obj.transform.position = transform.Find("boss:Hat_Geo").position; // 이름을 찾아서 직접 넣어줄수도 있다
        obj.transform.SetParent(transform);
        //obj.transform.parent = transform or nullptr;
    }
}
