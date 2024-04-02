using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Image icon;
    public Image glow;
    private int _itemIndex;

    private void Start()
    {
        SetIndex();
    }

    public void SetIndex()
    {
        _itemIndex = transform.GetSiblingIndex();
    }

}
