using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPurchaseConfirm : UIBase
{
    public TextMeshProUGUI curGoods;
    public int selectedPackageId;

    private void OnEnable()
    {
        
    }

    public void SetGoodsName(string goodsName)
    {
        curGoods.text = goodsName;
    }

    public void Confirm()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        if(selectedPackageId >0)
        {
            switch(DataManager.Instance.shopDict[selectedPackageId].currencyID)
            {
                case 50000002: //젬 결제일 경우(현재 모두 젬 결제)
                    {
                        if(accountInfo.gem >= DataManager.Instance.shopDict[selectedPackageId].price)
                        {
                            accountInfo.gem -= DataManager.Instance.shopDict[selectedPackageId].price;
                            switch(DataManager.Instance.shopDict[selectedPackageId].itemType1)
                            {
                                case ItemType.Gold:
                                    accountInfo.AddGold( DataManager.Instance.shopDict[selectedPackageId].amount1);
                                    break;
                                case ItemType.Timeticket:
                                    accountInfo.AddTimeTicket(DataManager.Instance.shopDict[selectedPackageId].amount1);
                                    break;
                                case ItemType.Energy:
                                    accountInfo.actionPoint += DataManager.Instance.shopDict[selectedPackageId].amount1;
                                    break;
                            }
                            SoundManager.Instance.PlaySound("PurchaseUI_1");
                        }
                        else
                        {
                            UINoCurrency popup = UIManager.Instance.ShowUI<UINoCurrency>();
                            popup.NoGem();
                            SoundManager.Instance.PlaySound("ErrorUI_1");
                        }
                    }
                    break;
            }
        }
       
        GameManager.Instance.UpdateUI();
        selectedPackageId = 0;
        gameObject.SetActive(false);
        SoundManager.Instance.PlaySound("ButtonClickUI_1");
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
        selectedPackageId = 0;
        SoundManager.Instance.PlaySound("ButtonClickUI_1");
    }
}
