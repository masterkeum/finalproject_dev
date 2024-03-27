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
        
        -- 아이템 능력치 적용 : 인게임으로 이동
    */
    public string aid;

    public class EquipItems
    {
        public ItemTable Weapon;
        public ItemTable Armor;
        public ItemTable Helmet;
        public ItemTable Gloves;
        public ItemTable Boots;
        public ItemTable Accessories;

    }
    //인벤토리 아이템들 => 딕셔너리는 저장 안됨
    //public Dictionary<string, ItemTable> equipItems = new Dictionary<string, ItemTable>();
    public EquipItems equipItems;


    public string name;
    public int level;
    public int totalExp;

    public int actionPoint; // 행동력
    public int gem;
    public int gold;
    public int core;

    public ItemTable newItem; // 분해되면 null 로 초기화
    public ItemTable changedSlot; // 아이템 교체 시 한쪽 데이터를 임시저장하는 곳

    public float lastUpdateTime;

    //생성자
    public AccountInfo(string _aid, string _name)
    {
        aid = _aid;
        name = _name;
        level = 1;
        totalExp = 0;

        actionPoint = 30; // 데이터로 빼야함
        gem = 0;
        gold = 0;
        core = 0;

        lastUpdateTime = UtilityKit.GetCurrentTime();
        equipItems = new EquipItems();
    }





    public void Equip()
    {
        switch (newItem.itemType)
        {
            case "Weapon":
                {
                    if (equipItems.Weapon != null)
                    {
                        ChangeEquip(equipItems.Weapon);
                    }
                    else
                        equipItems.Weapon = newItem;
                }
                break;
            case "Armor":
                {
                    if (equipItems.Armor != null)
                    {
                        ChangeEquip(equipItems.Armor);
                    }
                    else
                        equipItems.Armor = newItem;
                }
                break;
            case "Gloves":
                {
                    if (equipItems.Gloves != null)
                    {
                        ChangeEquip(equipItems.Gloves);
                    }
                    else
                        equipItems.Gloves = newItem;
                }
                break;
            case "Boots":
                {
                    if (equipItems.Boots != null)
                    {
                        ChangeEquip(equipItems.Boots);
                    }
                    else
                        equipItems.Boots = newItem;
                }
                break;
            case "Helmet":
                {
                    if (equipItems.Helmet != null)
                    {
                        ChangeEquip(equipItems.Helmet);
                    }
                    else
                        equipItems.Helmet = newItem;
                }
                break;
            case "Accessorries":
                {
                    if (equipItems.Accessories != null)
                    {
                        ChangeEquip(equipItems.Accessories);
                    }
                    else
                        equipItems.Accessories = newItem;
                }
                break;
        }


    }

    private void ChangeEquip(ItemTable item)
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
            case "Weapon":
                {
                    if (equipItems.Weapon != null)
                        UIManager.Instance.ShowUI<UIEquipChange>();
                    else if (equipItems.Weapon == null)
                        UIManager.Instance.ShowUI<UINewEquip>();
                }
                break;
            case "Armor":
                {
                    if (equipItems.Armor != null)
                        UIManager.Instance.ShowUI<UIEquipChange>();
                    else if (equipItems.Armor == null)
                        UIManager.Instance.ShowUI<UINewEquip>();
                }
                break;
            case "Gloves":
                {
                    if (equipItems.Gloves != null)
                        UIManager.Instance.ShowUI<UIEquipChange>();
                    else if (equipItems.Gloves == null)
                        UIManager.Instance.ShowUI<UINewEquip>();
                }
                break;
            case "Boots":
                {
                    if (equipItems.Boots != null)
                        UIManager.Instance.ShowUI<UIEquipChange>();
                    else if (equipItems.Boots == null)
                        UIManager.Instance.ShowUI<UINewEquip>();
                }
                break;
            case "Helmet":
                {
                    if (equipItems.Helmet != null)
                        UIManager.Instance.ShowUI<UIEquipChange>();
                    else if (equipItems.Helmet == null)
                        UIManager.Instance.ShowUI<UINewEquip>();
                }
                break;
            case "Accessorries":
                {
                    if (equipItems.Accessories != null)
                        UIManager.Instance.ShowUI<UIEquipChange>();
                    else if (equipItems.Accessories == null)
                        UIManager.Instance.ShowUI<UINewEquip>();
                }
                break;
        }
    }

}
