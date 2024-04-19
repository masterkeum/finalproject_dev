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
        if (newItem.nameAlias.Length > 10)
        {
            itemName.fontSize = 50;
        }
        itemName.text = newItem.nameAlias;

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
        itemStats.text = statList;

        GlowColorChange(newItem);
    }

    public void OnEquipButton()
    {
        GameManager.Instance.accountInfo.Equip();
        gameObject.SetActive(false);
        inventoryUI.UpdateUI();
        SoundManager.Instance.PlaySound("ChangeEquipUI_1");
    }

    public void GlowColorChange(Item item)
    {
        switch (item.grade)
        {
            case ItemGrade.Normal:
                itemBG.color = ColorTable.normalColor;
                break;
            case ItemGrade.Magic:
                itemBG.color = ColorTable.magicColor;
                break;
            case ItemGrade.Elite:
                itemBG.color = ColorTable.eliteColor;
                break;
            case ItemGrade.Rare:
                itemBG.color = ColorTable.rareColor;
                break;
            case ItemGrade.Epic:
                itemBG.color = ColorTable.epicColor;
                break;
            case ItemGrade.Legendary:
                itemBG.color = ColorTable.legendaryColor;
                break;
        }
    }


}
