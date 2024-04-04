using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMimicLevelUp : UIBase
{
    public TextMeshProUGUI playerLevelText;
    public TextMeshProUGUI playerGoldText;
    public TextMeshProUGUI playerTicketText;
    public TextMeshProUGUI levelUpTimer;
    public GameObject expBuyButton;
    public Slider expSlider;
    public GameObject timerNAcceleration;

    [Header("Timer")]
    public bool isLevelUpProcessing = false;
    public float totalTime;
    public float currentTime;
    

    [Header("CurProbability")]
    public TextMeshProUGUI probabilityNormalCur;
    public TextMeshProUGUI probabilityMagicCur;
    public TextMeshProUGUI probabilityEliteCur;
    public TextMeshProUGUI probabilityRareCur;
    public TextMeshProUGUI probabilityEpicCur;
    public TextMeshProUGUI probabilityLegendaryCur;

    [Header("NextProbability")]
    public TextMeshProUGUI probabilityNormalNext;
    public TextMeshProUGUI probabilityMagicNext;
    public TextMeshProUGUI probabilityEliteNext;
    public TextMeshProUGUI probabilityRareNext;
    public TextMeshProUGUI probabilityEpicNext;
    public TextMeshProUGUI probabilityLegendaryNext;

    private void OnEnable()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;

        playerLevelText.text = accountInfo.level.ToString();
        playerGoldText.text = accountInfo.gold.ToString();
        //playerTicketText.text = accountInfo.ticket.ToString();

    }
}
