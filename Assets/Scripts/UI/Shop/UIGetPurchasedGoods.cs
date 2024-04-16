using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIGetPurchasedGoods : UIBase
{
    [SerializeField] private Image goodsIcon;
    [SerializeField] private TextMeshProUGUI goodsName;

    public void SetGoods(int packageId)
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
        goodsName.text = goods;


        string goodsImage = DataManager.Instance.shopDict[packageId].imageAddress;
        goodsIcon.sprite = Resources.Load<Sprite>(goodsImage);
        goodsIcon.SetNativeSize();
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
