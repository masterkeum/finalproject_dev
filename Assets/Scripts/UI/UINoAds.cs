using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINoAds : UIBase
{
    public GameObject uiNoAds;
    // Start is called before the first frame update
    public void ClosePopup()
    {
        uiNoAds.SetActive(false);
    }
}
