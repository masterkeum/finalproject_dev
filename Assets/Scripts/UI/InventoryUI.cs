using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;




public class InventoryUI : MonoBehaviour
{
    [Serializable]
    public class ItemOptions
    {

    }

    [Header("UISlots")]
    public ItemSlotUI weaponSlot; // = new ItemSlotUI();
    public ItemSlotUI helmetSlot; // = new ItemSlotUI();
    public ItemSlotUI gloveSlot; // = new ItemSlotUI();
    public ItemSlotUI bootsSlot; // = new ItemSlotUI();
    public ItemSlotUI armorSlot; // = new ItemSlotUI();
    public ItemSlotUI shieldSlot; // = new ItemSlotUI();

    private ItemTable selectedItem = new ItemTable();

    private float normal;
    private float magic;
    private float elite;
    private float rare;
    private float epic;
    private float legendary;


    private void Start()
    {
        UpdateUI();


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
        int playerLevel = GameManager.Instance.accountInfo.level;
        normal = DataManager.Instance.GetLevelGacha(playerLevel).normal;
        magic = DataManager.Instance.GetLevelGacha(playerLevel).magic;
        elite = DataManager.Instance.GetLevelGacha(playerLevel).elite;
        rare = DataManager.Instance.GetLevelGacha(playerLevel).rare;
        epic = DataManager.Instance.GetLevelGacha(playerLevel).Epic;
        legendary = DataManager.Instance.GetLevelGacha(playerLevel).Legendary;

        System.Random random = new System.Random();

        int num = random.Next(0, 101);
        float[] probs = { legendary, epic, rare, elite, magic, normal };
        string grade=null;

        float cumulative = 0f;
        int target = -1;
        for (int i = 0; i < probs.Length; i++)
        {
            cumulative += probs[i];
            if (num <= cumulative)
            {
                target = 5 - i;
                break;
            }
        }

        switch (target)
        {
            case 0:
                grade = "Normal";
                break;
            case 1:
                grade = "Magic";
                break;
            case 2:
                grade = "Elite";
                break;
            case 3:
                grade = "Rare";
                break;
            case 4:
                grade = "Epic";
                break;
            case 5:
                grade = "Legendary";
                break;
        }

        Array enumValues = Enum.GetValues(typeof(ItemType));
        int type = random.Next(enumValues.Length);

        ItemType randomtype = (ItemType)enumValues.GetValue(type);


        List<ItemTable> items = DataManager.Instance.itemTableDict.Values.ToList();
        List<ItemTable> selectedGrade = items.FindAll((item) => { return item.grade == grade; });
        ItemTable selectedType = selectedGrade.Find((Item) => { return Item.itemType == randomtype; });











        //Dictionary<string, float> gradeWeights = new Dictionary<string, float>()
        //{
        //    {"Normal", normal},
        //    {"Magic", magic},
        //    {"Elite", elite},
        //    {"Rare", rare},
        //    {"Epic", epic},
        //    {"Legendary", legendary}
        //};

        //foreach (ItemTable newItem in DataManager.Instance.itemTableDict.Values)
        //{
        //    if (newItem.itemCategory == "Equipment")
        //    {
        //        float weight = gradeWeights[newItem.grade];
        //        for (int i = 0; i < weight; i++)
        //        {
        //            gachaTable.Add(newItem);
        //        }

        //    }
        //}

    }

    private void SetSelectedItemStats()
    {

        //switch(GameManager.Instance.accountInfo.newItem.grade)
        //{
        //    case "Normal":
        //        {

        //        }
        //}
    }

    public void StartMimicGacha()
    {
        SetMimicGacha();

        //int gachaNum = UnityEngine.Random.Range(0, gachaTable.Count);
        //GameManager.Instance.accountInfo.newItem = gachaTable[gachaNum];

        SetSelectedItemStats();

        UIManager.Instance.ShowUI<UIMimicGacha>();
        GameManager.Instance.SaveGame();
    }







}