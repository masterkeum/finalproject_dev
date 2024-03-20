using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    private void Awake()
    {
        _ = DataManager.Instance;
        UIManager.Instance.Clear();
    }

    private void Start()
    {
        UIManager.Instance.ShowUI<UILoading>();
    }
}
