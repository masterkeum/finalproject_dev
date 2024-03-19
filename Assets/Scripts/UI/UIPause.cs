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

    private PlayerIngameData playerData;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerData = player.GetComponent<PlayerIngameData>();
     
    }

    private void OnEnable()
    {
        SetActiveSlot();
        SetPassiveSlot();
    }

    public void SetActiveSlot()
    {
        if (playerData.activeSkillSlot != null)
        {
            for (int i = 0; i < playerData.activeSkillSlot.Count; i++)
            {

                activeSlotsUI[i].skillIcon.gameObject.SetActive(true);
                activeSlotsUI[i].starGroup.gameObject.SetActive(true);
                string path = playerData.activeSkillSlot[i].imageAddress;
                Sprite sprite = Resources.Load<Sprite>(path);
                activeSlotsUI[i].skillSprite.sprite = sprite;
                activeSlotsUI[i].starGroup.SetActive(true);

                activeSlotsUI[i].SetStars(playerData.activeSkillSlot[i].level);


            }

        }
    }
    public void SetPassiveSlot()
    {
        if (playerData.passiveSkillSlot != null)
        {
            for (int i = 0; i < playerData.passiveSkillSlot.Count; i++)
            {
                passiveSlotsUI[i].skillIcon.gameObject.SetActive(true);
                passiveSlotsUI[i].starGroup.gameObject.SetActive(true);
                string path = playerData.passiveSkillSlot[i].imageAddress;
                Sprite sprite = Resources.Load<Sprite>(path);
                passiveSlotsUI[i].skillSprite.sprite = sprite;
                passiveSlotsUI[i].starGroup.SetActive(true);

                passiveSlotsUI[i].SetStars(playerData.passiveSkillSlot[i].level);

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
