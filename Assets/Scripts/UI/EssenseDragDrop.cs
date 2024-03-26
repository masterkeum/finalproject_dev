using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EssenseDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("드래그시작");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

        Debug.Log("드래그중");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("드래그끝");
    }
}
