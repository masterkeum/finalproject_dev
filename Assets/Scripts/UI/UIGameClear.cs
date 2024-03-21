using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameClear : UIBase
{
    private GameObject player;
    private PlayerIngameData playerData;

    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI getGoldText;
    public TextMeshProUGUI getGemText;

    private void Start()
    {
        playerData = player.GetComponent<PlayerIngameData>();
        
    }

    private void Update()
    {
        killCountText.text = playerData.killCount.ToString();
        getGoldText.text = playerData.gold.ToString();
        //getGemText.text 플레이 시간에 비례해서 제공?
    }

    public void GetDoubleReward()
    {
        Debug.Log("두 배 보상");
    }

    public void GetCommonReward()
    {
        Debug.Log("일반 보상");
    }
    
    

}
