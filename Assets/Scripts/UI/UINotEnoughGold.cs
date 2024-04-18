using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINotEnoughGold : UIBase
{
    public GameObject uiNotEnoughGold;
    // Start is called before the first frame update
    public void ClosePopup()
    {
        uiNotEnoughGold.SetActive(false);
    }
}
