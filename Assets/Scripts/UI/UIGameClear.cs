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

    private void Start()
    {
        player = GameManager.Instance.player;
        itemInfos = DataManager.Instance.itemTableDict[50000003];
        
        Debug.Log("코어값 로드 "+itemInfos);
        Debug.Log("코어값 getExp "+itemInfos.getExp);
        Debug.Log("코어값 getGold "+itemInfos.getGold);
        getGemText.text = itemInfos.itemType.ToString();
        getHeroJS.text = itemInfos.getExp.ToString();
        getNormalJS.text = itemInfos.getGold.ToString();
        // itemInfos.itemId
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
