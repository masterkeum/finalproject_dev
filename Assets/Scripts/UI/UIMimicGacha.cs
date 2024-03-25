using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMimicGacha : UIBase
{
    public Image itemImage;
    public GameObject outGameHUD;
    private InventoryUI inventory;

    private void Awake()
    {
        inventory = outGameHUD.GetComponentInChildren<InventoryUI>();
        //랜덤아이템 리스트에 아이템 집어넣기(혹은 위쪽 장비UI로 뺄 수도 있음)
    }

    private void OnEnable()
    {
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);

        UIManager.Instance.ShowUI<UIEquipChange>();
    }

}
