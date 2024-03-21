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

    private GameObject player;
    private PlayerIngameData playerData;

    private void Start()
    {
        player = GameObject.Find("Player(Clone)");
        playerData = player.GetComponent<PlayerIngameData>();
        hpSlider = player.GetComponentInChildren<Slider>();
        SetWhenStart();
        UpdateWhenEnemyDie();
        UpdateWhenGetGold();
        UpdateWhenGetGem();
    }

    public void SetWhenStart()
    {
        expSlider.value = 0;
    }

    public void UpdateWhenGetGold()
    {
        //인게임 획득 골드 표시 업데이트
        goldText.text = playerData.gold.ToString();
    }
    public void UpdateWhenEnemyDie()
    {
        //킬카운트
        killText.text = playerData.killCount.ToString();
       
    }
    public void UpdateWhenGetGem()
    {
        // 인게임 경험치슬라이더,인게임 레벨 표시 업데이트
        if (expSlider.value > 0)
        {
            expSlider.value = playerData.sliderCurExp / playerData.sliderMaxExp;
        }

        curLevelText.text = playerData.curLevel.ToString();
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
        playerData.LevelUp();
    }


}
