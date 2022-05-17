using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageDrag : MonoBehaviour
{
    MouseEvent mouseEvent = null;
    //bool MouseIn = false;

    void Awake()
    {
        mouseEvent = GetComponent<MouseEvent>();
        //mouseEvent.MouseEnterEvent += (PointerEventData data) => { MouseIn = true; };
       // mouseEvent.MouseExitEvent += (PointerEventData data) => { MouseIn = false; };
    }

    void Update()
    {
        //if (Input.GetMouseButton(0) && MouseIn)
        //{
        //    OnMouseDown();
        //}
    }

    void OnMouseDown()
    {
        //Vector2 mousePos = Input.mousePosition;
        //transform.position = mousePos;
    }
}
