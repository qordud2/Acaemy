using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    Canvas myCanvas;
    MouseEvent mouseEvent = null;
    //bool MouseIn = false;

    void Awake()
    {
        myCanvas = FindObjectOfType<Canvas>();
        mouseEvent = GetComponent<MouseEvent>();
        mouseEvent.MouseDragEvent += OnDrag;
    }


    void OnDrag(PointerEventData data)
    {
        Vector2 mousePos = Input.mousePosition;
        transform.localPosition = data.position - myCanvas.GetComponent<RectTransform>().sizeDelta / 2;
    }
}
