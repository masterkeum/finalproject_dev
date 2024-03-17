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


    private void Awake()
    {
        UIManager.Instance.ShowUI<UIBattle>();
    }
    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {

    }

   
}
