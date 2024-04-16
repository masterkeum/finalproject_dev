using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIAdsPage : UIBase
{
    public GameObject uiAdsPage;
    public GameObject buttonsDefeated;
    public GameObject buttonsClear;
    public GameObject buttonsFree;
    public GameObject buttonsReroll;
    private Player player;
    
    public void Init(AdsStates adsStates)
    {
        buttonsDefeated.SetActive(adsStates == AdsStates.Defeated);
        buttonsClear.SetActive(adsStates == AdsStates.Clear);
        buttonsFree.SetActive(adsStates == AdsStates.Free);
        buttonsReroll.SetActive(adsStates == AdsStates.Reroll);
        player = GameManager.Instance.player;
    }
    public void Defeated() // 캐릭터 부활
    {
        Debug.Log("캐릭터 부활");
        uiAdsPage.SetActive(false);
        UIManager.Instance.Clear();
        player.ResetPlayerHP();
    }

    public void GameClear() // 재화2배획득
    {
        Debug.Log("광고시청 완료, 재화 2배 획득");
        SceneManager.LoadScene(2);
    }

   public void GetFreeGem() // 무료재화 잼 획득
    {
        Debug.Log("무료재화 잼 획득");
        GameManager.Instance.accountInfo.AddGem(10);
        SoundManager.Instance.PlaySound("PurchaseUI_1");
        uiAdsPage.SetActive(false);
    }

    public void SkillReroll()
    {
        Debug.Log("SkillReroll" );
        uiAdsPage.SetActive(false);
        // UIManager.Instance.Clear();
        --UIManager.Instance.popupUICount;
    }
}
