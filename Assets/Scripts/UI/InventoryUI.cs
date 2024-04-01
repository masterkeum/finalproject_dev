using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;




public class InventoryUI : MonoBehaviour
{
    [Header("UISlots")]
    public ItemSlotUI weaponSlot;
    public ItemSlotUI helmetSlot;
    public ItemSlotUI glovesSlot;
    public ItemSlotUI bootsSlot;
    public ItemSlotUI armorSlot;
    public ItemSlotUI accessorriesSlot;

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

    public void UpdateUI()
    {
        UpdateWeaponSlot();
        UpdateHelmetSlot();
        UpdateGlovesSlot();
        UpdateBootsSlot();
        UpdateArmorSlot();
        UpdateAccessorriesSlot();
    }
    
    public void UpdateWeaponSlot()
    {
        string path = GameManager.Instance.accountInfo.equipItems.Weapon.ImageFile;
        if(GameManager.Instance.accountInfo.equipItems.Weapon != null )
        {
            weaponSlot.icon.sprite = Resources.Load<Sprite>(path);
            weaponSlot.glow.enabled = true;
        }
        else
        {
            weaponSlot.glow.enabled=false;
        }
        switch(GameManager.Instance.accountInfo.equipItems.Weapon.grade)
        {
            case ItemGrade.Normal:
                weaponSlot.glow.color = new Color(1f,1f,1f); break;
            case ItemGrade.Magic:
                weaponSlot.glow.color = new Color(40/255f,1f,35/255f); break;
            case ItemGrade.Elite:
                weaponSlot.glow.color = new Color(0f,67/255f,1f); break;
            case ItemGrade.Rare:
                weaponSlot.glow.color = new Color(1f, 115 / 255f, 0f); break;
            case ItemGrade.Epic:
                weaponSlot.glow.color = new Color(1f,1f,0f); break;
            case ItemGrade.Legendary:
                weaponSlot.glow.color = new Color(1f, 0f, 0f);  break;
        }
    }
    public void UpdateHelmetSlot()
    {
        string path = GameManager.Instance.accountInfo.equipItems.Helmet.ImageFile;
        if (GameManager.Instance.accountInfo.equipItems.Helmet != null)
        {
            helmetSlot.icon.sprite = Resources.Load<Sprite>(path);
            helmetSlot.glow.enabled = true;
        }
        else
        {
            helmetSlot.glow.enabled = false;
        }
        switch (GameManager.Instance.accountInfo.equipItems.Helmet.grade)
        {
            case ItemGrade.Normal:
                helmetSlot.glow.color = new Color(1f, 1f, 1f); break;
            case ItemGrade.Magic:
                helmetSlot.glow.color = new Color(40 / 255f, 1f, 35 / 255f); break;
            case ItemGrade.Elite:
                helmetSlot.glow.color = new Color(0f, 67 / 255f, 1f); break;
            case ItemGrade.Rare:
                helmetSlot.glow.color = new Color(1f, 115 / 255f, 0f); break;
            case ItemGrade.Epic:
                helmetSlot.glow.color = new Color(1f, 1f, 0f); break;
            case ItemGrade.Legendary:
                helmetSlot.glow.color = new Color(1f, 0f, 0f); break;
        }
    }

    public void UpdateGlovesSlot()
    {
        string path = GameManager.Instance.accountInfo.equipItems.Gloves.ImageFile;
        if (GameManager.Instance.accountInfo.equipItems.Gloves != null)
        {
            glovesSlot.icon.sprite = Resources.Load<Sprite>(path);
            glovesSlot.glow.enabled = true;
        }
        else
        {
            glovesSlot.glow.enabled = false;
        }
        switch (GameManager.Instance.accountInfo.equipItems.Gloves.grade)
        {
            case ItemGrade.Normal:
                glovesSlot.glow.color = new Color(1f, 1f, 1f); break;
            case ItemGrade.Magic:
                glovesSlot.glow.color = new Color(40 / 255f, 1f, 35 / 255f); break;
            case ItemGrade.Elite:
                glovesSlot.glow.color = new Color(0f, 67 / 255f, 1f); break;
            case ItemGrade.Rare:
                glovesSlot.glow.color = new Color(1f, 115 / 255f, 0f); break;
            case ItemGrade.Epic:
                glovesSlot.glow.color = new Color(1f, 1f, 0f); break;
            case ItemGrade.Legendary:
                glovesSlot.glow.color = new Color(1f, 0f, 0f); break;
        }
    }

