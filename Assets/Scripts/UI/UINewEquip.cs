using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINewEquip : UIBase
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemStats;
    private void Start()
    {
        SetNewItem();
    }

    private void SetNewItem()
    {
        // TODO 이미지
        //ItemTable newItem = GameManager.Instance.accountInfo.euqipitems
        //itemName.text = newItem.nameAlias;
        //itemStats.text = ($"HP : {newItem.hp} \t 방어 : {newItem.dp} \n 공격 : {newItem.ap} \t 이동속도 : {newItem.speed}");
    }

    public void OnEquipButton()
    {
        //    GameManager.Instance.accountInfo.AddEquipDict();
        gameObject.SetActive(false);
    }

   
}
