using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletoneBase<GameManager>
{
    [ReadOnly, SerializeField] private string _pidStr;


    public Transform Player { get; private set; }


    protected override void Init()
    {
        _pidStr = _pid.ToString();
        base.Init();

        // 초기화

        // TODO : 플레이어 생성. 생성 후 player에 넣어주기

    }



}
