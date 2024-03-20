using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletoneBase<UIManager>
{
    //private List<UIBase> popups = new List<UIBase>();
    public Dictionary<string, UIBase> UIDictionary = new Dictionary<string, UIBase>();

    public T ShowUI<T>() where T : UIBase
    {
        string keyString = typeof(T).Name;
        if (UIDictionary.ContainsKey(keyString))
        {
            UIDictionary[keyString].gameObject.SetActive(true);
            return UIDictionary[keyString] as T;
        }
        else
        {
            return ShowUI(keyString) as T;
        }
    }

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

    public UIBase ShowPopupWithPrefab(GameObject prefab, string popupName)
    {
        var obj = Instantiate(prefab);
        return ShowUI(obj, popupName);
    }

    public UIBase ShowUI(GameObject obj, string popupname)
    {
        var popup = obj.GetComponent<UIBase>();
        //popups.Add(popup);
        UIDictionary[popupname] = popup;

        obj.SetActive(true);
        return popup;
    }

    public void Clear()
    {
        UIDictionary.Clear();
    }
}