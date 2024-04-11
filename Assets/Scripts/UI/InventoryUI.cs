using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;




public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI coreQuantity;
    public TextMeshProUGUI mimicLevel;


    [Header("UISlots")]
    public ItemSlotUI weaponSlot;
    public ItemSlotUI helmetSlot;
    public ItemSlotUI glovesSlot;
    public ItemSlotUI bootsSlot;
    public ItemSlotUI armorSlot;
    public ItemSlotUI accessorriesSlot;



    private Item selectedItem;

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

    public void UpdateUI()
    {
        AccountInfo.EquipItems equipItems = GameManager.Instance.accountInfo.equipItems;

        UpdateUISlot(weaponSlot, equipItems.Weapon);
        UpdateUISlot(armorSlot, equipItems.Armor);
        UpdateUISlot(helmetSlot, equipItems.Helmet);
        UpdateUISlot(glovesSlot, equipItems.Gloves);
        UpdateUISlot(bootsSlot, equipItems.Boots);
        UpdateUISlot(accessorriesSlot, equipItems.Accessories);

        coreQuantity.text = GameManager.Instance.accountInfo.core.ToString();
        mimicLevel.text = "Lv."+GameManager.Instance.accountInfo.mimicLevel;
    }

    public void UpdateUISlot(ItemSlotUI slot, Item item)
    {
        string path = item.ImageFile;
        if (item.itemId != 0)
        {
            slot.icon.sprite = Resources.Load<Sprite>(path);
            slot.glow.enabled = true;
        }
        else
        {
            slot.glow.enabled = false;
        }
        switch (item.grade)
        {

            case ItemGrade.Normal:
                slot.glow.color = new Color(1f, 1f, 1f); break;
            case ItemGrade.Magic:
                slot.glow.color = new Color(40 / 255f, 1f, 35 / 255f); break;
            case ItemGrade.Elite:
                slot.glow.color = new Color(0f, 67 / 255f, 1f); break;
            case ItemGrade.Rare:
                slot.glow.color = new Color(1f, 115 / 255f, 0f); break;
            case ItemGrade.Epic:
                slot.glow.color = new Color(1f, 1f, 0f); break;
            case ItemGrade.Legendary:
                slot.glow.color = new Color(1f, 0f, 0f); break;
        }
    }



    public void SetMimicGacha()
    {
        int playerLevel = GameManager.Instance.accountInfo.level;
        normal = DataManager.Instance.GetLevelGacha(playerLevel).normal;
        magic = DataManager.Instance.GetLevelGacha(playerLevel).magic;
        elite = DataManager.Instance.GetLevelGacha(playerLevel).elite;
        rare = DataManager.Instance.GetLevelGacha(playerLevel).rare;
        epic = DataManager.Instance.GetLevelGacha(playerLevel).epic;
        legendary = DataManager.Instance.GetLevelGacha(playerLevel).legendary;
        selectedItem = new Item();

        System.Random random = new System.Random();

        int num = random.Next(0, 101);
        float[] probs = { normal, magic, elite, rare, epic, legendary };
        ItemGrade grade = ItemGrade.Normal;

        float cumulative = 0f;
        int target = -1;
        for (int i = 0; i < probs.Length; i++)
        {
            cumulative += probs[i];
            if (num <= cumulative)
            {
                target = i;
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



        List<ItemType> equipList = new List<ItemType>();
        foreach (ItemType item in Enum.GetValues(typeof(ItemType)))
        {
            if (item == ItemType.Weapon || item == ItemType.Armor || item == ItemType.Helmet || item == ItemType.Gloves || item == ItemType.Boots || item == ItemType.Accessories)
            {
                equipList.Add(item);
            }
        }

        ItemType randomtype = equipList[UnityEngine.Random.Range(0, equipList.Count)];

        List<ItemTable> items = DataManager.Instance.itemTableDict.Values.ToList();

        List<ItemTable> selectedGrade = items.FindAll((item) => item.grade == grade);
        ItemTable selectedGradeNType = selectedGrade.Find((Item) => Item.itemType == randomtype);

        selectedItem.itemId = selectedGradeNType.itemId;
        selectedItem.itemCategory = selectedGradeNType.itemCategory;
        selectedItem.itemType = selectedGradeNType.itemType;
        selectedItem.nameAlias = selectedGradeNType.nameAlias;
        selectedItem.grade = selectedGradeNType.grade;
        selectedItem.ImageFile = selectedGradeNType.imageFile;
        selectedItem.getGold = selectedGradeNType.getGold;
        selectedItem.getExp = selectedGradeNType.getExp;

        List<ItemOptions> options = new List<ItemOptions>();
        int maxRange = 4;
        switch (selectedItem.grade)
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

        while (options.Count < (int)selectedItem.grade)
        {
            if (options.Count >= (int)selectedItem.grade)
                break;

            ItemOptions randomOption = (ItemOptions)UnityEngine.Random.Range(0, maxRange);
            if (!options.Contains(randomOption))
            {
                options.Add(randomOption);
            }
        }

        foreach (ItemOptions randomOption in options)
        {
            // TODO : 스탯 소수점 보정
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
        if (GameManager.Instance.accountInfo.core >= 0) //필요정수 조절
        {
            SetMimicGacha();
            GameManager.Instance.accountInfo.newItem = selectedItem;

            UIManager.Instance.ShowUI<UIMimicGacha>();
            switch (selectedItem.grade)
            {
                case ItemGrade.Normal:
                case ItemGrade.Magic:
                case ItemGrade.Elite:
                    {
                        SoundManager.Instance.PlaySound("GetItemUI_1");
                    }
                    break;
                case ItemGrade.Rare:
                case ItemGrade.Epic:
                    {
                        SoundManager.Instance.PlaySound("GetItemUI_2");
                    }
                    break;
                case ItemGrade.Legendary:
                    {
                        SoundManager.Instance.PlaySound("GetItemUI_3");
                    }
                    break;
            }
            GameManager.Instance.SaveGame();

        }
        else
        {
            UINoCurrency uINoCurrency = UIManager.Instance.ShowUI<UINoCurrency>();
            uINoCurrency.NoCore();
            SoundManager.Instance.PlaySound("ErrorUI_1");
        }
    }

    public void OnEquipClick(int itemIndex)
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        switch (itemIndex)
        {
            case 0:
                if (accountInfo.equipItems.Weapon.itemId == 0)
                    return;
                accountInfo.checkCurItem = accountInfo.equipItems.Weapon; break;
            case 1:
                if (accountInfo.equipItems.Helmet.itemId == 0)
                    return;
                accountInfo.checkCurItem = accountInfo.equipItems.Helmet; break;
            case 2:
                if (accountInfo.equipItems.Gloves.itemId == 0)
                    return;
                accountInfo.checkCurItem = accountInfo.equipItems.Gloves; break;
            case 3:
                if (accountInfo.equipItems.Boots.itemId == 0)
                    return;
                accountInfo.checkCurItem = accountInfo.equipItems.Boots; break;
            case 4:
                if (accountInfo.equipItems.Armor.itemId == 0)
                    return;
                accountInfo.checkCurItem = accountInfo.equipItems.Armor; break;
            case 5:
                if (accountInfo.equipItems.Accessories.itemId == 0)
                    return;
                accountInfo.checkCurItem = accountInfo.equipItems.Accessories; break;
        }
        UIManager.Instance.ShowUI<UICheckEquip>();
        SoundManager.Instance.PlaySound("ButtonClickUI_1");
    }

    public void PopupMimicLevelInfo()
    {
        UIManager.Instance.ShowUI<UIMimicLevelUp>();
        SoundManager.Instance.PlaySound("ButtonClickUI_1");
    }







}