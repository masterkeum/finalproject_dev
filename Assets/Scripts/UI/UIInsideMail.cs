using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInsideMail : UIBase
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI mainText;

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
