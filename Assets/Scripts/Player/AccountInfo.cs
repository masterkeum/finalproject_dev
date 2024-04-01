using System;
using UnityEngine;


[Serializable]
public class Item
{
    public int itemId;
    public ItemCategory itemCategory;
    public ItemType itemType;
    public string nameAlias;
    public ItemGrade grade;
    public string ImageFile;
    public int Hp;
    public int Dp;
    public int Ap;
    public float MoveSpeed;
    public float CriticalHit;
    public int HpGen;


}
/// <summary>
/// 계정정보 : 저장과 불러오기를 위한 클래스
/// </summary>
[Serializable]
public class AccountInfo
{
    /*
    데이터를 받거나 읽어와서 초기화
    계정 데이터 저장
        계정 레벨
        보유 재화
            
        클리어 스테이지
        퀘스트 상태
        인벤토리 상태
        

        골드/코인/ : gold
        젬/보석/ : gem
        행동력 : actionPoint
        계정 : aid (account Identification)

        -- 아이템 능력치 적용 : 인게임으로 이동
    */

    [SerializeField] private string aid;

    [Serializable]
    public class EquipItems
    {
        [SerializeField] public Item Weapon;
        [SerializeField] public Item Armor;
        [SerializeField] public Item Helmet;
        [SerializeField] public Item Gloves;
        [SerializeField] public Item Boots;
        [SerializeField] public Item Accessories;
    }

    public EquipItems equipItems;
    public Item newItem; // 분해되면 null 로 초기화
    public Item changedSlot; // 아이템 교체 시 한쪽 데이터를 임시저장하는 곳

    [SerializeField] public string name;
    [SerializeField] public int level;
    [SerializeField] public int totalExp;

    [SerializeField] public int actionPoint; // 행동력
    [SerializeField] public int gem;
    [SerializeField] public int gold;
    [SerializeField] public int core;
    [SerializeField] public int selectedStageId;
    [SerializeField] public float lastUpdateTime;


    // UI용 데이터
    public int sliderCurExp;
    public int sliderMaxExp;
    public int curExp;

    public CharacterType characterType;

    //생성자
    public AccountInfo(string _aid, string _name)
    {
        aid = _aid;
        name = _name;
        level = 1;
        totalExp = 0;

        actionPoint = DataManager.Instance._InitParam["ActionPoint"]; // 데이터로 빼야함
        gem = 0;
        gold = DataManager.Instance._InitParam["Gold"];
        core = 0;
        selectedStageId = DataManager.Instance._InitParam["StartStageId"];
        lastUpdateTime = UtilityKit.GetCurrentTime();


        equipItems = new EquipItems()
        {
            Weapon = new Item(),
            Armor = new Item(),
            Helmet = new Item(),
            Gloves = new Item(),
            Boots = new Item(),
            Accessories = new Item()
        };

        PlayerLevel levelData = DataManager.Instance.GetPlayerLevel(level + 1);
        sliderCurExp = 0;
        sliderMaxExp = levelData.exp;
        curExp = 0;

        characterType = CharacterType.Player;
    }


    // TODO : AP, gem, gold 마이너스, 현재값, 최대값 검증과정 추가
    public void AddActionPoint(int addActionPoint)
    {
        actionPoint = Math.Min(actionPoint + addActionPoint, GameManager.Instance._maxActionPoint);

        GameManager.Instance.SaveGame();
        GameManager.Instance.UpdateUI();
    }

    public void AddGem(int addGem)
    {
        gem += addGem;
        GameManager.Instance.SaveGame();
        GameManager.Instance.UpdateUI();
    }
    public void AddGold(int addGold)
    {
        gold += addGold;
        GameManager.Instance.SaveGame();
        GameManager.Instance.UpdateUI();
    }


    public void AddUpdateTime(float time = 0)
    {
        if (time == 0)
        {
            lastUpdateTime = UtilityKit.GetCurrentTime();
        }
        lastUpdateTime += time;
    }

