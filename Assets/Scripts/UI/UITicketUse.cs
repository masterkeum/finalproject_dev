using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITicketUse : UIBase
{
    public int ticketCountToUse = 1;

    public TextMeshProUGUI currentticketCountText;
    public TextMeshProUGUI ticketToUseText;


    private void OnEnable()
    {
        ticketCountToUse = 1;
        UpdateUI();
    }

    private void UpdateUI()
    {
        currentticketCountText.text = GameManager.Instance.accountInfo.timeTicket.ToString();
        ticketToUseText.text = ticketCountToUse.ToString();

    }

    public void MinusButton()
    {
        ticketCountToUse -= 1;
        UpdateUI();
    }
    public void MinusTenButton()
    {
        ticketCountToUse -= 10;
        UpdateUI();
    }
    public void PlusButton()
    {
        ticketCountToUse += 1;
        UpdateUI();
    }
    public void PlusTenButton()
    {
        ticketCountToUse += 10;
        UpdateUI();
    }

    public void UseButton()
    {
       if(GameManager.Instance.accountInfo.timeTicket >= ticketCountToUse)
        {
            GameManager.Instance.accountInfo.AddTimeTicket(-ticketCountToUse);
            GameManager.Instance.accountInfo.completeTime = GameManager.Instance.accountInfo.completeTime.AddSeconds(-300 * ticketCountToUse);
            gameObject.SetActive(false);
        }

    }

    public void SelectTicketAll()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        TimeSpan remainTime = accountInfo.completeTime - DateTime.UtcNow;
        if (300 * accountInfo.timeTicket <= remainTime.TotalSeconds)
        {
            ticketCountToUse = accountInfo.timeTicket;
            UpdateUI();
        }
        else
        {
            ticketCountToUse = ((int)remainTime.TotalSeconds / 300)+1;
            UpdateUI();
        }
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }


}
