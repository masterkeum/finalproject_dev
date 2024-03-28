using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINewEquip : UIBase
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemStats;
    private void Start()
    {
        SetNewItem();
    }

    private void SetNewItem()
    {
        // TODO 이미지
        ItemTable newItem = GameManager.Instance.accountInfo.newItem;
        itemName.text = newItem.nameAlias;
    }

    public void OnEquipButton()
    {
        GameManager.Instance.accountInfo.Equip();
        gameObject.SetActive(false);
    }

   
}
