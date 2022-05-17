using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MinimapIcon : MonoBehaviour
{
    //public Image playerImage;
    //public Image MonsterImage;

    // Start is called before the first frame update
    void Start()
    {
        // 없애기
        transform.SetParent(GameObject.Find("MiniMap").transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FollowTransform(Transform target)
    {
        // target은 World상의 위치다
        StartCoroutine(Following(target));
    }

    IEnumerator Following(Transform target)
    {
        while(target != null)
        {
            //allCameras 배열중 depth가 작을수록 앞에온다
            Vector3 pos = Camera.allCameras[1].WorldToViewportPoint(target.position);
            pos.x = pos.x * 200.0f - 100.0f;
            pos.y = pos.y * 200.0f - 100.0f;

            transform.localPosition = pos;

            yield return null;
        }

        
    }
}
