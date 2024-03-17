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
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI userNameText;
    public TextMeshProUGUI energyQuantityText;
    public TextMeshProUGUI gemQuantityText;
    public TextMeshProUGUI goldQuantityText;

    [Header("Battle")]
    public TextMeshProUGUI StageNameText;
    public Image StageImage;

    [Header("Inventory")]
    public TextMeshProUGUI userTotalAtkText;
    public TextMeshProUGUI userTotalDefText;
    public TextMeshProUGUI userTotalMoveSpeed;
    public TextMeshProUGUI userTotalAttackSpeed;



    public void OnClickStageSelect()
    {
        UIManager.Instance.ShowUI<UIStageSelect>();
    }
    public void OnClickListButton()
    {
        UIManager.Instance.ShowUI<UIPropertiesList>();
    }
    public void OnClickMimic()
    {
        UIManager.Instance.ShowUI<UIMimicGacha>();
    }
}
