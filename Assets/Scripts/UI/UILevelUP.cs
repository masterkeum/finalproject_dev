using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILevelUP : UIBase
{
    private List<SkillTable> variableSkills = new List<SkillTable>();
    private List<SkillTable> randomSkills = new List<SkillTable>();

    public List<SkillSlotUI> selectableSkillUI = new List<SkillSlotUI>();
    public List<SkillSlotUI> curAcitveSkillUI = new List<SkillSlotUI>();
    public List<SkillSlotUI> curPassiveSkillUI = new List<SkillSlotUI>();

    public TextMeshProUGUI skillPointText;

    private Player player;

    private void Awake()
    {
        player = GameManager.Instance.player;

        foreach (SkillTable skill in DataManager.Instance.skillTableDict.Values)
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
        UpdateSkillPoint();
    }

    /// <summary>
    /// 남은 스킬포인트 UI갱신
    /// </summary>
    private void UpdateSkillPoint()
    {
        skillPointText.text = $"스킬포인트 + {player.playeringameinfo.skillpoint}";
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
            selectableSkillUI[i].skillNameText.text = randomSkills[i].skillName;
            selectableSkillUI[i].skillDescriptionText.text = randomSkills[i].skillDesc;
            string path = randomSkills[i].imageAddress;
            Sprite sprite = Resources.Load<Sprite>(path);
            selectableSkillUI[i].skillSprite.sprite = sprite;
        }
    }
    private void SetCurSkills()
    {
        //현재 가진 스킬 종류만 표시

        for (int i = 0; i < player.activeSkillSlot.Count; i++)
        {
            curAcitveSkillUI[i].skillIcon.SetActive(true);
            string path = player.activeSkillSlot[i].imageAddress;
            Sprite sprite = Resources.Load<Sprite>(path);
            curAcitveSkillUI[i].skillSprite.sprite = sprite;
        }
        for (int i = 0; i < player.passiveSkillSlot.Count; i++)
        {
            curPassiveSkillUI[i].skillIcon.SetActive(true);
            string path = player.passiveSkillSlot[i].imageAddress;
            Sprite sprite = Resources.Load<Sprite>(path);
            curPassiveSkillUI[i].skillSprite.sprite = sprite;
        }
        for (int j = player.CurrentOpenSkillSlotCount(); j < 6; j++)  // 잠긴 슬롯에 자물쇠 세팅
        {
            curAcitveSkillUI[j].skilllock.SetActive(true);
            curPassiveSkillUI[j].skilllock.SetActive(true);
        }
    }

    /// <summary>
    /// 스킬 선택
    /// FIXME : 깊은복사로 변경
    /// </summary>
    /// <param name="index">스킬 index</param>
    public void OnButtonSelect(int index)
    {
        if (randomSkills[index].skillId == 30000301)  // 초월스킬 변경
        {
            for (int i = 0; i < player.activeSkillSlot.Count; i++)
            {
                if (player.activeSkillSlot[i].skillId == 30000001)
                {
                    player.activeSkillSlot.Remove(player.activeSkillSlot[i]);
                    player.activeSkillSlot.Add(randomSkills[index]);

                }
            }

        }

        else if (randomSkills[index].applyType == SkillApplyType.Active)
        {
            if (player.activeSkillSlot.Count == 0)
            {
                player.activeSkillSlot.Add(randomSkills[index]);

            }
            else
            {
                bool skillFound = false;
                for (int i = 0; i < player.activeSkillSlot.Count; i++)
                {
                    if (randomSkills[index] == player.activeSkillSlot[i])
                    {
                        player.activeSkillSlot[i].level++;
                        skillFound = true;

                        break;
                    }
                }
                if (!skillFound)
                {
                    player.activeSkillSlot.Add(randomSkills[index]);

                }

            }
        }
        else
        {

            if (player.passiveSkillSlot.Count == 0)
            {
                player.passiveSkillSlot.Add(randomSkills[index]);
            }
            else
            {
                bool skillFound = false;
                for (int i = 0; i < player.passiveSkillSlot.Count; i++)
                {
                    if (randomSkills[index] == player.passiveSkillSlot[i])
                    {

                        player.passiveSkillSlot[i].level++;
                        skillFound = true;
                        break;
                    }
                }
                if (!skillFound)
                {
                    player.passiveSkillSlot.Add(randomSkills[index]);
                }
            }
        }


        --player.playeringameinfo.skillpoint;

        if (player.playeringameinfo.skillpoint > 0)
        {
            RemoveAtVariableSkills();
            OnReRollButton();
            UpdateSkillPoint();
            SetCurSkills();
        }
        else
        {
            gameObject.SetActive(false);
            randomSkills.Clear();
            --UIManager.Instance.popupUICount;
        }
    }


    private void SetStar()
    {
        for (int i = 0; i < selectableSkillUI.Count; i++)
        {
            selectableSkillUI[i].ClearStars();
            for (int j = 0; j < player.activeSkillSlot.Count; j++)
            {
                if (selectableSkillUI[i].skillNameText.text == player.activeSkillSlot[j].skillName)
                {
                    selectableSkillUI[i].SetStars(player.activeSkillSlot[j].level);
                }
            }
            for (int j = 0; j < player.passiveSkillSlot.Count; j++)
            {
                if (selectableSkillUI[i].skillNameText.text == player.passiveSkillSlot[j].skillName)
                {
                    selectableSkillUI[i].SetStars(player.passiveSkillSlot[j].level);
                }
            }
        }
    }

    /// <summary>
    /// 초월스킬로 변경
    /// </summary>
    /// <param name="skill"></param>
    private void SkillTransform(SkillTable skill)
    {

        switch (skill.skillId)
        {
            case 30000001:
                variableSkills.Add(DataManager.Instance.skillTableDict[30000301]);
                break;
            case 30000002:
                variableSkills.Add(DataManager.Instance.skillTableDict[30000302]);
                break;
            case 30000003:
                variableSkills.Add(DataManager.Instance.skillTableDict[30000303]);
                break;
            case 30000004:
                variableSkills.Add(DataManager.Instance.skillTableDict[30000304]);
                break;
            case 30000005:
                variableSkills.Add(DataManager.Instance.skillTableDict[30000305]);
                break;
            case 30000006:
                variableSkills.Add(DataManager.Instance.skillTableDict[30000306]);
                break;
        }

    }


    /// <summary>
    /// 스킬레벨이 5가 되거나 허용슬롯이 꽉 찼을 때, 추가할 수 없는 스킬을 variable 목록에서 제외한다.
    /// </summary>
    public void RemoveAtVariableSkills()
    {
        if (player.activeSkillSlot.Count > player.CurrentOpenSkillSlotCount() - 1)
        {
            variableSkills.RemoveAll(skill => !player.activeSkillSlot.Contains(skill) && skill.applyType == SkillApplyType.Active);
        }
        if (player.passiveSkillSlot.Count > player.CurrentOpenSkillSlotCount() - 1)
        {
            variableSkills.RemoveAll(skill => !player.passiveSkillSlot.Contains(skill) && skill.applyType == SkillApplyType.Passive);
        }
        foreach (SkillTable skill in player.activeSkillSlot)
        {
            if (skill.level >= skill.maxLevel)
            {
                variableSkills.Remove(skill);
                SkillTransform(skill);
            }
        }
        foreach (SkillTable skill in player.passiveSkillSlot)
        {
            if (skill.level >= skill.maxLevel)
            {
                variableSkills.Remove(skill);
            }
        }

    }

    /// <summary>
    /// 스킬 선택목록 리롤
    /// </summary>
    public void OnReRollButton()
    {
        randomSkills.Clear();
        SetSelectableSkills();
        SetStar();
    }
}
