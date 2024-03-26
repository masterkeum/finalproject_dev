using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

/// <summary>
/// 계정정보 : 저장과 불러오기를 위한 클래스
/// </summary>
public class AccountInfo : MonoBehaviour
{
    /*
    데이터를 받거나 읽어와서 초기화
    계정 데이터 저장
        계정 레벨
        보유 재화
            
        클리어 스테이지
        퀘스트 상태
        인벤토리 상태
        
    */
    [ReadOnly, SerializeField] private string aId;

    [Header("Inventory")] //인벤토리 아이템들
    public Dictionary<string, ItemTable> equipItems = new Dictionary<string, ItemTable>();


    private string _name;
    private string _level;
    private string _totalExp;

    private int actionPoint; // 행동력
    private int gem;
    private int gold;

    private Time lastUpdateTime;

    public ItemTable newItem = new ItemTable();


    public void AddEquipDict()
    {
        switch (newItem.itemType)
        {
            case "Weapon":
                {
                    if (equipItems["Weapon"] != null)
                    {
                        equipItems.Remove("Weapon");
                    }
                    equipItems.Add(newItem.itemType, newItem);
                }
                break;
            case "Armor":
                {
                    if (equipItems["Armor"] != null)
                    {
                        equipItems.Remove("Armor");
                    }
                    equipItems.Add(newItem.itemType, newItem);
                }
                break;
            case "Helmet":
                {
                    if (equipItems["Helmet"] != null)
                    {
                        equipItems.Remove("Helmet");
                    }
                    equipItems.Add(newItem.itemType, newItem);
                }
                break;
            case "Gloves":
                {
                    if (equipItems["Gloves"] != null)
                    {
                        equipItems.Remove("Gloves");
                    }
                    equipItems.Add(newItem.itemType, newItem);
                }
                break;
            case "Boots":
                {
                    if (equipItems["Boots"] != null)
                    {
                        equipItems.Remove("Boots");
                    }
                    equipItems.Add(newItem.itemType, newItem);
                }
                break;
            case "Accessorries":
                {
                    if (equipItems["Accessorries"] != null)
                    {
                        equipItems.Remove("Accessorries");
                    }
                    equipItems.Add(newItem.itemType, newItem);
                }
                break;
        }
    }

}
