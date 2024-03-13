using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelUP : UIBase
{
    public List<SkillSlotUI> selectableSkillUI = new List<SkillSlotUI>();
    public List<SkillSlotUI> curAcitveSkillUI =new List<SkillSlotUI>();
    public List<SkillSlotUI> curPassiveSkillUI = new List<SkillSlotUI>();

    private void OnEnable()
    {
        SetSelectableSkills();
        SetCurSkills();
    }

    private void SetSelectableSkills()
    {
        //랜덤스킬 생성, 현재레벨표시
    }
    private void SetCurSkills()
    {
        //현재 가진 스킬 종류만 표시
    }
}
