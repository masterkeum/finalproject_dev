using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIDefeated : UIBase
{
    private GameObject player;
    private PlayerIngameData playerData;

    public GameObject LifeDown;
    public GameObject TimeOver;
    public TextMeshProUGUI KillCountText;
    public TextMeshProUGUI GetGoldText;

    private void Start()
    {
        playerData = player.GetComponent<PlayerIngameData>();
    }

    private void OnEnable()
    {
        //타임아웃으로 게임오버가 됐냐, 체력소진으로 게임오버가 됐냐에 따라 표시할 요소 결정
        KillCountText.text = playerData.killCount.ToString();
        GetGoldText.text = playerData.gold.ToString();
    }

    public void Resurrection()
    {

    }
    public void GiveUP()
    {
        SceneManager.LoadScene("MainScene");
        //현재 얻은 골드 등을 계정에 더하는 작업 todo
    }
}
