using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    [Header("General")]
    public List<StarUI> starSlot = new List<StarUI>();
    public GameObject skillIcon;
    public Image skillSprite;
    public GameObject starGroup;
    private int _skillIndex;
    public GameObject skilllock;
    [Header("LevelUp")]
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI skillDescriptionText;
    public Image skillType;


    private void Start()
    {
        SetIndex();
    }

    public void SetStars(int level)
    {
        for (int i = 0; i < level; i++)
        {
            if (level == 0)
            {
                starSlot[i].ClearYellowStar();
            }

            else
            {
                starSlot[i].SetYellowStar();
            }
        }
    }

    public void ClearStars()
    {
        for(int i = 0; i < starSlot.Count; i++)
        {
            starSlot[i].ClearYellowStar();
        }
    }

    private void SetIndex()
    {
        _skillIndex = transform.GetSiblingIndex();
    }



}
