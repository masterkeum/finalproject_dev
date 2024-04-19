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

    private Player player;
    private void Awake()
    {
        player = GameManager.Instance.player;
        for (int i = player.CurrentOpenSkillSlotCount(SkillApplyType.Active); i < 6; i++)
        {
            activeSlotsUI[i].skillLock.SetActive(true);
        }
        for (int j = player.CurrentOpenSkillSlotCount(SkillApplyType.Passive); j < 6; j++)
        {
            passiveSlotsUI[j].skillLock.SetActive(true);
        }
    }

    private void OnEnable()
    {
        SetActiveSlot();
        SetPassiveSlot();
    }

    public void SetActiveSlot()
    {
        if (player.activeSkillSlot != null)
        {

            for (int i = 0; i < player.activeSkillSlot.Count; i++)
            {
                activeSlotsUI[i].skillIcon.gameObject.SetActive(true);
                activeSlotsUI[i].starGroup.gameObject.SetActive(true);
                activeSlotsUI[i].skillSprite.sprite = Resources.Load<Sprite>(player.activeSkillSlot[i].imageAddress);
                activeSlotsUI[i].starGroup.SetActive(true);

                activeSlotsUI[i].SetStars(player.activeSkillSlot[i].level);
            }
        }
    }

    public void SetPassiveSlot()
    {
        if (player.passiveSkillSlot != null)
        {
            for (int i = 0; i < player.passiveSkillSlot.Count; i++)
            {
                passiveSlotsUI[i].skillIcon.gameObject.SetActive(true);
                passiveSlotsUI[i].starGroup.gameObject.SetActive(true);
                passiveSlotsUI[i].skillSprite.sprite = Resources.Load<Sprite>(player.passiveSkillSlot[i].imageAddress);
                passiveSlotsUI[i].starGroup.SetActive(true);

                passiveSlotsUI[i].SetStars(player.passiveSkillSlot[i].level);
            }
        }
    }

    public void GoToMainButton()
    {
        UIManager.Instance.ShowUI<UIGoToMainWarning>();
    }
    public void ContinueButton()
    {
        --UIManager.Instance.popupUICount;
        gameObject.SetActive(false);
    }
}
