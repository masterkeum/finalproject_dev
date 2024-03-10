using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutGameHUD : MonoBehaviour
{
    [Header("UserData")]
    public Slider expSlider;
    public Image userProfileIcon;
    public TextMeshPro levelText;
    public TextMeshPro userNameText;
    public TextMeshPro energyQuantityText;
    public TextMeshPro gemQuantityText;
    public TextMeshPro goldQuantityText;

    [Header("Battle")]
    public TextMeshPro StageNameText;
    public Image StageImage;

    [Header("Inventory")]
    public TextMeshPro userTotalAtkText;
    public TextMeshPro userTotalHpText;

    public void OnClickStageSelect()
    {
        UIManager.Instance.ShowUI<UIStageSelect>();
    }
    public void OnClickListButton()
    {
        UIManager.Instance.ShowUI<UIPropertiesList>();
    }

}
