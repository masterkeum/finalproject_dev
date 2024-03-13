using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillSlot
{
    public int level;
}
public class UIPause : UIBase
{
    public TextMeshProUGUI pauseText;
    
    public List<SkillSlot> activeSlots = new List<SkillSlot>();
    public List<SkillSlot> passiveSlots = new List<SkillSlot>();
    public List<SkillSlotUI> activeSlotsUI = new List<SkillSlotUI>();
    public List<SkillSlotUI> passiveSlotsUI = new List<SkillSlotUI>();  // 매니저로 옮겨놓을 것들
    

    private void Start()
    {
        SetActiveSlot();
        SetPassiveSlot();
    }

    public void SetActiveSlot()
    {
        for(int i = 0; i < activeSlots.Count; i++)
        {
            if (activeSlots[i] != null)
            {
                activeSlotsUI[i].skillIcon.gameObject.SetActive(true);
                activeSlotsUI[i].starGroup.gameObject.SetActive(true);
                //activeSlotsUI[i].skillSprite.sprite = ;
            }

        }
    }
    public void SetPassiveSlot()
    {
        for (int i = 0; i < passiveSlots.Count; i++)
        {
            if (passiveSlots[i] != null)
            {
                passiveSlotsUI[i].skillIcon.gameObject.SetActive(true);
                passiveSlotsUI[i].starGroup.gameObject.SetActive(true);
                //passiveSlotsUI[i].skillSprite.sprite = ;
            }
        }
    }

    public void GoToMainButton()
    {

    }
    public void ContinueButton()
    {
        gameObject.SetActive(false);
    }
}
