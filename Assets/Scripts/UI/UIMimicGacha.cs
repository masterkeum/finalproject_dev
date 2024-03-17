using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMimicGacha : UIBase
{
    public Image itemImage;
    public bool isMimiced = false;

    private void Awake()
    {
        //랜덤아이템 리스트에 아이템 집어넣기(혹은 위쪽 장비UI로 뺄 수도 있음)
    }

    private void OnEnable()
    {
        isMimiced = true;
    }

    public void ClosePopup()
    {
        isMimiced = false;
        gameObject.SetActive(false);
    }
}
