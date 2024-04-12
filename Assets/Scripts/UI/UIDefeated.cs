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

    private AccountInfo accountInfo;
    
    private void Awake()
    {
        player = GameManager.Instance.player;
    }

    private void Start()
    {
        accountInfo.AddGold(player.playeringameinfo.gold); // 획득 골드 account info 에 저장
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
        //현재 얻은 골드 등을 계정에 더하는 작업 todo
    }
}
