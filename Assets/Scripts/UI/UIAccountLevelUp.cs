using TMPro;

public class UIAccountLevelUp : UIBase
{
    private InventoryUI inventoryUI;

    public TextMeshProUGUI levelText;
    public AccountLevelUpRewardSlotUI[] rewardSlotUI;

    private void OnEnable()
    {
        levelText.text = GameManager.Instance.accountInfo.level.ToString();
        inventoryUI = FindObjectOfType<InventoryUI>();
        inventoryUI.UpdateUI();
    }

    public void ClosePopUp()
    {
        gameObject.SetActive(false);
    }
}
