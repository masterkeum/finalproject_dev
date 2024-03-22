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

    private Player player;
    private void Awake()
    {
        player = GameManager.Instance.player;
        for (int j = player.CurrentOpenSkillSlotCount(); j < 6; j++)
        {
            activeSlotsUI[j].skilllock.SetActive(true);
            passiveSlotsUI[j].skilllock.SetActive(true);
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
                string path = player.activeSkillSlot[i].imageAddress;
                Sprite sprite = Resources.Load<Sprite>(path);
                activeSlotsUI[i].skillSprite.sprite = sprite;
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
                string path = player.passiveSkillSlot[i].imageAddress;
                Sprite sprite = Resources.Load<Sprite>(path);
                passiveSlotsUI[i].skillSprite.sprite = sprite;
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
