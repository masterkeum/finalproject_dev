using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelUP : UIBase
{
    private List<SkillTable> variableSkills = new List<SkillTable>();
    private List<SkillTable> randomSkills = new List<SkillTable>();

    public List<SkillSlotUI> selectableSkillUI = new List<SkillSlotUI>();
    public List<SkillSlotUI> curAcitveSkillUI =new List<SkillSlotUI>();
    public List<SkillSlotUI> curPassiveSkillUI = new List<SkillSlotUI>();

    private GameObject player;
    private PlayerIngameData playerData;


    private void Awake()
    {
        player = GameObject.Find("Player");
        playerData = player.GetComponent<PlayerIngameData>();

        foreach(SkillTable skill in DataManager.Instance.SkillTableDict.Values)
        {
            if(skill.skillId >30000000 && skill.skillId < 30000300)
            {
                variableSkills.Add(skill);
            }
        }
        
    }
    private void OnEnable()
    {
        SetSelectableSkills();
        SetCurSkills();
    }

    private void SetSelectableSkills()
    {
        //랜덤스킬 생성, 현재레벨표시
        System.Random random = new System.Random();
        while (randomSkills.Count < selectableSkillUI.Count)
        {
            int randomIndex = random.Next(0, variableSkills.Count);
            if (randomSkills.Count >= selectableSkillUI.Count)
            { break; }
            if (!randomSkills.Contains(variableSkills[randomIndex]))
            randomSkills.Add(variableSkills[randomIndex]);
        }

        for(int i = 0; i< selectableSkillUI.Count; i++)
        {
            selectableSkillUI[i].skillNameText.text = randomSkills[i].skill;
            selectableSkillUI[i].skillDescriptionText.text = randomSkills[i].skillStatsExplanation;
        }
    }
    private void SetCurSkills()
    {
        //현재 가진 스킬 종류만 표시
    }

    public void OnButtonSelect(int index)
    {
        if (randomSkills[index].type == "Active")
        {
            playerData.activeSkillSlot.Add(randomSkills[index]);
        }
        else
        {
            playerData.passiveSkillSlot.Add(randomSkills[index]);
        }
        gameObject.SetActive(false);
        randomSkills.Clear();
    }
}
