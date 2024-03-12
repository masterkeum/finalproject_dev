using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletoneBase<GameManager>
{
    [ReadOnly, SerializeField] private string _pidStr;
    private GameObject player;
    [SerializeField] private Transform playerParent;
    private VariableJoystick joyStick;
    [SerializeField] private Transform parentCanvas;
    

    protected override void Init()
    {
        _pidStr = _pid.ToString();
        base.Init();
        // 초기화
    }

    private void Start()
    {
        MakePlayer();
    }

    private void MakePlayer()
    {
        player = Instantiate(Resources.Load<GameObject>("Prefabs/Player/man_casual_shorts"), playerParent);
        joyStick = Instantiate(Resources.Load<VariableJoystick>("Prefabs/Joystick/VariableJoystick"), parentCanvas);
        player.GetComponent<Player>().JoyStick(joyStick);
    }
}
