using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPropertiesList : UIBase
{
   public void Mail()
    {
        UIManager.Instance.ShowUI<UIMail>();
    }
    public void Notice()
    {

    }
    public void Option()
    {
        UIManager.Instance.ShowUI<UIOption>();
    }

    public void ClosePopup()
    {
        Destroy(gameObject);
    }
}
