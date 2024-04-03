using UnityEngine;

public class Cheat : MonoBehaviour
{
    private AccountInfo accountInfo;

    private void Start()
    {
        accountInfo = GameManager.Instance.accountInfo;
    }

    public void AddExpCheat(int value)
    {
        accountInfo.AddExp(value);
    }

    public void AddActionPointCheat(int value)
    {
        accountInfo.AddActionPoint(value);
    }

    public void AddGemCheat(int value)
    {
        accountInfo.AddGem(value);
    }

    public void AddGoldCheat(int value)
    {
        accountInfo.AddGold(value);
    }

}
