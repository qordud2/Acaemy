using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCorourtine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //int [] list = new int[10];

        //foreach(int n in list)
        //{
        // c#에서는 이런식으로 배열을 for문 돌린다
        //}
        StartCoroutine(UpDown(3.0f, 5.0f));
    }

    // Update is called once per frame
    void Update()
    {
    }

    //IEnumerator Up()
    //{
    //    //float speed = 0.5f;
    //    //int count = 0;
    //    //while(true)
    //    //{
    //    //    //transform.Translate(Vector3.up * Time.deltaTime * speed);
    //    //    //myText.text = count.ToString() + "초";

    //    //    ++count;
    //    //    yield return new WaitForSeconds(1.0f);
    //    //    // WaitForEndOfFrame()

    //    //    //yield return null;  // c#의 null == c++ nullptr 
    //    //    // 코로틴은 한번 translate하고 null return이 발동한다 (return이 사라지는게 아니고 살아있다?)
    //    //    // 그다음은 null return발동하고 translate가 발동한다 계속 (Update의 Tranlate있는 것과 같다)
    //    //    // 변수도 항상 살아있게된다
    //    //    // 매 프레임에 들어온다 (한번씩 발동한다)
    //    //    // yield - 지연하다, null 지연시간이 없다
    //    //
    //}

    IEnumerator UpDown(float height, float speed)
    {
        float dist = height;
        Vector3 dir = Vector3.up;
        while (true)
        {
            float delta = speed * Time.deltaTime;
            if (dist - delta < Mathf.Epsilon)
            {
                delta = dist;
                dir *= -1.0f;
                dist = height;
            }
            else
            {
                dist -= delta;
            }
            transform.Translate(dir * delta);
            yield return null;
        } 
    }
}
