using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAccountLevelUp : UIBase
{
    public TextMeshProUGUI levelText;
    public AccountLevelUpRewardSlotUI[] rewardSlotUI;

    private void OnEnable()
    {
        levelText.text = GameManager.Instance.accountInfo.level.ToString();
    }

    public void ClosePopUp()
    {
        gameObject.SetActive(false);
    }
}
