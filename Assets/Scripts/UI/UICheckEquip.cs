using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICheckEquip : UIBase
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemStats;
    [SerializeField] private Image itemBG;
    [SerializeField] private TextMeshProUGUI itemGrade;

    private void OnEnable()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        string path = accountInfo.checkCurItem.ImageFile;
        itemIcon.sprite = Resources.Load<Sprite>(path);

        if (accountInfo.checkCurItem.nameAlias.Length > 10)
        {
            itemName.fontSize = 50;
        }
        else if (accountInfo.checkCurItem.nameAlias.Length > 5)
        {
            itemName.fontSize = 60;
        }
        else
        {
            itemName.fontSize = 80;
        }

        itemName.text = accountInfo.checkCurItem.nameAlias;
        string statList = "";
        if (accountInfo.checkCurItem.Hp > 0)
            statList += "체력 : " + accountInfo.checkCurItem.Hp + "\n";
        if (accountInfo.checkCurItem.Dp > 0)
            statList += "방어력 : " + accountInfo.checkCurItem.Dp + "\n";
        if (accountInfo.checkCurItem.Ap > 0)
            statList += "공격력 : " + accountInfo.checkCurItem.Ap + "\n";
        if (accountInfo.checkCurItem.MoveSpeed > 0)
            statList += "이동속도 : " + accountInfo.checkCurItem.MoveSpeed + "\n";
        if (accountInfo.checkCurItem.CriticalHit > 0)
            statList += "치명타 : " + accountInfo.checkCurItem.CriticalHit + "\n";
        if (accountInfo.checkCurItem.HpGen > 0)
            statList += "재생 : " + accountInfo.checkCurItem.HpGen + "\n";
        itemStats.text = statList;

        GlowColorChange(accountInfo.checkCurItem);
    }

    public void GlowColorChange(Item item)
    {
        switch (item.grade)
        {
            case ItemGrade.Normal:
                itemBG.color = ColorTable.normalColor;
                itemGrade.text = "(노멀)";
                break;
            case ItemGrade.Magic:
                itemBG.color = ColorTable.magicColor;
                itemGrade.text = "(매직)";
                break;
            case ItemGrade.Elite:
                itemBG.color = ColorTable.eliteColor;
                itemGrade.text = "(엘리트)";
                break;
            case ItemGrade.Rare:
                itemBG.color = ColorTable.rareColor;
                itemGrade.text = "(레어)";
                break;
            case ItemGrade.Epic:
                itemBG.color = ColorTable.epicColor;
                itemGrade.text = "(에픽)";
                break;
            case ItemGrade.Legendary:
                itemBG.color = ColorTable.legendaryColor;
                itemGrade.text = "(레전더리)";
                break;
        }
    }

    public void OnConfirmButton()
    {
        GameManager.Instance.accountInfo.checkCurItem = null;
        gameObject.SetActive(false);
        SoundManager.Instance.PlaySound("ButtonClickUI_1");
    }
}
