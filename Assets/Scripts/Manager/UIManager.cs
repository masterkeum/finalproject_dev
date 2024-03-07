using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : SingletoneBase<UIManager>
{
    private List<UIBase> popups = new List<UIBase>();

    public UIBase ShowUI(string uiName)
    {
        var obj = Resources.Load("Prefabs/UI/" + uiName, typeof(GameObject)) as GameObject;
        if (!obj)
        {
            Debug.LogWarning($"Failed to ShowPopup({uiName})");
            return null;
        }
        return ShowPopupWithPrefab(obj, uiName);
    }

    public T ShowUI<T>() where T : UIBase
    {
        return ShowUI(typeof(T).Name) as T;
    }

    public UIBase ShowPopupWithPrefab(GameObject prefab, string popupName)
    {
        var obj = Instantiate(prefab);
        return ShowUI(obj, popupName);
    }

    public UIBase ShowUI(GameObject obj, string popupname)
    {
        var popup = obj.GetComponent<UIBase>();
        popups.Insert(0, popup);

        obj.SetActive(true);
        return popup;
    }
}