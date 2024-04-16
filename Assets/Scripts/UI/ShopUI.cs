using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private StarterPackageSlotUI adDeletePackage;
    [SerializeField] private StarterPackageSlotUI starterPackage;
    [SerializeField] private GameObject starterPackageButton;
    [SerializeField] private GameObject adDeletePackageButton;
    [SerializeField] private TextMeshProUGUI totalcostText;
    

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
        totalcostText.text = GameManager.Instance.accountInfo.totalCost.ToString("N0") + "원";
        
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

        if(GameManager.Instance.accountInfo.isPurchasedStarterPack == true)
        {
            starterPackageButton.SetActive(false);
        }
        else
        {
            starterPackageButton.SetActive(true);
            string gem = DataManager.Instance.shopDict[40100001].amount1.ToString("N0");
            string core = DataManager.Instance.shopDict[40100001].amount2.ToString();
            string timeticket = DataManager.Instance.shopDict[40100001].amount3.ToString();
            starterPackage.coreAmountText.text = " X " + core;
            starterPackage.timeTicketAmountText.text = " X " + timeticket;
            starterPackage.gemAmountText.text = " X " + gem;
        }
        if (GameManager.Instance.accountInfo.isPurchasedAdDeletePack == true)
            adDeletePackageButton.SetActive(false);
        else
        {
            adDeletePackageButton.SetActive(true);
            adDeletePackage.priceText.text = DataManager.Instance.shopDict[40100000].price.ToString("N0")+"원";

        }
    }

    
    public string GoodsName(int packageId)
    {
        string goods = DataManager.Instance.shopDict[packageId].amount1.ToString("N0");
        switch (DataManager.Instance.shopDict[packageId].itemId1)
        {
            case 50000001:
                goods += "골드";
                break;
            case 50000002:
                goods += "젬";
                break;
            case 50000005:
                goods += "타임티켓";
                break;
            case 50000004:
                goods += "행동력";
                break;
        }
        return goods;
    }
    public string GoodsPrice(int packageId)
    {
        string price = DataManager.Instance.shopDict[packageId].price.ToString("N0");
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

    public void SelectStarterPack(int packageId)
    {
        UIPurchaseConfirm popup = UIManager.Instance.ShowUI<UIPurchaseConfirm>();
        popup.SetGoodsName("초보자 패키지");
        popup.selectedPackageId = packageId;
        SoundManager.Instance.PlaySound("ButtonClickUI_1");
    }
    public void SelectAdDeletePack(int packageId)
    {
        UIPurchaseConfirm popup = UIManager.Instance.ShowUI<UIPurchaseConfirm>();
        popup.SetGoodsName("영구적 광고제거");
        popup.selectedPackageId = packageId;
        SoundManager.Instance.PlaySound("ButtonClickUI_1");
    }

}
