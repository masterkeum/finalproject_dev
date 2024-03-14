using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameHUD : MonoBehaviour
{
    public TextMeshProUGUI killText;
    public TextMeshProUGUI goldText;
    public Slider expSlider;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI curLevelText;

    private PlayerIngameData player;


    private void Start()
    {
        player = GetComponent<PlayerIngameData>();

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
        //킬카운트와 인게임 경험치슬라이더,인게임 레벨 표시 업데이트
        killText.text = player.killCount.ToString();
        expSlider.value = player.sliderCurExp / player.sliderMaxExp;
        curLevelText.text = player.curLevel.ToString();
    }
    public void OnPauseButton()
    {
        UIManager.Instance.ShowUI<UIPause>();
    }
}
