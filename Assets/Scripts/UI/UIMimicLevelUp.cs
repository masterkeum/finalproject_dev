using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using Unity.Mathematics;
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
    public GameObject expSlider_go;
    public GameObject timerNAcceleration;
    public GameObject levelUpButton;
    public GameObject completeButton;
    public TextMeshProUGUI reqExpText;


    AccountInfo accountInfo = GameManager.Instance.accountInfo;
    Coroutine timerRoutine;


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
        UpdateUI();
        if (accountInfo.isLevelUpProcessing && accountInfo.completeTime > UtilityKit.GetCurrentDateTime())
        {
            timerRoutine = StartCoroutine(UpdateTimerRoutine());
        }


    }

    public void UpdateUI()
    {
        playerLevelText.text = accountInfo.mimicLevel.ToString();
        playerGoldText.text = accountInfo.gold.ToString();
        playerTicketText.text = accountInfo.timeTicket.ToString();
        reqExpText.text = DataManager.Instance.mimiclevelDict[accountInfo.mimicLevel + 1].reqgold.ToString();

        probabilityNormalCur.text = DataManager.Instance.levelGachaDict[accountInfo.mimicLevel].normal.ToString("F4");
        probabilityMagicCur.text = DataManager.Instance.levelGachaDict[accountInfo.mimicLevel].magic.ToString("F4");
        probabilityEliteCur.text = DataManager.Instance.levelGachaDict[accountInfo.mimicLevel].elite.ToString("F4");
        probabilityRareCur.text = DataManager.Instance.levelGachaDict[accountInfo.mimicLevel].rare.ToString("F4");
        probabilityEpicCur.text = DataManager.Instance.levelGachaDict[accountInfo.mimicLevel].epic.ToString("F4");
        probabilityLegendaryCur.text = DataManager.Instance.levelGachaDict[accountInfo.mimicLevel].legendary.ToString("F4");

        probabilityNormalNext.text = DataManager.Instance.levelGachaDict[accountInfo.mimicLevel + 1].normal.ToString("F4");
        probabilityMagicNext.text = DataManager.Instance.levelGachaDict[accountInfo.mimicLevel + 1].magic.ToString("F4");
        probabilityEliteNext.text = DataManager.Instance.levelGachaDict[accountInfo.mimicLevel + 1].elite.ToString("F4");
        probabilityRareNext.text = DataManager.Instance.levelGachaDict[accountInfo.mimicLevel + 1].rare.ToString("F4");
        probabilityEpicNext.text = DataManager.Instance.levelGachaDict[accountInfo.mimicLevel + 1].epic.ToString("F4");
        probabilityLegendaryNext.text = DataManager.Instance.levelGachaDict[accountInfo.mimicLevel + 1].legendary.ToString("F4");

        float sliderCurValue = accountInfo.mimicCurExp;
        float sliderMaxValue = DataManager.Instance.mimiclevelDict[accountInfo.mimicLevel].totalExp - DataManager.Instance.mimiclevelDict[accountInfo.mimicLevel].totalExp;

        expSlider.value = sliderCurValue == 0 ? 0 : sliderCurValue / sliderMaxValue;

        if (expSlider.value < 1 && accountInfo.isLevelUpProcessing == false)
        {
            expSlider_go.SetActive(true);
            expBuyButton.SetActive(true);
        }
        if (expSlider.value == 1 && accountInfo.isLevelUpProcessing == false)
        {
            expBuyButton.SetActive(false);
            levelUpButton.SetActive(true);
        }
        else if (accountInfo.isLevelUpProcessing == true)
        {
            levelUpButton.SetActive(false);
            expBuyButton.SetActive(false);
            timerNAcceleration.SetActive(true);
            expSlider_go.SetActive(false);
        }
        if (accountInfo.isLevelUpProcessing == true && UtilityKit.GetCurrentDateTime() >= accountInfo.completeTime)
        {
            completeButton.SetActive(true);
            timerNAcceleration.SetActive(false);
        }
    }

    public void ExpBuyButton()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        if (accountInfo.gold < DataManager.Instance.mimiclevelDict[accountInfo.mimicLevel + 1].reqgold)
        {
            UINoCurrency popup = UIManager.Instance.ShowUI<UINoCurrency>();
            popup.NoGold();
            return;
        }
        else
        {
            accountInfo.gold -= DataManager.Instance.mimiclevelDict[accountInfo.mimicLevel + 1].reqgold;
            accountInfo.AddMimicExp(1);
        }
        UpdateUI();
        GameManager.Instance.UpdateUI();

    }

    public void SetTimer() //레벨업 시작 버튼
    {
        accountInfo.isLevelUpProcessing = true;
        accountInfo.completeTime = UtilityKit.GetCurrentDateTime().AddSeconds(DataManager.Instance.mimiclevelDict[accountInfo.mimicLevel + 1].reqsec);
        if (timerRoutine != null)
        {
            StopCoroutine(timerRoutine);
        }
        timerRoutine = StartCoroutine(UpdateTimerRoutine());
    }

    public IEnumerator UpdateTimerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            TimeSpan remainingTime = UpdateTimer();
            if (remainingTime.TotalSeconds < 0)
            {
                yield break;
            }

        }

    }
    public TimeSpan UpdateTimer()
    {
        DateTime now = UtilityKit.GetCurrentDateTime();
        TimeSpan waitTime = accountInfo.completeTime - now;
        if (accountInfo.isLevelUpProcessing == true)
        {
            string time = waitTime.ToString(@"hh\:mm\:ss");
            levelUpTimer.text = time;

        }
        UpdateUI();
        return waitTime;
    }

    public void CompleteMimicLevelUp() //레벨업 완료 버튼
    {
        if (timerRoutine != null)
        {
            StopCoroutine(timerRoutine);
        }
        accountInfo.isLevelUpProcessing = false;
        accountInfo.mimicLevel++;
        accountInfo.mimicTotalExp = DataManager.Instance.mimiclevelDict[accountInfo.mimicLevel + 1].totalExp;
        accountInfo.mimicCurExp = 0;

        UpdateUI();
        completeButton.SetActive(false);
    }

    public void TimeAccelerationButton()
    {
        UIManager.Instance.ShowUI<UITimeAcceleration>();
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
