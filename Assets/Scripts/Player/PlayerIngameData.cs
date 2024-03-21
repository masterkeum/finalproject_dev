using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerIngameData : Player
{

    private void Awake()
    {
        curLevel = 1;
        sliderCurExp = 0;
        curExp = 0;
        totalExp = 0;
        killCount = 0;
        gold = 0;

        activeSkillSlot.Add(DataManager.Instance.GetSkillTable(30000001)); // 기본스킬 지급
    }

    private void Start()
    {
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        sliderMaxExp = DataManager.Instance.GetPlayerIngameLevel(curLevel).exp;
    }

    private void UpdateCurLevel()
    {
        for (int i = curLevel; i <= DataManager.Instance.PlayerIngameLevelDict.Count; i++)
        {
            PlayerIngameLevel levelData = DataManager.Instance.GetPlayerIngameLevel(i);
            if (totalExp <= levelData.totalExp)
            {
                if (curLevel < levelData.level)
                {
                    curLevel = levelData.level;
                    UIManager.Instance.ShowUI<UILevelUP>();
                }
                curExp = levelData.totalExp - totalExp;
                sliderCurExp = curExp;
                break;
            }
        }
    }

    public int CurrentOpenSkillSlotCount() //허용 슬롯의 숫자. 일단 3으로 정해놓았으나 이후 조건에 따른 값을 리턴하게 한다.
    {
        return 3;
    }

    public void AddKillCount()
    {
        killCount++;
    }

    public void AddGoldCount(int addGold)
    {
        gold += addGold;
    }

    public void AddExp(int addExp)
    {
        totalExp += addExp;
        UpdateCurLevel();
    }


    // 테스트 코드
    public void LevelUp()
    {
        curLevel++;
        sliderCurExp = 0;
        UpdateSlider();
        UIManager.Instance.ShowUI<UILevelUP>();
    }

}
