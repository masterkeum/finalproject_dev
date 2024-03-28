using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OutGameHUD : UIBase
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
    public GameObject[] selectEmphasis;

    [Header("Battle")]
    public TextMeshProUGUI StageNameText;
    public Image StageImage;
    public int SelectedStageId;

    private AccountInfo accountInfo;

    private void Awake()
    {
        SelectedStageId = GameManager.Instance.stageId;
    }

    private void Start()
    {
        GameManager.Instance.updateUIAction += UpdateHUD;
        accountInfo = GameManager.Instance.accountInfo;

        userNameText.text = accountInfo.name;

        UpdateHUD();
        OnClickBottomButton(0);
    }

    private void UpdateHUD()
    {
        // TODO : 나중에 UI별 나누기
        SelectedStageId = GameManager.Instance.stageId;

        energyQuantityText.text = accountInfo.actionPoint.ToString();
        gemQuantityText.text = accountInfo.gem.ToString();
        goldQuantityText.text = accountInfo.gold.ToString();
        levelText.text = accountInfo.level.ToString();

        if (accountInfo.sliderMaxExp > 0)
        {
            expSlider.value = (float)accountInfo.sliderCurExp / accountInfo.sliderMaxExp;
        }
    }

    public void OnClickBottomButton(int index)
    {
        for (int i = 0; i < mainMenues.Length; i++)
        {
            mainMenues[i].SetActive(false);
        }
        mainMenues[index].SetActive(true);
        for (int j = 0; j < mainMenues.Length; j++)
        {
            buttonRect[j].sizeDelta = new Vector2(index == j ? 450 : 300, buttonRect[j].sizeDelta.y);
            buttonRect[0].pivot = new Vector2(index == 2 ? 1.5f : 1, buttonRect[0].pivot.y);

            selectEmphasis[j].SetActive(index == j ? true : false);
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
    public void OnStartbutton()
    {
        Debug.Log($"OnStartbutton : {SelectedStageId}");
        // 인게임으로 넘어가기

        SceneManager.LoadScene(3); // 인게임씬
    }

}
