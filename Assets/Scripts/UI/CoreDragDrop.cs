using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoreDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 commonPosition;
    public string coreType;

    private void Start()
    {
        commonPosition = transform.position;
    }
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
        transform.position = commonPosition;
    }
}
