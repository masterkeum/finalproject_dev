using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    [SerializeField] private GameObject chestMonster;
    private Animator chestAnim;

    protected static readonly int OnLoad = Animator.StringToHash("OnLoad");
    protected static readonly int Ready = Animator.StringToHash("Ready");
    private UILoading loadingUI;

    private void Awake()
    {
        _ = DataManager.Instance;
        _ = GameManager.Instance;
        UIManager.Instance.Clear();
    }

    private void Start()
    {
        GenObject();
        loadingUI = UIManager.Instance.ShowUI<UILoading>();
    }

    private void Update()
    {
        if (loadingUI.IsReady)
        {
            chestAnim.SetBool(Ready, true);
        }
    }

    private void GenObject()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/IntroScene/IntroField"));
        chestMonster = Instantiate(Resources.Load<GameObject>("Prefabs/IntroScene/IntroChestMonster"));
        chestAnim = chestMonster.GetComponent<Animator>();

        chestAnim.SetBool(OnLoad, true);
    }
}

