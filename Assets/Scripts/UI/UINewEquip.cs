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
    [SerializeField] private Image itemBG;

    private void OnEnable()
    {
        SetNewItem();
    }

    private void SetNewItem()
    {
        ItemTable newItem = GameManager.Instance.accountInfo.newItem;
        string psth = newItem.ImageFile;
        itemIcon.sprite = Resources.Load<Sprite>(psth);
        itemName.text = newItem.nameAlias;

        GlowColorChange(newItem);
    }

    public void OnEquipButton()
    {
        GameManager.Instance.accountInfo.Equip();
        gameObject.SetActive(false);
    }

    public void GlowColorChange(ItemTable item)
    {
        switch(item.grade)
        {
            case "Normal":
                itemBG.color = new Color(1f, 1f, 1f);
                break;
            case "Magic":
                itemBG.color = new Color(40 / 255f, 1f, 35 / 255f);
                break;
            case "Elite":
                itemBG.color = new Color(0f, 67 / 255f, 1f);
                break;
            case "Rare":
                itemBG.color = new Color(1f, 115 / 255f, 0f);
                break;
            case "Epic":
                itemBG.color = new Color(1f, 1f, 0f);
                break;
            case "Legendary":
                itemBG.color = new Color(1f, 0f, 0f);
                break;
        }
    }

   
}
