using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemDescription : UIBase
{
    public Image icon;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemStatNameText;
    public TextMeshProUGUI itemStatValueText;
    public GameObject equipButton;
    public GameObject unEquipButton;

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
    public void OnUnEquip()
    {

    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }

}
