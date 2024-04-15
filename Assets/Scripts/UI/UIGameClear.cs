using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameClear : UIBase
{
    public Player player;

    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI getGoldText;
    public TextMeshProUGUI getGemText;
    public TextMeshProUGUI getNormalJS;
    public TextMeshProUGUI getHeroJS;
    public ItemTable itemInfos;

    private void OnEnable()
    {
        player = GameManager.Instance.player;
    }

    private void Update()
    {
        killCountText.text = player.playeringameinfo.killCount.ToString();
        getGoldText.text = player.playeringameinfo.gold.ToString();
        getGemText.text = player.playeringameinfo.gem.ToString();
        getNormalJS.text = player.playeringameinfo.core.ToString();
        //getGemText.text 플레이 시간에 비례해서 제공?
    }

    public void GetDoubleReward()
    {

        // player.DoubleReward();
        // SceneManager.LoadScene(2);

        Debug.Log("GetDoubleReward");
        var adPopup = UIManager.Instance.ShowUI<UIAdsPage>();
        ++UIManager.Instance.popupUICount;
        adPopup.Init(AdsStates.Clear);
        var killCount = player.playeringameinfo.killCount;
        var gold = player.playeringameinfo.gold;
        var gem = player.playeringameinfo.gem;
        var core = player.playeringameinfo.core;
        adPopup.GameClear(gold, gem, core);

    }

    public void GetCommonReward()
    {
        Debug.Log("GetCommonReward");
        SceneManager.LoadScene(2);
    }

}
