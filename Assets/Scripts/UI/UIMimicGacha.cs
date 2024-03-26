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
