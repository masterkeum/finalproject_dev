using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Scrollbar shopScroll;
    [SerializeField] private List<ShopSlotUI> goldSlotUI = new List<ShopSlotUI>();
    [SerializeField] private List<ShopSlotUI> dailySlotUI = new List<ShopSlotUI>();

    private void OnEnable()
    {
        shopScroll.value = 1;
    }
    void Start()
    {
        SetSlots();
    }

    public void SetSlots()
    {
        for (int i = 0; i < goldSlotUI.Count; i++)
        {
            goldSlotUI[i].amount.text = DataManager.Instance.shopDict[40001000 + i].amount1 + "골드";
            goldSlotUI[i].price.text = DataManager.Instance.shopDict[40001000 + i].price + "젬";
        }

        dailySlotUI[0].amount.text = DataManager.Instance.shopDict[40003000].amount1 + "장";
        dailySlotUI[0].price.text = DataManager.Instance.shopDict[40003000].price + "젬";
        dailySlotUI[1].amount.text = DataManager.Instance.shopDict[40002000].amount1 + "행동력";
        dailySlotUI[1].price.text = DataManager.Instance.shopDict[40002000].price + "젬";

    }

    public void PurchaseGold(int index)
    {
        Dictionary<int,Shop> shopDict = DataManager.Instance.shopDict;
        Purchase(ItemType.Gem, shopDict[40001000 + index].price,shopDict[40001000 + index].itemType1 , shopDict[40001000+index].amount1);
        GameManager.Instance.UpdateUI();
    }

    public void PerchaseTicket()
    {
        Dictionary<int, Shop> shopDict = DataManager.Instance.shopDict;
        Purchase(ItemType.Gem, shopDict[40003000].price, ItemType.Timeticket, shopDict[40003000].amount1);
        GameManager.Instance.UpdateUI();
    }

    public void PurchaseEnergy()
    {
        Dictionary<int, Shop> shopDict = DataManager.Instance.shopDict;
        Purchase(ItemType.Gem, shopDict[40002000].price, ItemType.Energy, shopDict[40002000].amount1 );
        GameManager.Instance.UpdateUI();
    }

    public void Purchase(ItemType input, int price, ItemType output, int amount)
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        switch (input)
        {
            case ItemType.Gold:
                {
                    if (accountInfo.gold >= price)
                    {

                    }
                    else
                    {
                        UINoCurrency popup = UIManager.Instance.ShowUI<UINoCurrency>();
                        popup.NoGold();
                    }
                }
                break;
            case ItemType.Gem:
                {
                    if (accountInfo.gem >= price)
                    {
                        accountInfo.gem -= price;
                        switch (output)
                        {
                            case ItemType.Gold:
                                accountInfo.gold += amount;
                                break;
                            case ItemType.Timeticket:
                                break;
                            case ItemType.Energy:
                                accountInfo.actionPoint += amount;
                                break;
                        }
                    }
                    else
                    {
                        UINoCurrency popup = UIManager.Instance.ShowUI<UINoCurrency>();
                        popup.NoGem();
                    }
                }
                break;
        }
    }

}
