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
        RemoveAtVariableSkills();
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
            string path = randomSkills[i].imageAddress;
            Sprite sprite = Resources.Load<Sprite>(path);
            selectableSkillUI[i].skillSprite.sprite = sprite;
        }
    }
    private void SetCurSkills()
    {
        //현재 가진 스킬 종류만 표시

        for (int i = 0; i < playerData.activeSkillSlot.Count; i++)
        {
            curAcitveSkillUI[i].skillIcon.SetActive(true);
            string path = playerData.activeSkillSlot[i].imageAddress;
            Sprite sprite = Resources.Load<Sprite>(path);
            curAcitveSkillUI[i].skillSprite.sprite = sprite;
        }
        for (int i = 0; i < playerData.passiveSkillSlot.Count; i++)
        {
            curPassiveSkillUI[i].skillIcon.SetActive(true);
            string path = playerData.passiveSkillSlot[i].imageAddress;
            Sprite sprite = Resources.Load<Sprite>(path);
            curPassiveSkillUI[i].skillSprite.sprite = sprite;
        }
        for (int j = playerData.CurrentOpenSkillSlotCount(); j < 6; j++)  // 잠긴 슬롯에 자물쇠 세팅
        {
            curAcitveSkillUI[j].skilllock.SetActive(true);
            curPassiveSkillUI[j].skilllock.SetActive(true);
        }
    }

    public void OnButtonSelect(int index)
    {
        if (randomSkills[index].skillId == 30000301)  // 초월스킬 변경
        {
            for (int i = 0; i < playerData.activeSkillSlot.Count; i++)
            {
                if (playerData.activeSkillSlot[i].skillId == 30000001)
                {
                    playerData.activeSkillSlot.Remove(playerData.activeSkillSlot[i]);
                    playerData.activeSkillSlot.Add(randomSkills[index]);

                }
            }

        }

        else if (randomSkills[index].applyType == "Active")
        {
            if (playerData.activeSkillSlot.Count == 0)
            {
                playerData.activeSkillSlot.Add(randomSkills[index]);

            }
            else
            {
                bool skillFound = false;
                for (int i = 0; i < playerData.activeSkillSlot.Count; i++)
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
            for (int j = 0; j < playerData.activeSkillSlot.Count; j++)
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

    private void SkillTransform(SkillTable skill)  //초월스킬로 변경
    {

        switch (skill.skillId)
        {
            case 30000001:
                variableSkills.Add(DataManager.Instance.SkillTableDict[30000301]);
                break;
            case 30000002:
                variableSkills.Add(DataManager.Instance.SkillTableDict[30000302]);
                break;
            case 30000003:
                variableSkills.Add(DataManager.Instance.SkillTableDict[30000303]);
                break;
            case 30000004:
                variableSkills.Add(DataManager.Instance.SkillTableDict[30000304]);
                break;
            case 30000005:
                variableSkills.Add(DataManager.Instance.SkillTableDict[30000305]);
                break;
            case 30000006:
                variableSkills.Add(DataManager.Instance.SkillTableDict[30000306]);
                break;
        }

    }
    public void RemoveAtVariableSkills() //스킬레벨이 5가 되거나 허용슬롯이 꽉 찼을 때, 추가할 수 없는 스킬을 variable 목록에서 제외한다.
    {
        if (playerData.activeSkillSlot.Count > playerData.CurrentOpenSkillSlotCount() - 1)
        {
            variableSkills.RemoveAll(skill => !playerData.activeSkillSlot.Contains(skill) && skill.applyType == "Active");
        }
        if (playerData.passiveSkillSlot.Count > playerData.CurrentOpenSkillSlotCount() - 1)
        {
            variableSkills.RemoveAll(skill => !playerData.passiveSkillSlot.Contains(skill) && skill.applyType == "Passive");
        }
        foreach (SkillTable skill in playerData.activeSkillSlot)
        {
            if (skill.level >= skill.maxLevel)
            {
                variableSkills.Remove(skill);
                SkillTransform(skill);
            }
        }
        foreach (SkillTable skill in playerData.passiveSkillSlot)
        {
            if (skill.level >= skill.maxLevel)
            {
                variableSkills.Remove(skill);
            }
        }

    }


    public void OnReRollButton()
    {
        randomSkills.Clear();
        SetSelectableSkills();
        SetStar();
    }
}
