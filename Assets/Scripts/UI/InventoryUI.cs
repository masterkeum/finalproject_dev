using System;
using System.Collections.Generic;
using System.Linq;
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

    private Item selectedItem = new Item();

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
        ItemGrade grade=ItemGrade.Normal;

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
                grade = ItemGrade.Normal;
                break;
            case 1:
                grade = ItemGrade.Magic;
                break;
            case 2:
                grade = ItemGrade.Elite;
                break;
            case 3:
                grade = ItemGrade.Rare;
                break;
            case 4:
                grade = ItemGrade.Epic;
                break;
            case 5:
                grade = ItemGrade.Legendary;
                break;
        }

        Array enumValues = Enum.GetValues(typeof(ItemType));
        int type = random.Next(enumValues.Length);

        ItemType randomtype = (ItemType)enumValues.GetValue(type);

        List<ItemTable> items = DataManager.Instance.itemTableDict.Values.ToList();
        List<ItemTable> selectedGrade = items.FindAll((item) => {return item.grade == grade; });
        ItemTable selectedGradeNType = selectedGrade.Find((Item) => {return Item.itemType == randomtype; });

        selectedItem.itemId = selectedGradeNType.itemId;
        selectedItem.itemCategory = selectedGradeNType.itemCategory;
        selectedItem.itemType = selectedGradeNType.itemType;
        selectedItem.nameAlias = selectedGradeNType.nameAlias;
        selectedItem.grade = selectedGradeNType.grade;
        selectedItem.ImageFile = selectedGradeNType.imageFile;

        List<ItemOptions> options = new List<ItemOptions>();
        int maxRange = 4;
        switch(selectedItem.grade)
        {
            case ItemGrade.Normal:
            case ItemGrade.Magic:
            case ItemGrade.Elite:
                maxRange = 4;
                break;
            case ItemGrade.Rare:
            case ItemGrade.Epic:
                maxRange = 5;
                break;
            case ItemGrade.Legendary:
                maxRange = 6;
                break;
        }

        while(options.Count <= (int)selectedItem.grade)
        {
            ItemOptions randomOption = (ItemOptions)UnityEngine.Random.Range(0, maxRange);
            if (!options.Contains(randomOption))
            {
                options.Add(randomOption);
            }
        }

        foreach(ItemOptions randomOption in options)
        {
            switch (randomOption)
            {
                case ItemOptions.Hp:
                    selectedItem.Hp = (UnityEngine.Random.Range(selectedGradeNType.minHp, selectedGradeNType.maxHp + 1));
                    break;
                case ItemOptions.Dp:
                    selectedItem.Dp = (UnityEngine.Random.Range(selectedGradeNType.minDp, selectedGradeNType.maxDp + 1));
                    break;
                case ItemOptions.Ap:
                    selectedItem.Ap = (UnityEngine.Random.Range(selectedGradeNType.minAp, selectedGradeNType.maxAp + 1));
                    break;
                case ItemOptions.MoveSpeed:
                    selectedItem.MoveSpeed = (UnityEngine.Random.Range(selectedGradeNType.minMovespeed, selectedGradeNType.maxMovespeed));
                    break;
                case ItemOptions.CriticalHit:
                    selectedItem.CriticalHit = (UnityEngine.Random.Range(selectedGradeNType.minCriticalHit, selectedGradeNType.maxCriticalHit));
                    break;
                case ItemOptions.HpGen:
                    selectedItem.HpGen = (UnityEngine.Random.Range(selectedGradeNType.minHpGen, selectedGradeNType.maxHpGen + 1));
                    break;
            }
        }

    }


    public void StartMimicGacha()
    {
        SetMimicGacha();
        GameManager.Instance.accountInfo.newItem = selectedItem;

        UIManager.Instance.ShowUI<UIMimicGacha>();
        GameManager.Instance.SaveGame();
        
    }







}