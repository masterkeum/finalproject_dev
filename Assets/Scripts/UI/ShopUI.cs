using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.ReloadAttribute;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Scrollbar shopScroll;
    [SerializeField] private List<ShopSlotUI> goldSlotUI = new List<ShopSlotUI>();
    [SerializeField] private List<ShopSlotUI> dailySlotUI = new List<ShopSlotUI>();
    [SerializeField] private List<ShopSlotUI> gemSlotUI = new List<ShopSlotUI>();
    [SerializeField] private StarterPackageSlotUI starterPackage;
    [SerializeField] private ShopSlotUI deleteAdPackage;

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
            goldSlotUI[i].amount.text = GoodsName(40001000 + i);
            goldSlotUI[i].price.text = GoodsPrice(40001000 + i);
        }
        for (int i = 0; i < gemSlotUI.Count; i++)
        {
            gemSlotUI[i].amount.text = GoodsName(40000000 + i);
            gemSlotUI[i].price.text = GoodsPrice(40000000 + i);
        }

        dailySlotUI[0].amount.text = GoodsName(40003000);
        dailySlotUI[0].price.text = GoodsPrice(40003000);
        dailySlotUI[1].amount.text = GoodsName(40002000);
        dailySlotUI[1].price.text = GoodsPrice(40002000);

        string gem = DataManager.Instance.shopDict[40100001].amount1.ToString();
        string core = DataManager.Instance.shopDict[40100001].amount2.ToString();
        string timeticket = DataManager.Instance.shopDict[40100001].amount3.ToString();
        starterPackage.coreAmountText.text = core;
        starterPackage.timeTicketAmountText.text = timeticket;
        starterPackage.gemAmountText.text = gem;

    }

    
    public string GoodsName(int packageId)
    {
        string goods = DataManager.Instance.shopDict[packageId].amount1.ToString();
        switch (DataManager.Instance.shopDict[packageId].itemId1)
        {
            case 50000001:
                goods += "골드";
                break;
            case 50000002:
                goods += "젬";
                break;
            case 50000005:
                goods += "장";
                break;
            case 50000004:
                goods += "행동력";
                break;
        }
        return goods;
    }
    public string GoodsPrice(int packageId)
    {
        string price = DataManager.Instance.shopDict[packageId].price.ToString();
        switch (DataManager.Instance.shopDict[packageId].currencyID)
        {
            case 0:
                price += "원";
                break;
            case 50000001:
                price += "골드";
                break;
            case 50000002:
                price += "젬";
                break;
        }
        return price;
    }

    public void SelectGoods(int packageId)
    {
        UIPurchaseConfirm popup = UIManager.Instance.ShowUI<UIPurchaseConfirm>();
        popup.SetGoodsName(GoodsName(packageId));
        popup.selectedPackageId = packageId;
        SoundManager.Instance.PlaySound("ButtonClickUI_1");
    }

    public void SelectGoodsName(int packageId)
    {
        UIPurchaseConfirm popup = UIManager.Instance.ShowUI<UIPurchaseConfirm>();
        
    }

}
