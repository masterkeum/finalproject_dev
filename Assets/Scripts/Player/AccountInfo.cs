using System;
using System.Collections.Generic;
using UnityEngine;
using static AccountInfo;


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

    [SerializeField] public string aid { get; private set; }

    public class EquipItems
    {
        public ItemTable Weapon;
        public ItemTable Armor;
        public ItemTable Helmet;
        public ItemTable Gloves;
        public ItemTable Boots;
        public ItemTable Accessories;
        public ItemTable newItem; // 분해되면 null 로 초기화
    }
    //인벤토리 아이템들 => 딕셔너리는 저장 안됨
    //public Dictionary<string, ItemTable> equipItems = new Dictionary<string, ItemTable>();
    EquipItems equipItems = new EquipItems();

    [SerializeField] private string name;
    [SerializeField] private int level;
    [SerializeField] private int totalExp;

    [SerializeField] public int actionPoint { get; private set; } // 행동력
    [SerializeField] public int gem { get; private set; }
    [SerializeField] public int gold { get; private set; }
    [SerializeField] public int core { get; private set; }
    [SerializeField] public int selectedStageId { get; private set; }
    [SerializeField] public float lastUpdateTime { get; private set; }

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
    }

    public void AddActionPoint(int addActionPoint)
    {
        actionPoint += addActionPoint;
    }

    public void AddUpdateTime(float time = 0)
    {
        if (time == 0)
        {
            lastUpdateTime = UtilityKit.GetCurrentTime();
        }
        lastUpdateTime = time;
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
