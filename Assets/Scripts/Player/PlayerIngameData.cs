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

    public List<SkillTable> activeSkillSlot = new List<SkillTable>();
    public List<SkillTable> passiveSkillSlot = new List<SkillTable>();

    private void Awake()
    {
        activeSkillSlot.Add(DataManager.Instance.GetSkillTable(30000001));
    }
    
    private void Start()
    {
        sliderMaxExp = DataManager.Instance.GetPlayerIngameLevel(curLevel).exp;
    }

    public int CurrentOpenSkillSlotCount() //허용 슬롯의 숫자. 일단 3으로 정해놓았으나 이후 조건에 따른 값을 리턴하게 한다.
    {
        return 3;
    }
    public void LevelUp()
    {
        curLevel++;
        sliderCurExp = 0;
        sliderMaxExp = DataManager.Instance.GetPlayerIngameLevel(curLevel).exp;
        UIManager.Instance.ShowUI<UILevelUP>();
    }

}
