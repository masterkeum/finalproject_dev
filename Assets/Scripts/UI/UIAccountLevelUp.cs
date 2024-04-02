using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAccountLevelUp : UIBase
{
    public TextMeshProUGUI levelText;

    private void OnEnable()
    {
        
    }

    public void ClosePopUp()
    {
        gameObject.SetActive(false);
    }
}
