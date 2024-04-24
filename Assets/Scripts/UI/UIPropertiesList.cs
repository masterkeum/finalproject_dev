using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPropertiesList : UIBase
{
    public void Mail()
    {
        UIManager.Instance.ShowUI<UIMail>();
        SoundManager.Instance.PlaySound("NextPageUI_1", 1f);
    }
    public void Notice()
    {
        UIManager.Instance.ShowUI<UINotice>();
        SoundManager.Instance.PlaySound("NextPageUI_1", 1f);
    }
    public void Option()
    {
        UIManager.Instance.ShowUI<UIOption>();
        SoundManager.Instance.PlaySound("ButtonClickUI_1", 1f);
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
        SoundManager.Instance.PlaySound("ButtonClickUI_1", 1f);
    }
}
