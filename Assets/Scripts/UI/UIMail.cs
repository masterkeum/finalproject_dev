using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMail : UIBase
{
    public void ClosePopup()
    {
        gameObject.SetActive(false);
        SoundManager.Instance.PlaySound("ButtonClickUI_1",1f);
    }
}
