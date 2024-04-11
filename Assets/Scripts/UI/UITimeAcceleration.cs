using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITimeAcceleration : UIBase
{
    public TextMeshProUGUI timeText;

    
    Coroutine getTime;

    private void OnEnable()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        if (accountInfo.isLevelUpProcessing && accountInfo.completeTime > UtilityKit.GetCurrentDateTime())
        {
            getTime = StartCoroutine(GetTimeUI());
        }
        UpdateUI();
    }

    private IEnumerator GetTimeUI()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        while (accountInfo.completeTime>UtilityKit.GetCurrentDateTime())
        {
            DateTime now = UtilityKit.GetCurrentDateTime();
            TimeSpan waitTime = accountInfo.completeTime - now;
            string time = waitTime.ToString(@"hh\:mm\:ss");
            timeText.text = time;
            yield return new WaitForSeconds(1f);
            if (accountInfo.completeTime < UtilityKit.GetCurrentDateTime())
                break;
            
        }
    }

    public void UpdateUI()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        if (accountInfo.completeTime < UtilityKit.GetCurrentDateTime())
        {
            StopCoroutine(GetTimeUI());
        }
    }

    public void WatchADButton()
    {
        SoundManager.Instance.PlaySound("ButtonClickUI_1");
    }

    public void UseTicketbutton()
    {
        UIManager.Instance.ShowUI<UITicketUse>();
        SoundManager.Instance.PlaySound("ButtonClickUI_1");
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
        SoundManager.Instance.PlaySound("ButtonClickUI_1");
    }


}
