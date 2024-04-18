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
    public int getGold;
    public int getExp;
}

[Serializable]
public struct PlayerStatInfo
{
    /*
    공격력
    +템 공격력
    *공격력 계수

    방어력 = 0
    +템 방어력
    *방어력 계수

    스킬데미지

    크리티컬 확률
    *크리티컬 계수

    */
    public int hp;
    public int addHp;

    public int attackPower;
    public int addAttackPower;

    public int defense;
    public int addDefense;

    public float moveSpeed;
    public float addMoveSpeed;

    public float critical;
    public float addCritical;

    public float hpGen;
    public float addHpGen;

    public PlayerStatInfo(CharacterInfo characterInfo, AccountInfo.EquipItems equipItems)
    {
        // TODO : 아이템과 더불어 개선 필요
        hp = characterInfo.hp;
        addHp = equipItems.Weapon.Hp + equipItems.Armor.Hp + equipItems.Helmet.Hp + equipItems.Gloves.Hp + equipItems.Boots.Hp + equipItems.Accessories.Hp;

        attackPower = characterInfo.attackPower;
        addAttackPower = equipItems.Weapon.Ap + equipItems.Armor.Ap + equipItems.Helmet.Ap + equipItems.Gloves.Ap + equipItems.Boots.Ap + equipItems.Accessories.Ap;

        defense = 0;
        addDefense = equipItems.Weapon.Dp + equipItems.Armor.Dp + equipItems.Helmet.Dp + equipItems.Gloves.Dp + equipItems.Boots.Dp + equipItems.Accessories.Dp;

        moveSpeed = characterInfo.moveSpeed;
        addMoveSpeed = equipItems.Weapon.MoveSpeed + equipItems.Armor.MoveSpeed + equipItems.Helmet.MoveSpeed + equipItems.Gloves.MoveSpeed + equipItems.Boots.MoveSpeed + equipItems.Accessories.MoveSpeed;

        critical = 0;
        addCritical = equipItems.Weapon.CriticalHit + equipItems.Armor.CriticalHit + equipItems.Helmet.CriticalHit + equipItems.Gloves.CriticalHit + equipItems.Boots.CriticalHit + equipItems.Accessories.CriticalHit;

        hpGen = 0;
        addHpGen = equipItems.Weapon.HpGen + equipItems.Armor.HpGen + equipItems.Helmet.HpGen + equipItems.Gloves.HpGen + equipItems.Boots.HpGen + equipItems.Accessories.HpGen;
    }
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
        // FIXME : 차후 아이템 구조는 개선을 해야할것.
        [SerializeField] public Item Weapon;
        [SerializeField] public Item Armor;
        [SerializeField] public Item Helmet;
        [SerializeField] public Item Gloves;
        [SerializeField] public Item Boots;
        [SerializeField] public Item Accessories;
    }

    public EquipItems equipItems;
    public Item newItem; // 분해되면 null 로 초기화
    public Item checkCurItem = new Item(); //장착 아이템 확인팝업 띄울 때 사용
    public Item changedSlot; // 아이템 교체 시 한쪽 데이터를 임시저장하는 곳

    public string name;
    public int level;
    public int totalExp;
    public int mimicLevel;
    public int mimicTotalExp;
    public int selectedProfileIndex = 0;

    public int actionPoint; // 행동력
    public int gem;
    public int gold;
    public int core;
    public int timeTicket;
    public int selectedStageId;
    public int clearStageId;
    public float lastUpdateTime;

    public PlayerStatInfo playerStatInfo;

    // UI용 데이터
    public int sliderCurExp;
    public int sliderMaxExp;
    public int curExp;
    // 미믹용 데이터
    public int mimicSliderCurExp;
    public int mimicSliderMaxExp;
    public int mimicCurExp;
    public bool isLevelUpProcessing = false;
    public DateTime completeTime;

    public float bgmVolume;
    public float sfxVolume;

    //상점용 데이터 
    public int totalCost = 0;
    public bool isPurchasedStarterPack = false;
    public bool isPurchasedAdDeletePack = false;

    //생성자
    public AccountInfo(string _aid, string _name)
    {
        aid = _aid;
        name = $"Player_{_name}";
        level = 1;
        totalExp = 0;
        mimicLevel = 1;
        mimicTotalExp = 0;

        actionPoint = DataManager.Instance._InitParam["ActionPoint"];
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

        Mimiclevel mimicLvData = DataManager.Instance.GetMimicLevel(level + 1);
        mimicSliderCurExp = 0;
        mimicSliderMaxExp = mimicLvData.exp;
        curExp = 0;

        clearStageId = 0;

        // 플레이어 스탯
        playerStatInfo = new PlayerStatInfo(DataManager.Instance.GetCharacterInfo(DataManager.Instance._InitParam["StartCharacterId"]), equipItems);

        bgmVolume = 1;
        sfxVolume = 1;
    }

    public void CalcPlayerStat()
    {
        playerStatInfo.addHp = equipItems.Weapon.Hp + equipItems.Armor.Hp + equipItems.Helmet.Hp + equipItems.Gloves.Hp + equipItems.Boots.Hp + equipItems.Accessories.Hp;

        playerStatInfo.addAttackPower = equipItems.Weapon.Ap + equipItems.Armor.Ap + equipItems.Helmet.Ap + equipItems.Gloves.Ap + equipItems.Boots.Ap + equipItems.Accessories.Ap;

        playerStatInfo.addDefense = equipItems.Weapon.Dp + equipItems.Armor.Dp + equipItems.Helmet.Dp + equipItems.Gloves.Dp + equipItems.Boots.Dp + equipItems.Accessories.Dp;

        playerStatInfo.addMoveSpeed = equipItems.Weapon.MoveSpeed + equipItems.Armor.MoveSpeed + equipItems.Helmet.MoveSpeed + equipItems.Gloves.MoveSpeed + equipItems.Boots.MoveSpeed + equipItems.Accessories.MoveSpeed;

        playerStatInfo.addCritical = equipItems.Weapon.CriticalHit + equipItems.Armor.CriticalHit + equipItems.Helmet.CriticalHit + equipItems.Gloves.CriticalHit + equipItems.Boots.CriticalHit + equipItems.Accessories.CriticalHit;

        playerStatInfo.addHpGen = equipItems.Weapon.HpGen + equipItems.Armor.HpGen + equipItems.Helmet.HpGen + equipItems.Gloves.HpGen + equipItems.Boots.HpGen + equipItems.Accessories.HpGen;
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

    public void MinusGold(int minusGold)
    {
        gold -= minusGold;
        GameManager.Instance.SaveGame();
        GameManager.Instance.UpdateUI();
    }

    public void AddCore(int addCore)
    {
        core += addCore;
        GameManager.Instance.SaveGame();
        GameManager.Instance.UpdateUI();
    }
    public void AddTimeTicket(int addTimeTicket)
    {
        timeTicket += addTimeTicket;
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
    public void AddMimicExp(int addExp)
    {
        Mimiclevel levelData = DataManager.Instance.GetMimicLevel(level + 1);
        if (levelData == null)
        {
            mimicSliderMaxExp = 0;
            Debug.Log("만랩");
            return;
        }
        else
        {
            mimicTotalExp += addExp;
            mimicCurExp += addExp;
            addExp = 0;
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

        CalcPlayerStat();
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
