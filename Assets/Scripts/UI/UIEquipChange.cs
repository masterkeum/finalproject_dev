using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipChange : UIBase
{
    private InventoryUI inventoryUI;

    public Image newIcon;
    public Image curIcon;
    public Image newGlow;
    public Image curGlow;
    public TextMeshProUGUI newItemNameText;
    public TextMeshProUGUI newItemDescriptionText;
    public TextMeshProUGUI curItemNameText;
    public TextMeshProUGUI curItemDescriptionText;
    public GameObject equipButton;
    public GameObject sellButton;


    private void OnEnable()
    {
        Set();
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    public void Set()
    {
        string newPath = GameManager.Instance.accountInfo.newItem.ImageFile;
        newIcon.sprite = Resources.Load<Sprite>(newPath);
        Item newItem = GameManager.Instance.accountInfo.newItem;
        if (newItem.nameAlias.Length > 10)
        {
            newItemNameText.fontSize = 50;
        }
        newItemNameText.text = GameManager.Instance.accountInfo.newItem.nameAlias;
        GlowColorChange(newGlow, GameManager.Instance.accountInfo.newItem);

        string statList = "";
        if (newItem.Hp > 0)
            statList += "체력 : " + newItem.Hp + "\n";
        if (newItem.Dp > 0)
            statList += "방어력 : " + newItem.Dp + "\n";
        if (newItem.Ap > 0)
            statList += "공격력 : " + newItem.Ap + "\n";
        if (newItem.MoveSpeed > 0)
            statList += "이동속도 : " + newItem.MoveSpeed + "\n";
        if (newItem.CriticalHit > 0)
            statList += "치명타 : " + newItem.CriticalHit + "\n";
        if (newItem.HpGen > 0)
            statList += "재생 : " + newItem.HpGen + "\n";
        newItemDescriptionText.text = statList;


        string curPath = CurChangeableItme().ImageFile;
        curIcon.sprite = Resources.Load<Sprite>(curPath);
        curItemNameText.text = CurChangeableItme().nameAlias;
        GlowColorChange(curGlow, CurChangeableItme());
        
        string curStatList = "";
        if (CurChangeableItme().Hp > 0)
            curStatList += "체력 : " + CurChangeableItme().Hp + "\n";
        if (CurChangeableItme().Dp > 0)
            curStatList += "방어력 : " + CurChangeableItme().Dp + "\n";
        if (CurChangeableItme().Ap > 0)
            curStatList += "공격력 : " + CurChangeableItme().Ap + "\n";
        if (CurChangeableItme().MoveSpeed > 0)
            curStatList += "이동속도 : " + CurChangeableItme().MoveSpeed + "\n";
        if (CurChangeableItme().CriticalHit > 0)
            curStatList += "치명타 : " + CurChangeableItme().CriticalHit + "\n";
        if (CurChangeableItme().HpGen > 0)
            curStatList += "재생 : " + CurChangeableItme().HpGen + "\n";
        curItemDescriptionText.text = curStatList;

    }

    private Item CurChangeableItme()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        switch (accountInfo.newItem.itemType)
        {
            case ItemType.Weapon:
                return accountInfo.equipItems.Weapon;
            case ItemType.Armor:
                return accountInfo.equipItems.Armor;
            case ItemType.Gloves:
                return accountInfo.equipItems.Gloves;
            case ItemType.Boots:
                return accountInfo.equipItems.Boots;
            case ItemType.Helmet:
                return accountInfo.equipItems.Helmet;
            case ItemType.Accessories:
                return accountInfo.equipItems.Accessories;
        }
        return null;
    }

    private void GlowColorChange(Image glow, Item item)
    {
        switch (item.grade)
        {
            case ItemGrade.Normal:
                glow.color = new Color(1f, 1f, 1f);
                break;
            case ItemGrade.Magic:
                glow.color = new Color(40 / 255f, 1f, 35 / 255f);
                break;
            case ItemGrade.Elite:
                glow.color = new Color(0f, 67 / 255f, 1f);
                break;
            case ItemGrade.Rare:
                glow.color = new Color(1f, 115 / 255f, 0f);
                break;
            case ItemGrade.Epic:
                glow.color = new Color(1f, 1f, 0f);
                break;
            case ItemGrade.Legendary:
                glow.color = new Color(1f, 0f, 0f);
                break;
        }
    }

    public void OnEquip()
    {
        GameManager.Instance.accountInfo.Equip();
        inventoryUI.UpdateUI();
        Set();
    }


    public void OnDigestion()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        //경험치와 골드 얻기
        accountInfo.AddGold(accountInfo.newItem.getGold);
        accountInfo.AddExp(accountInfo.newItem.getExp);

        accountInfo.newItem = null;
        gameObject.SetActive(false);
    }

}