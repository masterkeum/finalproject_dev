using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINewEquip : UIBase
{
    private InventoryUI inventoryUI;

    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemStats;
    [SerializeField] private Image itemBG;

    private void OnEnable()
    {
        SetNewItem();
        inventoryUI = FindObjectOfType<InventoryUI>();

    }

    private void SetNewItem()
    {
        Item newItem = GameManager.Instance.accountInfo.newItem;
        string psth = newItem.ImageFile;
        itemIcon.sprite = Resources.Load<Sprite>(psth);
        if(newItem.nameAlias.Length > 10)
        {
            itemName.fontSize = 60;
        }
        itemName.text = newItem.nameAlias;

        GlowColorChange(newItem);
    }

    public void OnEquipButton()
    {
        GameManager.Instance.accountInfo.Equip();
        gameObject.SetActive(false);
        inventoryUI.UpdateUI();
    }

    public void GlowColorChange(Item item)
    {
        switch(item.grade)
        {
            case ItemGrade.Normal:
                itemBG.color = new Color(1f, 1f, 1f);
                break;
            case ItemGrade.Magic:
                itemBG.color = new Color(40 / 255f, 1f, 35 / 255f);
                break;
            case ItemGrade.Elite:
                itemBG.color = new Color(0f, 67 / 255f, 1f);
                break;
            case ItemGrade.Rare:
                itemBG.color = new Color(1f, 115 / 255f, 0f);
                break;
            case ItemGrade.Epic:
                itemBG.color = new Color(1f, 1f, 0f);
                break;
            case ItemGrade.Legendary:
                itemBG.color = new Color(1f, 0f, 0f);
                break;
        }
    }

   
}
