using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private MonsterLevel monsterlevel1;
    private MonsterLevel monsterlevel2;
    private MonsterLevel monsterlevel3;

    private void Start()
    {
        player = GameManager.Instance.player;
        itemInfos = DataManager.Instance.itemTableDict[50000003];
        // todo 공부하기!!
        List<MonsterLevel> monaterlevel = DataManager.Instance.characterInfoDict[20100001].monsterLevelData.Values.ToList();
        
        monsterlevel1 = DataManager.Instance.characterInfoDict[20100001].monsterLevelData[1];
        monsterlevel2 = DataManager.Instance.characterInfoDict[20100001].monsterLevelData[2];
        monsterlevel3 = DataManager.Instance.characterInfoDict[20100001].monsterLevelData[3];
        
        getGemText.text = itemInfos.getExp.ToString();
        getHeroJS.text = itemInfos.getExp.ToString();
        getNormalJS.text = itemInfos.getGold.ToString();
        accountInfo.AddGold(player.playeringameinfo.gold);
        accountInfo.AddGem(itemInfos.getExp);
        
        if (monsterlevel1.level == 1)
        {
            accountInfo.AddCore(monsterlevel1.rewardAmount);
        }
        if (monsterlevel2.level == 2)
        {
            accountInfo.AddCore(monsterlevel2.rewardAmount);
        }
        if (monsterlevel3.level == 3)
        {
            accountInfo.AddCore(monsterlevel3.rewardAmount);
        }
        
            
        
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
