using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
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
    public TextMeshProUGUI coreQuentityText;


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

        goldQuantityText.text = accountInfo.gold.ToString();
        gemQuantityText.text = accountInfo.gem.ToString();
        coreQuentityText.text = accountInfo.core.ToString();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 뒤로 가기 버튼
            UI2Btn escapePopup = UIManager.Instance.ShowUI<UI2Btn>();
            escapePopup.SetPopup("게임을 종료 하시겠습니까?", () =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
            });
        }
    }

    private void UpdateHUD()
    {
        // TODO : 나중에 UI별 나누기
        SelectedStageId = GameManager.Instance.stageId;
        SetStageName();

        energyQuantityText.text = accountInfo.actionPoint.ToString();
        gemQuantityText.text = accountInfo.gem.ToString();
        goldQuantityText.text = accountInfo.gold.ToString();
        levelText.text = accountInfo.level.ToString();
        string path = "Textures/SnapShot/";
        switch (GameManager.Instance.accountInfo.selectedProfileIndex)
        {
            case 0:
                userProfileIcon.sprite = Resources.Load<Sprite>(path + "profile_mimic");
                break;
            case 1:
                userProfileIcon.sprite = Resources.Load<Sprite>(path + "profile_mushroom");
                break;
        }

        if (accountInfo.sliderMaxExp > 0)
        {
            expSlider.value = (float)accountInfo.sliderCurExp / accountInfo.sliderMaxExp;
        }
    }

    private void SetStageName()
    {
        StageNameText.text = DataManager.Instance.stageListDict[SelectedStageId].stageName;
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
        SoundManager.Instance.PlaySound("TabChangeUI_1", 1f);
    }

    public void OnClickStageSelect()
    {
        UIManager.Instance.ShowUI<UIStageSelect>();
        SoundManager.Instance.PlaySound("ButtonClickUI_1", 1f);
    }

    public void OnClickListButton()
    {
        UIManager.Instance.ShowUI<UIPropertiesList>();
        SoundManager.Instance.PlaySound("ButtonClickUI_1", 1f);
    }

    public void OnStartbutton()
    {
        SoundManager.Instance.PlaySound("ButtonClickUI_1", 1f);
        Debug.Log($"OnStartbutton : {SelectedStageId}");
        // 인게임으로 넘어가기
        if (accountInfo.actionPoint >= GameManager.Instance._combatActionPoint)
        {
            SoundManager.Instance.PlaySound("ConfirmUI_1", 1f);
            SceneManager.LoadScene(3); // 인게임씬
        }
        else
        {
            SoundManager.Instance.PlaySound("ErrorUI_1", 1f);
            UINoCurrency noActionPoint = UIManager.Instance.ShowUI<UINoCurrency>();
            noActionPoint.NoActionPoint();
        }
    }

    public void OnProfileImage()
    {
        UIChangeProfile popup = UIManager.Instance.ShowUI<UIChangeProfile>();
        SoundManager.Instance.PlaySound("ButtonClickUI_1");
        //popup.Init();

    }

    public void GoToShop()
    {
        //Shop으로 이동
        OnClickBottomButton(1);
    }


    public void ShowInfoUI(string info)
    {
        try
        {
            GameManager.Instance.infoGraphic = (InfoGraphic)System.Enum.Parse(typeof(InfoGraphic), info);
            UIManager.Instance.ShowUI<InfoOverlay>();
        }
        catch
        {
            Debug.LogError("UIError");
        }
    }

}
