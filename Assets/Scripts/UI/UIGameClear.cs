using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameClear : UIBase
{
    private Player player;

    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI getGoldText;
    public TextMeshProUGUI getGemText;

    private void Start()
    {
        player = GameManager.Instance.player;
    }

    private void Update()
    {
        killCountText.text = player.playeringameinfo.killCount.ToString();
        getGoldText.text = player.playeringameinfo.gold.ToString();
        //getGemText.text 플레이 시간에 비례해서 제공?
    }

    public void GetDoubleReward()
    {
        Debug.Log("GetDoubleReward");
    }

    public void GetCommonReward()
    {
        Debug.Log("GetCommonReward");
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }



}
