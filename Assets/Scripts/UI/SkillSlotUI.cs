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
    public GameObject skillLock;

    [Header("LevelUp")]
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI skillDescriptionText;
    public Image skillType;
    public GameObject activeIcon;
    public GameObject passiveIcon;
    public int skillGroupId;

    [Header("Awaken")]
    public GameObject possibleAwakeGO;
    public SkillSlotUI possibleAwakeIcon;

    private void Start()
    {
        SetIndex();
    }

    public void SetStars(int level, bool up = false)
    {
        //Debug.Log("level : " + level);
        for (int i = 0; i < level; i++)
        {
            starSlot[i].SetYellowStar();
        }

        if (up)
        {
            //level 가 5로 들어오면 각성
            if (level < 5)
            {
                starSlot[level].SetYellowStar(0.5f);
            }
            else
            {
                starSlot[0].SetYellowStar(0.5f);
                starSlot[1].SetYellowStar(0.5f);
                starSlot[2].SetYellowStar(0.5f);
                starSlot[3].SetYellowStar(0.5f);
                starSlot[4].SetYellowStar(0.5f);
            }
        }
    }

    public void ClearStars()
    {
        for (int i = 0; i < starSlot.Count; i++)
        {
            starSlot[i].ClearYellowStar();
        }
    }

    private void SetIndex()
    {
        _skillIndex = transform.GetSiblingIndex();
    }



}
