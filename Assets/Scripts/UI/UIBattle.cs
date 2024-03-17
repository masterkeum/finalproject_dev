using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBattle : UIBase
{
    public void OnClickStageSelect()
    {
        UIManager.Instance.ShowUI<UIStageSelect>();
    }
    public void OnClickListButton()
    {
        UIManager.Instance.ShowUI<UIPropertiesList>();
    }
}
