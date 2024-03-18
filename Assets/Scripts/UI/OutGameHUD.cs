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

    [Header("BottomButton")]
    public GameObject[] mainMenues;
    public RectTransform[] buttonRect;

    [Header("Battle")]
    public TextMeshProUGUI StageNameText;
    public Image StageImage;


    private void Start()
    {
        OnClickBottomButton(0);
    }
    public void OnClickBottomButton(int index)
    {
        for (int i = 0; i < mainMenues.Length; i++)
        {
            mainMenues[i].SetActive(false);
        }
        mainMenues[index].SetActive(true);
        for (int j  = 0; j < mainMenues.Length; j++)
        {
            buttonRect[j].sizeDelta = new Vector2(index == j ? 450 : 300, buttonRect[j].sizeDelta.y);
            buttonRect[0].pivot = new Vector2(index ==2? 1.5f : 1, buttonRect[0].pivot.y);
        }
    }

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
