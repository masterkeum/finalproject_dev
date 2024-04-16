using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameHUD : UIBase
{
    private Player player;

    public TextMeshProUGUI killText;
    public TextMeshProUGUI goldText;
    public Slider expSlider;
    public Slider hpSlider;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI curLevelText;

    private float timer = 0f;

    private void Awake()
    {
        SetWhenStart();
    }

    private void Start()
    {
        GameManager.Instance.updateUIAction += UpdateWhenEnemyDie;
        GameManager.Instance.updateUIAction += UpdateWhenGetGold;
        GameManager.Instance.updateUIAction += UpdateWhenGetGem;

        player = GameManager.Instance.player;
        //hpSlider = player.GetComponentInChildren<Slider>();

        SetWhenStart();
        UpdateWhenEnemyDie();
        UpdateWhenGetGold();
        UpdateWhenGetGem();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timeText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(timer / 60f), Mathf.FloorToInt(timer % 60f));
    }

    public void SetWhenStart()
    {
        expSlider.value = 0;
    }

    public void UpdateWhenGetGold()
    {
        //인게임 획득 골드 표시 업데이트
        goldText.text = player.playeringameinfo.gold.ToString();
    }
    public void UpdateWhenEnemyDie()
    {
        //킬카운트
        killText.text = player.playeringameinfo.killCount.ToString();
    }
    public void UpdateWhenGetGem()
    {
        // 인게임 경험치슬라이더,인게임 레벨 표시 업데이트
        //Debug.Log($"경험치 슬라이더 {player.playeringameinfo.sliderCurExp} / {player.playeringameinfo.sliderMaxExp}");
        if (player.playeringameinfo.sliderMaxExp > 0)
        {
            expSlider.value = (float)player.playeringameinfo.sliderCurExp / player.playeringameinfo.sliderMaxExp;
        }

        curLevelText.text = $"{player.playeringameinfo.curLevel}";
    }

    public void OnPauseButton()
    {
        UIManager.Instance.ShowUI<UIPause>();
        ++UIManager.Instance.popupUICount;
    }
}
