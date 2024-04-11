using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameClear : UIBase
{
    public Player player;

    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI getGoldText;
    public TextMeshProUGUI getGemText;
    public TextMeshProUGUI getNormalJS;
    public TextMeshProUGUI getHeroJS;
    public ItemTable itemInfos;
    private AccountInfo accountInfo;

    private void Start()
    {
        player = GameManager.Instance.player;
        itemInfos = DataManager.Instance.itemTableDict[50000003];
        
        // todo core 데이터 부분이 정해지지 않아 임의 데이터 삽입. 추후 수정.
        Debug.Log("코어값 로드 "+itemInfos);
        Debug.Log("코어값 getExp "+itemInfos.getExp);
        Debug.Log("코어값 getGold "+itemInfos.getGold);
        getGemText.text = itemInfos.getExp.ToString();
        getHeroJS.text = itemInfos.getExp.ToString();
        getNormalJS.text = itemInfos.getGold.ToString();
        
        // 팝업이 뜨면서 accountinfo 에 게임데이터 저장
        accountInfo.AddGold(player.playeringameinfo.gold);
        accountInfo.AddGem(itemInfos.getExp);
        accountInfo.AddCore(itemInfos.getExp);
    }

    private void Update()
    {
        killCountText.text = player.playeringameinfo.killCount.ToString();
        getGoldText.text = player.playeringameinfo.gold.ToString();
        //getGemText.text 플레이 시간에 비례해서 제공?
    }

    public void GetDoubleReward()
    {
        Debug.Log("GetDoubleReward");
    }

    public void GetCommonReward()
    {
        Debug.Log("GetCommonReward");
        SceneManager.LoadScene(2);
    }



}
