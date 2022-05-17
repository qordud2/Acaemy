using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void AllClose();

public class TestUI : MonoBehaviour
{
    int data = 0;
    // 프로퍼티
    // data가 값이 바뀌는 순간 데이터를 쓰지 않는이상 바로 알아챌수 없는데 프로퍼티를 쓰면 바로 값의 변화를 알 수 있다
    public int Data
    {
        // 처음 get이 호출되서 data를 가져온다
        get
        {
            return data;
        }
        // 값을 대입하면 set이 호출된다
        // value는 Data와 타입이 동일하다
        set
        {
            data = value;
        }
    }

    public GameObject popup;
    Vector2 PopupPos = Vector2.zero;

    // event를 붙이면 외부에서 추가 삭제는 가능한데 실행은 불가능하다
    //public event Action onDeath;
    //AllClose myClose = null;
    public event AllClose myClose = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // f1을 누르면 팝업창이 대각선 방향으로 조금씩 움직이면서 화면에 출력되도록
        if(Input.GetKeyDown(KeyCode.F1))
        {
            GameObject obj = Instantiate(popup);
            obj.transform.SetParent(GameUtiles.CANVAS.transform);
            Debug.Log("PopupPos : " + PopupPos);
            Debug.Log("obj.transform : " + obj.transform);
            obj.transform.localPosition = PopupPos;
            PopupPos.x += 10.0f;
            PopupPos.y -= 10.0f;
            myClose += obj.GetComponent<Popup>().ClosePopup;
        }
    }

    public void AllClosePopup()
    {
        // 둘다 같은 의미
        //if (myClose != null) myClose();
        myClose?.Invoke();
        myClose = null;
    }

    public void RemoveClose(Popup up)
    {

    }
}
