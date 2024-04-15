using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPurchaseConfirm : UIBase
{
    public TextMeshProUGUI curGoods;
    public int selectedPackageId;
    private ShopUI shopUI;

    private void Start()
    {
        shopUI = FindAnyObjectByType<ShopUI>();
    }

    public void SetGoodsName(string goodsName)
    {
        curGoods.text = goodsName;
    }

    public void Confirm()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        if (selectedPackageId > 0)
        {
            switch (DataManager.Instance.shopDict[selectedPackageId].currencyID)
            {
                case 0:
                    {
                        if (DataManager.Instance.shopDict[selectedPackageId].currencyID == 0)
                        {
                            if (selectedPackageId == 40100000)
                            {
                                accountInfo.totalCost += DataManager.Instance.shopDict[selectedPackageId].price;
                                accountInfo.isPurchasedAdDeletePack = true;
                            }
                            else if (selectedPackageId == 40100001)
                            {
                                accountInfo.totalCost += DataManager.Instance.shopDict[selectedPackageId].price;
                                accountInfo.AddGem(DataManager.Instance.shopDict[selectedPackageId].amount1);
                                accountInfo.AddCore((int)DataManager.Instance.shopDict[selectedPackageId].amount2);
                                accountInfo.AddTimeTicket((int)DataManager.Instance.shopDict[selectedPackageId].amount3);
                                accountInfo.isPurchasedStarterPack = true;
                                
                            }
                            else
                            {
                                for (int i = 0; i < 40000006; i++)
                                {
                                    if (selectedPackageId == 40000000 + i)
                                    {
                                        accountInfo.totalCost += DataManager.Instance.shopDict[40000000+i].price;
                                        accountInfo.AddGem(DataManager.Instance.shopDict[40000000 + i].amount1);
                                    }
                                }
                            }

                            SoundManager.Instance.PlaySound("PurchaseUI_1");

                        }
                    }
                    break;
                case 50000002: //젬 결제일 경우(현재 모두 젬 결제)
                    {
                        if (accountInfo.gem >= DataManager.Instance.shopDict[selectedPackageId].price)
                        {
                            accountInfo.gem -= DataManager.Instance.shopDict[selectedPackageId].price;
                            switch (DataManager.Instance.shopDict[selectedPackageId].itemType1)
                            {
                                case ItemType.Gold:
                                    accountInfo.AddGold(DataManager.Instance.shopDict[selectedPackageId].amount1);
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
        shopUI.SetSlots();
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
