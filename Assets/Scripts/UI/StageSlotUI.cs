using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSlotUI : MonoBehaviour
{
    [SerializeField] private Image _stageBanner;
    [SerializeField] private GameObject _selectMark;

    public int index;

    public bool isCurStage;

    private void Start()
    {
        SetIndex();
    }
    public void UpdateMark()
    {
        _selectMark.SetActive(isCurStage);
    }

    private void SetIndex()
    {
        index = transform.GetSiblingIndex();
    }
}
