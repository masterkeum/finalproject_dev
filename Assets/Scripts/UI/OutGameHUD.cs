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

    [Header("MainMenu")]
    public GameObject[] mainMenu;

    [Header("Bottom")]
    public RectTransform[] buttonRect;


    private void Awake()
    {
        ChangeMenu(0);
    }
    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {

    }

    public void ChangeMenu(int index)
    {
        for(int i = 0; i < mainMenu.Length; i++)
        {
            if (i == index)
            {
                mainMenu[i].SetActive(true);
            }
            else
            {
                mainMenu[i].SetActive(false);
            }

        }
        for (int i = 0; i < buttonRect.Length; i++)
        {
            buttonRect[i].sizeDelta = new Vector2(i == index ? 450 : 300, buttonRect[i].sizeDelta.y);
        }
        buttonRect[0].pivot = new Vector2(index ==2? 1.5f : 1, 0);

    }


}
