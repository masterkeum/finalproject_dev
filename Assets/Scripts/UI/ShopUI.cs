using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
            goldSlotUI[i].amount.text = GoodsName(40001000 + i);
            goldSlotUI[i].price.text = GoodsPrice(40001000 + i);
        }

        dailySlotUI[0].amount.text = GoodsName(40003000);
        dailySlotUI[0].price.text = GoodsPrice(40003000);
        dailySlotUI[1].amount.text = GoodsName(40002000);
        dailySlotUI[1].price.text = GoodsPrice(40002000);

    }

    public string GoodsName(int packageId)
    {
        string goods = DataManager.Instance.shopDict[packageId].amount1.ToString();
        switch (DataManager.Instance.shopDict[packageId].itemId1)
        {
            case 50000001:
                goods += "골드";
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


}
