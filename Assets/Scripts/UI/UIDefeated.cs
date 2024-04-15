using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIDefeated : UIBase 
{
    private Player player;

    public GameObject LifeDown;
    public GameObject TimeOver;
    public TextMeshProUGUI KillCountText;
    public TextMeshProUGUI GetGoldText;

    private void Awake()
    {
        player = GameManager.Instance.player;
    }

    private void OnEnable()
    {
        Debug.Log("UIDefeated");
        //타임아웃으로 게임오버가 됐냐, 체력소진으로 게임오버가 됐냐에 따라 표시할 요소 결정
        if (player.playeringameinfo.killCount != null)
        {
            KillCountText.text = player.playeringameinfo.killCount.ToString();    
        }
        else
        {
            KillCountText.text = "0";
        }

        if(player.playeringameinfo.gold != null)
        {
            GetGoldText.text = player.playeringameinfo.gold.ToString();    
        }
        else
        {
            GetGoldText.text = "0";
        }
        
        
    }

    public void Resurrection()
    {
        Debug.Log("부활했습니다.");
        
        var adPopup = UIManager.Instance.ShowUI<UIAdsPage>();
        ++UIManager.Instance.popupUICount;
        adPopup.Init(AdsStates.Defeated);
        
        
        // 두가지 방법, 함수를 만들어주고 변수를 통해 enum , adpage 에서 처리 // 성능저하는 없다. 코드 늘어남
        // opened 로 파라미터로 값 넘겨줘서 사용할 수 있다. // 박싱언박싱 성능저하 이슈

        
    }

    public void GiveUP()
    {
        SceneManager.LoadScene("MainScene");
    }
}
