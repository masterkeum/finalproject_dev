using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class UICheckEquip : UIBase
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemStats;
    [SerializeField] private Image itemBG;

    private void OnEnable()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        string path = accountInfo.checkCurItem.ImageFile;
        itemIcon.sprite = Resources.Load<Sprite>(path);

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

    public void OnConfirmButton()
    {
        GameManager.Instance.accountInfo.checkCurItem = null;
        gameObject.SetActive(false);
        SoundManager.Instance.PlaySound("ButtonClickUI_1");
    }
}
