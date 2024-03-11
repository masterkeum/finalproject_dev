using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    public List<StarUI> starSlot = new List<StarUI>();
    public GameObject skillIcon;
    public Image skillSprite;
    public GameObject starGroup;
    private int _skillIndex;

    private void Start()
    {
        SetIndex();
    }

    private void SetIndex()
    {
        _skillIndex = transform.GetSiblingIndex();
    }


}
