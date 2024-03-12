using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINotice : MonoBehaviour
{
    public Image noticeImage;
    
    public void OnclickNotice(int index)
    {

    }
    public void ClosePopup()
    {
        Destroy(gameObject);
    }

}
