using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public GameObject icon;
    private int _itemIndex;

    private void Start()
    {
        SetIndex();
        icon = transform.GetChild(1).gameObject;

    }

    public void SetIndex()
    {
        _itemIndex = transform.GetSiblingIndex();
    }

    public void Clear()
    {
        Destroy(gameObject);
    }

}
