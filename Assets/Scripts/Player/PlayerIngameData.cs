using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIngameData : MonoBehaviour
{
    public int curLevel = 1;
    public int maxLevel;
    public int sliderCurExp = 0;
    public int sliderMaxExp;
    public int curExp = 0;
    public int maxExp;
    public int killCount = 0;
    public int gold = 0;

    public List<SkillSlot> activeSkillSlot = new List<SkillSlot>();
    public List<SkillSlot> passiveSkillSlot = new List<SkillSlot>();


    private void Awake()
    {
        
    }

    public void LevelUp()
    {
        if(curExp >= DataManager.Instance.GetPlayerIngameLevel(curLevel).totalExp)
        {
            curLevel++;
            sliderCurExp = 0;
            sliderMaxExp = DataManager.Instance.GetPlayerIngameLevel(curLevel).exp;
            UIManager.Instance.ShowUI<UILevelUP>();
        }
    }

}