    public void AddExp(int addExp)
    {

        while (addExp > 0)
        {
            PlayerLevel levelData = DataManager.Instance.GetPlayerLevel(level + 1);
            if (levelData == null)
            {
                sliderMaxExp = 0;
                Debug.Log("만랩");
                break; // 만랩
            }

            sliderMaxExp = levelData.exp;
            if (totalExp + addExp >= levelData.totalExp)
            {
                // 랩업
                addExp -= (levelData.totalExp - totalExp);
                totalExp = levelData.totalExp;
                curExp = 0;
                sliderCurExp = curExp;
                ++level;
            }
            else
            {
                totalExp += addExp;
                curExp += addExp;
                sliderCurExp = curExp;
                addExp = 0;
            }
        }
        GameManager.Instance.SaveGame();
        GameManager.Instance.UpdateUI();
    }



    public void Equip()
    {
        switch (newItem.itemType)
        {
            case ItemType.Weapon:
                {
                    if (equipItems.Weapon.itemId != 0)
                    {
                        ChangeEquip(ref equipItems.Weapon, ref newItem, ref changedSlot);
                    }
                    else
                        equipItems.Weapon = newItem;
                }
                break;
            case ItemType.Armor:
                {
                    if (equipItems.Armor.itemId != 0)
                    {
                        ChangeEquip(ref equipItems.Armor, ref newItem, ref changedSlot);
                    }
                    else
                        equipItems.Armor = newItem;
                }
                break;
            case ItemType.Gloves:
                {
                    if (equipItems.Gloves.itemId != 0)
                    {
                        ChangeEquip(ref equipItems.Gloves, ref newItem, ref changedSlot);
                    }
                    else
                        equipItems.Gloves = newItem;
                }
                break;
            case ItemType.Boots:
                {
                    if (equipItems.Boots.itemId != 0)
                    {
                        ChangeEquip(ref equipItems.Boots, ref newItem, ref changedSlot);
                    }
                    else
                        equipItems.Boots = newItem;
                }
                break;
            case ItemType.Helmet:
                {
                    if (equipItems.Helmet.itemId != 0)
                    {
                        ChangeEquip(ref equipItems.Helmet, ref newItem, ref changedSlot);
                    }
                    else
                        equipItems.Helmet = newItem;
                }
                break;
            case ItemType.Accessories:
                {
                    if (equipItems.Accessories.itemId != 0)
                    {
                        ChangeEquip(ref equipItems.Accessories, ref newItem, ref changedSlot);
                    }
                    else
                        equipItems.Accessories = newItem;
                }
                break;
        }


    }

    private void ChangeEquip(ref Item item, ref Item newItem, ref Item changedSlot)
    {
        changedSlot = item;
        item = newItem;
        newItem = changedSlot;
        changedSlot = null;
    }

    public void GetANewItem()
    {
        Debug.Log(newItem.itemType);

        switch (newItem.itemType)
        {
            case ItemType.Weapon:
                {
                    if (equipItems.Weapon.itemId != 0)
                        UIManager.Instance.ShowUI<UIEquipChange>();
                    else
                        UIManager.Instance.ShowUI<UINewEquip>();
                }
                break;
            case ItemType.Armor:
                {
                    if (equipItems.Armor.itemId != 0)
                        UIManager.Instance.ShowUI<UIEquipChange>();
                    else
                        UIManager.Instance.ShowUI<UINewEquip>();
                }
                break;
            case ItemType.Gloves:
                {
                    if (equipItems.Gloves.itemId != 0)
                        UIManager.Instance.ShowUI<UIEquipChange>();
                    else
                        UIManager.Instance.ShowUI<UINewEquip>();
                }
                break;
            case ItemType.Boots:
                {
                    if (equipItems.Boots.itemId != 0)
                        UIManager.Instance.ShowUI<UIEquipChange>();
                    else
                        UIManager.Instance.ShowUI<UINewEquip>();
                }
                break;
            case ItemType.Helmet:
                {
                    if (equipItems.Helmet.itemId != 0)
                        UIManager.Instance.ShowUI<UIEquipChange>();
                    else
                        UIManager.Instance.ShowUI<UINewEquip>();
                }
                break;
            case ItemType.Accessories:
                {
                    if (equipItems.Accessories.itemId != 0)
                        UIManager.Instance.ShowUI<UIEquipChange>();
                    else
                        UIManager.Instance.ShowUI<UINewEquip>();
                }
                break;
        }
    }

}
