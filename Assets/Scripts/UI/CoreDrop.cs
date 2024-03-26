using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoreDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject.GetComponent<CoreDragDrop>() != null)
        {
            if (droppedObject.GetComponent<CoreDragDrop>().coreType == "Common")
            {
                //일반 뽑기
            }
            else
            {
                //영웅 뽑기
            }
        }
    }
}
