using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMimicGacha : UIBase
{
    public Image itemImage;
    

    private void Awake()
    {
       
    }

    private void OnEnable()
    {
    }

    public void ClosePopup()
    {
        GameManager.Instance.accountInfo.GetANewItem();
        gameObject.SetActive(false);
        
    }

}
