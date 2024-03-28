using System;
using UnityEngine;


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
        [SerializeField] public ItemTable Weapon;
        [SerializeField] public ItemTable Armor;
        [SerializeField] public ItemTable Helmet;
        [SerializeField] public ItemTable Gloves;
        [SerializeField] public ItemTable Boots;
        [SerializeField] public ItemTable Accessories;
        [SerializeField] public ItemTable newItem; // 분해되면 null 로 초기화
    }

    public EquipItems equipItems;

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
            Weapon = new ItemTable(),
            Armor = new ItemTable(),
            Helmet = new ItemTable(),
            Gloves = new ItemTable(),
            Boots = new ItemTable(),
            Accessories = new ItemTable()
        };

        sliderCurExp = 0;
        sliderMaxExp = 0;
        curExp = 0;
    }


    // TODO : AP, gem, gold 마이너스, 현재값, 최대값 검증과정 추가
    public void AddActionPoint(int addActionPoint)
    {
        actionPoint += addActionPoint;
        GameManager.Instance.UpdateUI();
    }

    public void AddGem(int addGem)
    {
        gem += addGem;
        GameManager.Instance.UpdateUI();
    }
    public void AddGold(int addGold)
    {
        gold += addGold;
        GameManager.Instance.UpdateUI();
    }


    public void AddUpdateTime(float time = 0)
    {
        if (time == 0)
        {
            lastUpdateTime = UtilityKit.GetCurrentTime();
        }
        lastUpdateTime = time;
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
        GameManager.Instance.UpdateUI();
    }



    //private void AddEquipDict()
    //{
    //    switch (newItem.itemType)
    //    {
    //        case "Weapon":
    //            {
    //                if (equipItems["Weapon"] != null)
    //                {
    //                    equipItems.Remove("Weapon");
    //                }
    //                equipItems.Add(newItem.itemType, newItem);
    //            }
    //            break;
    //        case "Armor":
    //            {
    //                if (equipItems["Armor"] != null)
    //                {
    //                    equipItems.Remove("Armor");
    //                }
    //                equipItems.Add(newItem.itemType, newItem);
    //            }
    //            break;
    //        case "Helmet":
    //            {
    //                if (equipItems["Helmet"] != null)
    //                {
    //                    equipItems.Remove("Helmet");
    //                }
    //                equipItems.Add(newItem.itemType, newItem);
    //            }
    //            break;
    //        case "Gloves":
    //            {
    //                if (equipItems["Gloves"] != null)
    //                {
    //                    equipItems.Remove("Gloves");
    //                }
    //                equipItems.Add(newItem.itemType, newItem);
    //            }
    //            break;
    //        case "Boots":
    //            {
    //                if (equipItems["Boots"] != null)
    //                {
    //                    equipItems.Remove("Boots");
    //                }
    //                equipItems.Add(newItem.itemType, newItem);
    //            }
    //            break;
    //        case "Accessorries":
    //            {
    //                if (equipItems["Accessorries"] != null)
    //                {
    //                    equipItems.Remove("Accessorries");
    //                }
    //                equipItems.Add(newItem.itemType, newItem);
    //            }
    //            break;
    //    }
    //}

}
