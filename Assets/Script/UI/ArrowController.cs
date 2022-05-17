using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowController : MonoBehaviour
{

    public enum DIRECTION
    {
        LEFT, RIGHT
    }
    public DIRECTION mydir;

    public GameObject player;
    MouseEvent mouseEvent = null;

    bool MouseIn = false;

    void Awake()
    {
        mouseEvent = GetComponent<MouseEvent>();
        mouseEvent.MouseEnterEvent += (PointerEventData data) => { MouseIn = true; };
        mouseEvent.MouseExitEvent += (PointerEventData data) => { MouseIn = false; };
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && MouseIn)
        {
            OnMouseDown();
        }
    }

    public void OnMouseDown()
    {
        switch(mydir)
        {
            case DIRECTION.LEFT:
                Vector3 dir = Vector2.left * 3.0f * Time.deltaTime;
                player.transform.position += dir;
                break;
            case DIRECTION.RIGHT:
                Vector3 dir1 = Vector2.right * 3.0f * Time.deltaTime;
                player.transform.position += dir1;
                break;
        }
    }

}
