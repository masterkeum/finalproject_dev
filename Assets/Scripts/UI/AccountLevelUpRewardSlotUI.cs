using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountLevelUpRewardSlotUI : MonoBehaviour
{
    public Image rewardIcon;
    public TextMeshProUGUI rewardAmount;

    private int index;

    private void OnEnable()
    {
        SetIndex();
    }

    private void SetIndex()
    {
        index = transform.GetSiblingIndex();
    }

    private void SetReward()
    {
        switch (index)
        {
            case 0:
                {

                }
                break;
            case 1:
                {

                }
                break;
            case 2:
                {

                }
                break;
        }
    }
}
