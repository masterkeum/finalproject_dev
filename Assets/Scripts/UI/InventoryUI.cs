using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("UISlots")]
    public ItemSlotUI weaponSlot; // = new ItemSlotUI();
    public ItemSlotUI helmetSlot; // = new ItemSlotUI();
    public ItemSlotUI gloveSlot; // = new ItemSlotUI();
    public ItemSlotUI bootsSlot; // = new ItemSlotUI();
    public ItemSlotUI armorSlot; // = new ItemSlotUI();
    public ItemSlotUI shieldSlot; // = new ItemSlotUI();

    private List<ItemTable> gachaTable = new List<ItemTable>();


    private void Start()
    {
        UpdateUI();
        SetMimicGacha();

    }

    private void UpdateUI()
    {
        //if (weaponSlot != null)
        //{
        //    string imagePath = GameManager.Instance.accountInfo.equipItems["Weapon"].ImageFile;
        //    Resources.Load(imagePath);

        //    weaponSlot.transform.GetChild(1).gameObject.SetActive(true);
        //    //weaponSlot.transform.GetChild(1).GetComponent<Image>().sprite = 

        //}
    }

    public void SetMimicGacha()
    {
        Dictionary<string, float> gradeWeights = new Dictionary<string, float>()
        {
            {"Normal", 40f},
            {"Magic", 30f},
            {"Elite", 15f},
            {"Rare",8f },
            {"Epic",5f},
            {"Legendary", 2f }
        };

        foreach (ItemTable newItem in DataManager.Instance.itemTableDict.Values)
        {
            if (newItem.itemCategory == "Equipment")
            {
                float weight = gradeWeights[newItem.grade];
                for (int i = 0; i < weight; i++)
                {
                    gachaTable.Add(newItem);
                }

            }
        }
       
    }

    public void StartMimicGacha()
    {
        int gachaNum = Random.Range(0, gachaTable.Count);
        GameManager.Instance.accountInfo.newItem = gachaTable[gachaNum];
    }

    public void GetANewItem()
    {
        switch (GameManager.Instance.accountInfo.newItem.itemType)
        {
            case "Weapon":
                    if (GameManager.Instance.accountInfo.equipItems["Weapon"] != null)
                        UIManager.Instance.ShowUI<UIEquipChange>();
                    else
                        UIManager.Instance.ShowUI<UINewEquip>();
                break;
            case "Armor":
                if (GameManager.Instance.accountInfo.equipItems["Armor"] != null)
                    UIManager.Instance.ShowUI<UIEquipChange>();
                else
                    UIManager.Instance.ShowUI<UINewEquip>();
                break;
            case "Helmet":
                if (GameManager.Instance.accountInfo.equipItems["Helmet"] != null)
                    UIManager.Instance.ShowUI<UIEquipChange>();
                else
                    UIManager.Instance.ShowUI<UINewEquip>();
                break;
            case "Gloves":
                if (GameManager.Instance.accountInfo.equipItems["Gloves"] != null)
                    UIManager.Instance.ShowUI<UIEquipChange>();
                else
                    UIManager.Instance.ShowUI<UINewEquip>();
                break;
            case "Boots":
                if (GameManager.Instance.accountInfo.equipItems["Boots"] != null)
                    UIManager.Instance.ShowUI<UIEquipChange>();
                else
                    UIManager.Instance.ShowUI<UINewEquip>();
                break;
            case "Accessarries":
                if (GameManager.Instance.accountInfo.equipItems["Accessarries"] != null)
                    UIManager.Instance.ShowUI<UIEquipChange>();
                else
                    UIManager.Instance.ShowUI<UINewEquip>();
                break;
        }
    }





}