    public void UpdateBootsSlot()
    {
        string path = GameManager.Instance.accountInfo.equipItems.Boots.ImageFile;
        if (GameManager.Instance.accountInfo.equipItems.Boots != null)
        {
            bootsSlot.icon.sprite = Resources.Load<Sprite>(path);
            bootsSlot.glow.enabled = true;
        }
        else
        {
            bootsSlot.glow.enabled = false;
        }
        switch (GameManager.Instance.accountInfo.equipItems.Boots.grade)
        {
            case ItemGrade.Normal:
                bootsSlot.glow.color = new Color(1f, 1f, 1f); break;
            case ItemGrade.Magic:
                bootsSlot.glow.color = new Color(40 / 255f, 1f, 35 / 255f); break;
            case ItemGrade.Elite:
                bootsSlot.glow.color = new Color(0f, 67 / 255f, 1f); break;
            case ItemGrade.Rare:
                bootsSlot.glow.color = new Color(1f, 115 / 255f, 0f); break;
            case ItemGrade.Epic:
                bootsSlot.glow.color = new Color(1f, 1f, 0f); break;
            case ItemGrade.Legendary:
                bootsSlot.glow.color = new Color(1f, 0f, 0f); break;
        }
    }

    public void UpdateArmorSlot()
    {
        string path = GameManager.Instance.accountInfo.equipItems.Armor.ImageFile;
        if (GameManager.Instance.accountInfo.equipItems.Armor != null)
        {
            armorSlot.icon.sprite = Resources.Load<Sprite>(path);
            armorSlot.glow.enabled = true;
        }
        else
        {
            armorSlot.glow.enabled = false;
        }
        switch (GameManager.Instance.accountInfo.equipItems.Armor.grade)
        {
            case ItemGrade.Normal:
                armorSlot.glow.color = new Color(1f, 1f, 1f); break;
            case ItemGrade.Magic:
                armorSlot.glow.color = new Color(40 / 255f, 1f, 35 / 255f); break;
            case ItemGrade.Elite:
                armorSlot.glow.color = new Color(0f, 67 / 255f, 1f); break;
            case ItemGrade.Rare:
                armorSlot.glow.color = new Color(1f, 115 / 255f, 0f); break;
            case ItemGrade.Epic:
                armorSlot.glow.color = new Color(1f, 1f, 0f); break;
            case ItemGrade.Legendary:
                armorSlot.glow.color = new Color(1f, 0f, 0f); break;
        }
    }

    public void UpdateAccessorriesSlot()
    {
        string path = GameManager.Instance.accountInfo.equipItems.Accessories.ImageFile;
        if (GameManager.Instance.accountInfo.equipItems.Accessories != null)
        {
            accessorriesSlot.icon.sprite = Resources.Load<Sprite>(path);
            accessorriesSlot.glow.enabled = true;
        }
        else
        {
            accessorriesSlot.glow.enabled = false;
        }
        switch (GameManager.Instance.accountInfo.equipItems.Accessories.grade)
        {
            case ItemGrade.Normal:
                accessorriesSlot.glow.color = new Color(1f, 1f, 1f); break;
            case ItemGrade.Magic:
                accessorriesSlot.glow.color = new Color(40 / 255f, 1f, 35 / 255f); break;
            case ItemGrade.Elite:
                accessorriesSlot.glow.color = new Color(0f, 67 / 255f, 1f); break;
            case ItemGrade.Rare:
                accessorriesSlot.glow.color = new Color(1f, 115 / 255f, 0f); break;
            case ItemGrade.Epic:
                accessorriesSlot.glow.color = new Color(1f, 1f, 0f); break;
            case ItemGrade.Legendary:
                accessorriesSlot.glow.color = new Color(1f, 0f, 0f); break;
        }
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
        List<ItemTable> selectedGrade = items.FindAll((item) => item.grade == grade );
        ItemTable selectedGradeNType = selectedGrade.Find((Item) => Item.itemType == randomtype);

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
        if(GameManager.Instance.accountInfo.core >= 1)
        {
            SetMimicGacha();
            GameManager.Instance.accountInfo.newItem = selectedItem;

            UIManager.Instance.ShowUI<UIMimicGacha>();
            GameManager.Instance.SaveGame();
        }
        else
        {
            UINoCurrency uINoCurrency = UIManager.Instance.ShowUI<UINoCurrency>();
            uINoCurrency.NoCore();
        }
       
        
    }







}