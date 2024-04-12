using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIDefeated : UIBase
{
    private Player player;

    public GameObject LifeDown;
    public GameObject TimeOver;
    public TextMeshProUGUI KillCountText;
    public TextMeshProUGUI GetGoldText;

    private void Awake()
    {
        player = GameManager.Instance.player;
    }

    private void OnEnable()
    {
        Debug.Log("UIDefeated");
        //타임아웃으로 게임오버가 됐냐, 체력소진으로 게임오버가 됐냐에 따라 표시할 요소 결정
        KillCountText.text = player.playeringameinfo.killCount.ToString();
        GetGoldText.text = player.playeringameinfo.gold.ToString();
    }

    public void Resurrection()
    {
        Debug.Log("부활했습니다.");
    }

    public void GiveUP()
    {
        SceneManager.LoadScene("MainScene");
    }
}
