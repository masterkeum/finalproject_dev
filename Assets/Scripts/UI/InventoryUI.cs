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
        UIManager.Instance.ShowUI<UIMimicGacha>();
        GameManager.Instance.SaveGame();
    }







}