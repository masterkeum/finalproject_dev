using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI coreQuantity;
    public TextMeshProUGUI mimicLevel;
    public TextMeshProUGUI levelBonusText;
    public TextMeshProUGUI equipBonusText;

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
        mimicLevel.text = "Lv." + GameManager.Instance.accountInfo.mimicLevel;

        PrintBonusStats();
    }

    public void UpdateUISlot(ItemSlotUI slot, Item item)
    {
        string path = item.ImageFile;
        if (item.itemId != 0)
        {
            slot.icon.sprite = Resources.Load<Sprite>(path);
            slot.glow.enabled = true;
            slot.decoGO.SetActive(true);
        }
        else
        {
            slot.glow.enabled = false;
            slot.decoGO.SetActive(false);
        }
        switch (item.grade)
        {

            case ItemGrade.Normal:
                {
                    slot.glow.color = ColorTable.normalColor;
                    slot.decoImage.color = ColorTable.normalDecoColor;
                }
                break;
            case ItemGrade.Magic:
                {
                    slot.glow.color = ColorTable.magicColor;
                    slot.decoImage.color = ColorTable.magicDecoColor;
                }
                break;
            case ItemGrade.Elite:
                {
                    slot.glow.color = ColorTable.eliteColor;
                    slot.decoImage.color = ColorTable.eliteDecoColor;
                }
                break;
            case ItemGrade.Rare:
                {
                    slot.glow.color = ColorTable.rareColor;
                    slot.decoImage.color = ColorTable.rareDecoColor;
                }
                break;
            case ItemGrade.Epic:
                {
                    slot.glow.color = ColorTable.epicColor;
                    slot.decoImage.color = ColorTable.epicDecoColor;
                }
                break;
            case ItemGrade.Legendary:
                {
                    slot.glow.color = ColorTable.legendaryColor;
                    slot.decoImage.color = ColorTable.legendaryDecoColor;
                }
                break;
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
                    selectedItem.MoveSpeed = Mathf.Round(UnityEngine.Random.Range(selectedGradeNType.minMovespeed, selectedGradeNType.maxMovespeed) * 100f) / 100f;
                    break;
                case ItemOptions.CriticalHit:
                    selectedItem.CriticalHit = Mathf.Round(UnityEngine.Random.Range(selectedGradeNType.minCriticalHit, selectedGradeNType.maxCriticalHit) * 100f) / 100f;
                    break;
                case ItemOptions.HpGen:
                    selectedItem.HpGen = (UnityEngine.Random.Range(selectedGradeNType.minHpGen, selectedGradeNType.maxHpGen + 1));
                    break;
            }
        }

    }


    public void StartMimicGacha()
    {
        if (GameManager.Instance.accountInfo.core >= 1) //필요정수 조절
        {
            GameManager.Instance.accountInfo.AddCore(-1);
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
        UpdateUI();
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

    public void PrintBonusStats()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        levelBonusText.text = $" - 계정레벨 : {accountInfo.level} \n HP + {accountInfo.playerStatInfo.addLevelHp+accountInfo.playerStatInfo.hp} \n 공격력 + {accountInfo.playerStatInfo.addLevelAttack+accountInfo.playerStatInfo.attackPower}";
        equipBonusText.text = $" - 장비 능력치 합 \n HP + {accountInfo.playerStatInfo.addHp} \n 공격력 + {accountInfo.playerStatInfo.addAttackPower} \n 방어력 + {accountInfo.playerStatInfo.addDefense} \n " +
            $"이동속도 + {accountInfo.playerStatInfo.addMoveSpeed} \n 크리티컬 + {accountInfo.playerStatInfo.addCritical} \n 체력리젠 + {accountInfo.playerStatInfo.addHpGen}";
    }







}