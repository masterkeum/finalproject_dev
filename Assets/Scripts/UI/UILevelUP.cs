using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelUP : UIBase
{
    private List<SkillTable> variableSkills = new List<SkillTable>();
    private List<SkillTable> randomSkills = new List<SkillTable>();

    public List<SkillSlotUI> selectableSkillUI = new List<SkillSlotUI>();
    public List<SkillSlotUI> curAcitveSkillUI = new List<SkillSlotUI>();
    public List<SkillSlotUI> curPassiveSkillUI = new List<SkillSlotUI>();


    private GameObject player;
    private PlayerIngameData playerData;


    private void Awake()
    {
        player = GameObject.Find("Player");
        playerData = player.GetComponent<PlayerIngameData>();
        

        foreach (SkillTable skill in DataManager.Instance.SkillTableDict.Values)
        {
            if (skill.skillId > 30000000 && skill.skillId < 30000300)
            {
                variableSkills.Add(skill);
            }
        }

    }
    private void OnEnable()
    {
        SetSelectableSkills();
        SetCurSkills();
        SetStar();


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

        for (int i = 0; i < selectableSkillUI.Count; i++)
        {
            selectableSkillUI[i].skillNameText.text = randomSkills[i].skill;
            selectableSkillUI[i].skillDescriptionText.text = randomSkills[i].skillStatsExplanation;
        }
    }
    private void SetCurSkills()
    {
        //현재 가진 스킬 종류만 표시
        
        for(int i = 0; i<playerData.activeSkillSlot.Count; i++)
        {
            curAcitveSkillUI[i].skillIcon.SetActive(true);
            string path = playerData.activeSkillSlot[i].imageAddress;
            Sprite sprite = Resources.Load<Sprite>(path);
            curAcitveSkillUI[i].skillSprite.sprite = sprite;
            if(sprite == null)
            {
                Debug.LogError("스프라이트 없음");
            }
        }
        for (int i = 0; i<playerData.passiveSkillSlot.Count; i++)
        {
            curPassiveSkillUI[i].skillIcon.SetActive(true);
            string path = playerData.passiveSkillSlot[i].imageAddress;
            Sprite sprite = Resources.Load<Sprite>(path);
            curPassiveSkillUI[i].skillSprite.sprite = sprite;
            
        }
    }

    public void OnButtonSelect(int index)
    {
        
        if (randomSkills[index].applyType == "Active")
        {
            if (playerData.activeSkillSlot.Count == 0)
            {
                playerData.activeSkillSlot.Add(randomSkills[index]);
                
            }
            else
            {
                bool skillFound = false;
                for (int i = 0; i<playerData.activeSkillSlot.Count; i++)
                {
                    if (randomSkills[index] == playerData.activeSkillSlot[i])
                    {
                        playerData.activeSkillSlot[i].level++;
                        skillFound = true;
                        break;
                    }
                }
                if (!skillFound)
                {
                    playerData.activeSkillSlot.Add(randomSkills[index]);
                }
            }

        }
        else
        {

            if (playerData.passiveSkillSlot.Count == 0)
            {             
                playerData.passiveSkillSlot.Add(randomSkills[index]);
            }
            else
            {
                bool skillFound = false;
                for (int i = 0; i < playerData.passiveSkillSlot.Count; i++)
                {
                    if (randomSkills[index] == playerData.passiveSkillSlot[i])
                    {

                        playerData.passiveSkillSlot[i].level++;
                        skillFound = true;
                        break;
                    }
                }
                if (!skillFound)
                {
                    playerData.passiveSkillSlot.Add(randomSkills[index]);
                }
            }
        }
        gameObject.SetActive(false);
        randomSkills.Clear();
    }

    private void SetStar()
    {
        for (int i = 0; i < selectableSkillUI.Count; i++)
        {
            selectableSkillUI[i].ClearStars();
            for (int j = 0; j<playerData.activeSkillSlot.Count; j++)
            {
                if (selectableSkillUI[i].skillNameText.text == playerData.activeSkillSlot[j].skill)
                {
                    selectableSkillUI[i].SetStars(playerData.activeSkillSlot[j].level);
                }
            }
            for (int j = 0; j < playerData.passiveSkillSlot.Count; j++)
            {
                if (selectableSkillUI[i].skillNameText.text == playerData.passiveSkillSlot[j].skill)
                {
                    selectableSkillUI[i].SetStars(playerData.passiveSkillSlot[j].level);
                }
            }
        }
    }
}
