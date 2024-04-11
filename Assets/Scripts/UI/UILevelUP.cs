using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UILevelUP : UIBase
{
    private List<SkillTable> variableSkills = new List<SkillTable>(); // 뜰수있는 스킬 목록
    private List<SkillTable> randomSkills = new List<SkillTable>(); // 화면에 띄울 목록

    public List<SkillSlotUI> selectableSkillUI = new List<SkillSlotUI>(); // 슬롯
    public List<SkillSlotUI> curAcitveSkillUI = new List<SkillSlotUI>();
    public List<SkillSlotUI> curPassiveSkillUI = new List<SkillSlotUI>();

    public TextMeshProUGUI skillPointText;

    private Player player;

    private void Awake()
    {
        player = GameManager.Instance.player;

        foreach (SkillTable skill in DataManager.Instance.skillTableDict.Values)
        {
            if ((skill.applyType == SkillApplyType.Active && skill.isEnable == true && skill.level == 1)
                || (skill.applyType == SkillApplyType.Passive && skill.isEnable == true && skill.level == 1))
            {
                variableSkills.Add(skill);
            }
        }
    }

    private void OnEnable()
    {
        RemoveAtVariableSkills(); // 
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

    /// <summary>
    /// 선택가능한 스킬 UI세팅
    /// </summary>
    private void SetSelectableSkills()
    {
        randomSkills.Clear();

        foreach (SkillTable skill in variableSkills)
        {
            if (skill == null)
            {
                Debug.LogWarning(skill);
            }
        }

        //랜덤스킬 생성, 현재레벨표시
        System.Random random = new System.Random();
        while (randomSkills.Count < selectableSkillUI.Count)
        {
            int randomIndex = random.Next(0, variableSkills.Count);
            if (!randomSkills.Contains(variableSkills[randomIndex]))
            {
                randomSkills.Add(variableSkills[randomIndex]);
                if (randomSkills.Count >= variableSkills.Count
                    || randomSkills.Count >= selectableSkillUI.Count)
                {
                    break;
                }
            }
        }

        for (int i = 0; i < selectableSkillUI.Count; i++)
        {
            if (i < randomSkills.Count)
            {
                selectableSkillUI[i].skillSprite.color = Color.white;
                selectableSkillUI[i].gameObject.SetActive(true);
                selectableSkillUI[i].skillGroupId = randomSkills[i].skillGroup;
                selectableSkillUI[i].skillNameText.text = randomSkills[i].skillName;
                selectableSkillUI[i].skillDescriptionText.text = randomSkills[i].skillDesc;
                string path = randomSkills[i].imageAddress;
                Sprite sprite = Resources.Load<Sprite>(path);
                selectableSkillUI[i].skillSprite.sprite = sprite;
                switch (randomSkills[i].applyType)
                {
                    case SkillApplyType.Active:
                        selectableSkillUI[i].activeIcon.SetActive(true);
                        selectableSkillUI[i].passiveIcon.SetActive(false);
                        break;
                    case SkillApplyType.Passive:
                        selectableSkillUI[i].activeIcon.SetActive(false);
                        selectableSkillUI[i].passiveIcon.SetActive(true);
                        break;
                    case SkillApplyType.Awaken:
                        selectableSkillUI[i].activeIcon.SetActive(false);
                        selectableSkillUI[i].passiveIcon.SetActive(false);
                        selectableSkillUI[i].skillSprite.color = new Color(0, 1, 1, 1);
                        break;
                }
            }
            else
            {
                selectableSkillUI[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 현재 스킬 UI노출
    /// </summary>
    private void SetCurSkills()
    {
        int activeSlotCount = player.CurrentOpenSkillSlotCount(SkillApplyType.Active);
        int passiveSlotCount = player.CurrentOpenSkillSlotCount(SkillApplyType.Passive);

        //현재 가진 스킬 종류만 표시
        for (int i = 0; i < player.activeSkillSlot.Count; i++)
        {

            curAcitveSkillUI[i].skillIcon.SetActive(true);
            curAcitveSkillUI[i].skillSprite.sprite = Resources.Load<Sprite>(player.activeSkillSlot[i].imageAddress);
            if (player.activeSkillSlot[i].applyType == SkillApplyType.Awaken)
            {
                curAcitveSkillUI[i].skillSprite.color = new Color(0,1,1,1); 
            }
        }
        for (int i = 0; i < player.passiveSkillSlot.Count; i++)
        {
            curPassiveSkillUI[i].skillIcon.SetActive(true);
            curPassiveSkillUI[i].skillSprite.sprite = Resources.Load<Sprite>(player.passiveSkillSlot[i].imageAddress);
        }

        for (int i = 0; i < 6; i++)
        {
            if (activeSlotCount <= i)
            {
                curAcitveSkillUI[i].skillLock.SetActive(true);
            }
            if (passiveSlotCount <= i)
            {
                curPassiveSkillUI[i].skillLock.SetActive(true);
            }
        }
    }

    /// <summary>
    /// UI 레벨 표시
    /// </summary>
    private void SetStar()
    {
        for (int i = 0; i < selectableSkillUI.Count; i++)
        {
            selectableSkillUI[i].ClearStars();
            for (int j = 0; j < player.activeSkillSlot.Count; j++)
            {
                if (selectableSkillUI[i].skillGroupId == player.activeSkillSlot[j].skillGroup)
                {
                    selectableSkillUI[i].SetStars(player.activeSkillSlot[j].level);
                }
            }
            for (int j = 0; j < player.passiveSkillSlot.Count; j++)
            {
                if (selectableSkillUI[i].skillGroupId == player.passiveSkillSlot[j].skillGroup)
                {
                    selectableSkillUI[i].SetStars(player.passiveSkillSlot[j].level);
                }
            }
        }

        for (int i = 0; i <player.activeSkillSlot.Count; i++)
        {
            curAcitveSkillUI[i].SetStars(player.activeSkillSlot[i].level);
        }
        for (int i = 0; i <player.passiveSkillSlot.Count;i++)
        {
            curPassiveSkillUI[i].SetStars(player.passiveSkillSlot[i].level);
        }
    }

    /// <summary>
    /// 스킬 선택. 스킬 변경이 일어난다
    /// </summary>
    /// <param name="index">스킬 index</param>
    public void OnButtonSelect(int index)
    {
        // 선택된 스킬을 내 스킬에 저장
        player.SkillUpdate(randomSkills[index]);

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

    /// <summary>
    /// 선택 가능한 스킬 목록 관리. 슬롯 꽉차면 더 안뜸
    /// </summary>
    public void RemoveAtVariableSkills()
    {
        if (player.activeSkillSlot.Count > player.CurrentOpenSkillSlotCount(SkillApplyType.Active) - 1)
        {
            variableSkills.RemoveAll(skill => !player.activeSkillSlot.Contains(skill) && skill.applyType == SkillApplyType.Active);
        }
        if (player.passiveSkillSlot.Count > player.CurrentOpenSkillSlotCount(SkillApplyType.Passive) - 1)
        {
            variableSkills.RemoveAll(skill => !player.passiveSkillSlot.Contains(skill) && skill.applyType == SkillApplyType.Passive);
        }

        // 들고 있는 스킬 순회
        foreach (SkillTable skill in player.activeSkillSlot)
        {
            // 이미 선택된 스킬을 확인해서 빼준다.
            if (variableSkills.Contains(skill))
            {
                variableSkills.Remove(skill);
            }
            // 다음 스킬 조건 확인 후 삽입
            if (skill.nextSkillId != 0 && (skill.reqSkillId == 0 || player.passiveSkill.ContainsKey(skill.reqSkillId)))
            {
                if (DataManager.Instance.GetSkillTable(skill.nextSkillId) == null)
                {
                    Debug.LogWarning($"NotFound Skill : {skill.nextSkillId}");
                    continue;
                }
                if (!variableSkills.Contains(DataManager.Instance.GetSkillTable(skill.nextSkillId)))
                    variableSkills.Add(DataManager.Instance.GetSkillTable(skill.nextSkillId));
            }
        }
        foreach (SkillTable skill in player.passiveSkillSlot)
        {
            if (variableSkills.Contains(skill))
            {
                variableSkills.Remove(skill);
            }

            if (skill.nextSkillId != 0)
            {
                if (DataManager.Instance.GetSkillTable(skill.nextSkillId) == null)
                {
                    Debug.LogWarning($"NotFound Skill : {skill.nextSkillId}");
                    continue;
                }
                if (!variableSkills.Contains(DataManager.Instance.GetSkillTable(skill.nextSkillId)))
                    variableSkills.Add(DataManager.Instance.GetSkillTable(skill.nextSkillId));
            }
        }
    }

    /// <summary>
    /// 스킬 선택목록 리롤
    /// </summary>
    public void OnReRollButton()
    {
        SetSelectableSkills();
        SetStar();
    }
}
