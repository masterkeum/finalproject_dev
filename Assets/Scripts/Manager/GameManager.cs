using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletoneBase<GameManager>
{
    [ReadOnly, SerializeField] private string _pidStr;

    protected override void Init()
    {
        _pidStr = _pid.ToString();
        base.Init();

        // 초기화

    }
}
