using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    
    public GameObject Space;
    Image myImage;
    
    Color color;
    public enum STATE
    {
        CREATE, INIT, START, CENTER, DISAPEAR, END
    }

    
    public STATE myState = STATE.CREATE;

    void Start()
    {
        ChangeState(STATE.INIT);
    }

    void Update()
    {
        StateProcess();
    }

    void Init()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        myImage = GetComponent<Image>();
        color = myImage.color;
        // 캔버스의 width 불러오는 방법들
        //canvas.GetComponent<RectTransform>().sizeDelta.x;
        //canvas.pixelRect.width;

        // 캔버스.width / 2 + image.width / 2 해야 캔버스 밖으로 나간다
        Vector3 pos = GetComponent<RectTransform>().localPosition;
        pos.x = canvas.pixelRect.width / 2 + GetComponent<RectTransform>().sizeDelta.x / 2;
        GetComponent<RectTransform>().localPosition = pos;
    }

    Vector3 dir = Vector3.zero;
    float dist = 0f;

    void Alpha()
    {
        Space.SetActive(true);
    }

    void ChangeState(STATE s)
    {
        if (myState == s)
            return;
        myState = s;

        switch(myState)
        {
            case STATE.CREATE:
                break;
            case STATE.INIT:
                Init();
                ChangeState(STATE.START);
                break;
            case STATE.START:
                {
                    dir = new Vector3(-1, 0, 0);
                    Canvas canvas = FindObjectOfType<Canvas>();
                    dist = canvas.pixelRect.width / 2 + GetComponent<RectTransform>().sizeDelta.x / 2;
                }
                break;
            case STATE.CENTER:
                {
                    Alpha();
                }
                break;
            case STATE.DISAPEAR:
                {
                    Space.SetActive(false);
                    speed = 0;
                    dir = new Vector3(-1, 0, 0);
                    Canvas canvas = FindObjectOfType<Canvas>();
                    dist = canvas.pixelRect.width / 2 + GetComponent<RectTransform>().sizeDelta.x / 2;
                } 
                break;
            case STATE.END:
                break;
        }
    }

    float speed = 100.0f;
    float Accel = 5.0f;

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.CREATE:
                break;
            case STATE.INIT:
                break;
            case STATE.START:
                {
                    speed += Accel;
                    float delta = Time.deltaTime * speed;
                    if (dist - delta <= Mathf.Epsilon)
                    {
                        delta = dist;
                        ChangeState(STATE.CENTER);
                    }
                    dist -= delta;
                    transform.Translate(dir * delta);
                }
                break;
            case STATE.CENTER:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ChangeState(STATE.DISAPEAR);
                }
                break;
            case STATE.DISAPEAR:
                {
                    speed += Accel;
                    float delta = Time.deltaTime * speed;
                    if (dist - delta <= Mathf.Epsilon)
                    {
                        delta = dist;
                        ChangeState(STATE.END);
                    }
                    dist -= delta;
                    transform.Translate(dir * delta);
                }
                break;
            case STATE.END:
                break;
        }
    }
}
