using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    private void Awake()
    {
        _ = GameManager.Instance;
        //_ = DataManager.Instance;
    }

    private void Start()
    {
        UIManager.Instance.ShowUI<UILoading>();
    }
}
