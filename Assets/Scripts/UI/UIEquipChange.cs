using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipChange : UIBase
{
    public Image newIcon;
    public Image curIcon;
    public TextMeshProUGUI newItemNameText;
    public TextMeshProUGUI newItemDescriptionText;
    public TextMeshProUGUI curItemNameText;
    public TextMeshProUGUI curItemDescriptionText;
    public GameObject equipButton;
    public GameObject sellButton;

    private void Start()
    {
        Set();
    }

    public void Set()
    {

    }

    public void OnEquip()
    {

    }


    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }

}