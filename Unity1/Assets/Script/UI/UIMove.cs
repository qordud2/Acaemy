using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMove : MonoBehaviour
{
    Canvas myCanvas;
    Image myImage;

    float a, b;
    Vector3 dir;

    public GameObject Space;
    public GameObject Target;

    void Start()
    {
        myCanvas = FindObjectOfType<Canvas>();
        myImage = GetComponent<Image>();

        Vector3 CanSize = myCanvas.GetComponent<RectTransform>().sizeDelta;

        myImage.transform.position = CanSize / 2;

        dir = myImage.transform.position - Target.transform.position;
        dir.Normalize();
        //StartCoroutine(Move(Target.transform.position, Vector3.Distance(myImage.transform.position, Target.transform.position)));
    }

    IEnumerator Move(Vector3 target, float dist)
    {
        float moveSpeed = 100.0f;

        while(dist > Mathf.Epsilon)
        {
            float delta = Time.deltaTime;
            if(dist - delta < Mathf.Epsilon)
            {
                delta = dist;
            }
            dist -= delta;

            Vector3 pos = transform.position;
            pos += dir * delta * moveSpeed;
            transform.position = pos;
            Debug.Log(dir);
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        Space.SetActive(true);

        
    }
}
