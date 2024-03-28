using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipChange : UIBase
{
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
    }

    public void Set()
    {
        string newPath = GameManager.Instance.accountInfo.newItem.ImageFile;
        newIcon.sprite = Resources.Load<Sprite>(newPath);
        newItemNameText.text = GameManager.Instance.accountInfo.newItem.nameAlias;
        GlowColorChange(newGlow,GameManager.Instance.accountInfo.newItem);
        
        
        string curPath = CurChangeableItme().ImageFile;
        curIcon.sprite = Resources.Load<Sprite>(curPath);
        curItemNameText.text = CurChangeableItme().nameAlias;
        GlowColorChange(curGlow, CurChangeableItme());

    }

    private ItemTable CurChangeableItme()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        switch(accountInfo.newItem.itemType)
        {
            case "Weapon":
                return accountInfo.equipItems.Weapon;
            case "Armor":
                return accountInfo.equipItems.Armor;
            case "Gloves":
                return accountInfo.equipItems.Gloves;
            case "Boots":
                return accountInfo.equipItems.Boots;
            case "Helmet":
                return accountInfo.equipItems.Helmet;
            case "Accessorries":
                return accountInfo.equipItems.Accessories;
        }
        return null;
    }

    private void GlowColorChange(Image glow, ItemTable item)
    {
        switch(item.grade)
        {
            case "Normal":
                glow.color = new Color(1f, 1f, 1f);
                break;
            case "Magic":
                glow.color = new Color(40 / 255f, 1f, 35 / 255f);
                break;
            case "Elite":
                glow.color = new Color(0f, 67 / 255f, 1f);
                break;
            case "Rare":
                glow.color = new Color(1f, 115 / 255f, 0f);
                break;
            case "Epic":
                glow.color = new Color(1f, 1f, 0f);
                break;
            case "Legendary":
                glow.color = new Color(1f, 0f, 0f);
                break;
        }
    }

    public void OnEquip()
    {
        GameManager.Instance.accountInfo.Equip();
        Set();
    }


    public void OnDigestion()
    {
        //경험치와 골드 얻기
        gameObject.SetActive(false);
    }

}