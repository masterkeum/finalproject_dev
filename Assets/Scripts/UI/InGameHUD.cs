using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameHUD : UIBase
{
    public TextMeshProUGUI killText;
    public TextMeshProUGUI goldText;
    public Slider expSlider;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI curLevelText;

    private Player player;

    private void Start()
    {
        SetWhenStart();
        UpdateWhenEnemyDie();
        UpdateWhenGetGold();
        UpdateWhenGetGem();
        player = GameManager.Instance.player;
        hpSlider = player.GetComponentInChildren<Slider>();
    }

    public void SetWhenStart()
    {
        expSlider.value = 0;
    }

    public void UpdateWhenGetGold()
    {
        //인게임 획득 골드 표시 업데이트
        goldText.text = player.gold.ToString();
    }
    public void UpdateWhenEnemyDie()
    {
        //킬카운트
        killText.text = player.killCount.ToString();
       
    }
    public void UpdateWhenGetGem()
    {
        // 인게임 경험치슬라이더,인게임 레벨 표시 업데이트
        if (expSlider.value > 0)
        {
            expSlider.value = player.sliderCurExp / player.sliderMaxExp;
        }

        curLevelText.text = player.curLevel.ToString();
    }

    public void Update()
    {

    }
    public void OnPauseButton()
    {
        UIManager.Instance.ShowUI<UIPause>();
    }
    public void OnTestLevelUpButton()
    {
        player.LevelUp();
    }


}
