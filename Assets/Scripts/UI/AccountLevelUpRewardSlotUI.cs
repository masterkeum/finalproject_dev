using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountLevelUpRewardSlotUI : MonoBehaviour
{
    public TextMeshProUGUI rewardName;
    public TextMeshProUGUI rewardAmount;

    private int index;

    private void OnEnable()
    {
        SetIndex();
        SetReward();
    }

    private void SetIndex()
    {
        index = transform.GetSiblingIndex();
    }

    private void SetReward()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        switch (index)
        {
            case 0:
                {
                    rewardName.text = "체력";
                    rewardAmount.text = (DataManager.Instance.playerLevelDict[accountInfo.level].hp - DataManager.Instance.playerLevelDict[accountInfo.level-1].hp).ToString();

                }
                break;
            case 1:
                {
                    rewardName.text = "공격력";
                    rewardAmount.text = (DataManager.Instance.playerLevelDict[accountInfo.level].attack - DataManager.Instance.playerLevelDict[accountInfo.level-1].attack).ToString();
                }
                break;
        }
    }
}
