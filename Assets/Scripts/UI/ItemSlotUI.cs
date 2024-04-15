using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Image icon;
    public Image glow;
    public GameObject decoGO;
    public Image decoImage;
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
