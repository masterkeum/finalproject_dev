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
    
    public List<SkillSlotUI> activeSlotsUI = new List<SkillSlotUI>();
    public List<SkillSlotUI> passiveSlotsUI = new List<SkillSlotUI>();
    private PlayerIngameData player;

    private void Start()
    {
        player = GetComponent<PlayerIngameData>();
        SetActiveSlot();
        SetPassiveSlot();
    }

    public void SetActiveSlot()
    {
        for(int i = 0; i < player.activeSkillSlot.Count; i++)
        {
            if (player.activeSkillSlot[i] != null)
            {
                activeSlotsUI[i].skillIcon.gameObject.SetActive(true);
                activeSlotsUI[i].starGroup.gameObject.SetActive(true);
                //activeSlotsUI[i].skillSprite.sprite = ;
            }

        }
    }
    public void SetPassiveSlot()
    {
        for (int i = 0; i < player.passiveSkillSlot.Count; i++)
        {
            if (player.passiveSkillSlot[i] != null)
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
