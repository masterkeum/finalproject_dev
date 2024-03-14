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
        UIManager.Instance.ShowUI<UINotice>();
    }
    public void Option()
    {
        UIManager.Instance.ShowUI<UIOption>();
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
