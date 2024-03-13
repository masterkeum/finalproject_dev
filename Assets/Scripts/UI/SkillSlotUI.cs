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
    [Header("LevelUp")]
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI skillDescriptionText;
    public Image skillType;


    private void Start()
    {
        SetIndex();
    }

    private void SetStars()
    {
        //for( int i = 0; i<level ; i++ )
        //{
        //    if(level == 0)
        //    {
        //        starSlot.Clear();
        //    }

        //    else if (starSlot[i].starSlotIndex > level)
        //    {
        //        starSlot[i].SetYellowStar();
        //    }
        //}
    }

    private void SetIndex()
    {
        _skillIndex = transform.GetSiblingIndex();
    }



}
