using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void MouseEventData(PointerEventData data);

public class MouseEvent : MonoBehaviour,
    IPointerClickHandler, IPointerDownHandler,  // 클릭하면 호출됨, 
    IDragHandler,           // 드래그하면 호출됨
    IEndDragHandler,        // 드래그가 멈추면 호출됨
    IPointerEnterHandler, IPointerExitHandler   // 마우스 포인터가 움직이다가 이미지 위로 overrap되면 호출됨, 이미지를 벗어나면 호출됨
{
    public event MouseEventData MouseClickEvent;
    public event MouseEventData MouseDownEvent;
    public event MouseEventData MouseDragEvent;
    public event MouseEventData MouseEndDragEvent;
    public event MouseEventData MouseEnterEvent;
    public event MouseEventData MouseExitEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("OnPointerClick");    // Down하고 Up할때 호출됨
        MouseClickEvent?.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
        MouseDownEvent?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag" + eventData.position);
        MouseDragEvent?.Invoke(eventData);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag" + eventData.position);
        MouseEndDragEvent?.Invoke(eventData);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter" + eventData.position);
        MouseEnterEvent?.Invoke(eventData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit" + eventData.position);
        MouseExitEvent?.Invoke(eventData);
    }
}
