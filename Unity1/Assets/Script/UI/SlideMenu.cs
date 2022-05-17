using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class SlideMenu : MonoBehaviour
{
    public enum STATE
    {
        CREATE, HIDE, SHOWING, SHOW, HIDING
    }

    public STATE myState = STATE.CREATE;

    Vector3 ShowPos = Vector3.zero;
    Vector3 HidePos = Vector3.zero;

    public MouseEvent myEvent;

    public Sprite[] Iconse;
    public Image myImage;
    GameUtiles GameUtile;

    void Start()
    {
        HidePos = transform.localPosition;
        HidePos.x = GameUtiles.CANVAS.pixelRect.width / 2 + GetComponent<RectTransform>().sizeDelta.x / 2;
        ShowPos = HidePos;
        ShowPos.x -= GetComponent<RectTransform>().sizeDelta.x;
        Debug.Log("HidePos" + HidePos);
        Debug.Log("ShowPos" + ShowPos);
        ChangeState(STATE.HIDE);

        myEvent.MouseEnterEvent += OnShow;
        myEvent.MouseClickEvent += OnHide;
    }

    void OnShow(PointerEventData eventData)
    {
        if (myState != STATE.HIDE) return;
        ChangeState(STATE.SHOWING);
    }

    void OnHide(PointerEventData eventData)
    {
        if (myState != STATE.SHOW) return;
        ChangeState(STATE.HIDING);
    }

    void Update()
    {
        StateProcess();
    }

    void ChangeState(STATE s)
    {
        if (myState == s)
            return;
        myState = s;
        switch (myState)
        {
            case STATE.CREATE:
                break;
            case STATE.SHOW:
                myImage.sprite = Iconse[1];
                transform.localPosition = ShowPos;
                break;
            case STATE.SHOWING:
                // 이동이 끝나면 ChangeState(STATE.SHOW)가 호출된다
                StartCoroutine(Moveing(DoneShowing, ShowPos, 10.0f));

                break;
            case STATE.HIDE:
                myImage.sprite = Iconse[0];
                transform.localPosition = HidePos;
                break;
            case STATE.HIDING:
                StartCoroutine(Moveing(() => ChangeState(STATE.HIDE), HidePos, 10.0f));
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.CREATE:
                break;
            case STATE.SHOW:
                break;
            case STATE.SHOWING:
                break;
            case STATE.HIDE:
                break;
            case STATE.HIDING:
                break;
        }
    }

    IEnumerator Moveing(UnityAction done, Vector3 target, float speed)
    {
        Vector3 pos = transform.localPosition;
        float playtime = 0f;

        while(playtime < 1f)
        {
            playtime += Time.smoothDeltaTime * speed;
            pos = Vector3.Lerp(pos, target, playtime);
            transform.localPosition = pos;
            yield return null;
        }
        transform.localPosition = target;
        done?.Invoke();
    }

    void DoneShowing()
    {
        ChangeState(STATE.SHOW);
    }
}
