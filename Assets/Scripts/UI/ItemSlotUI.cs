using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Image icon;
    private int _itemIndex;

    private void Start()
    {
        SetIndex();

    }

    public void SetIndex()
    {
        _itemIndex = transform.GetSiblingIndex();
    }

    public void Set()
    {
        
    }
    public void Clear()
    {
        Destroy(gameObject);
    }

    public void Onclick()
    {
        UIManager.Instance.ShowUI<UIItemDescription>();
    }
}
