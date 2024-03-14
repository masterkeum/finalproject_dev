using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletoneBase<GameManager>
{
    [ReadOnly, SerializeField] private string _pidStr;
    private GameObject playerPrefab; // 프리팹 담는것.
    [SerializeField] private Transform playerParent; // 
    private VariableJoystick joyStick;
    [SerializeField] private Transform parentCanvas;
    private GameObject weapon;
    private Player player;
    

    /*
    게임매니저
        진입한 스테이지ID 가지고있게
        
        해당하는 스테이지 필드 생성
        플레이어 생성 -> 완료
        게임 진행 관련 기본 세팅 (타이머, 레벨, 경험치 등 초기화)

        게임 진행 로직
            보스몬스터 생성
            몬스터 생성 시작
            레벨업
            게임 완료 처리

        일시정지 로직
    */

    protected override void Init()
    {
        _pidStr = _pid.ToString();
        base.Init();
        // 초기화
    }

    private void Start()
    {
        MakePlayer();
        MakeWeapon();
    }

    private void MakePlayer()
    {
        var playerRes = Resources.Load<GameObject>("Prefabs/Player/Player");
        playerPrefab = Instantiate(playerRes);
        
        var joyStickRes = Resources.Load<VariableJoystick>("Prefabs/Joystick/VariableJoystick");
        joyStick = Instantiate(joyStickRes, parentCanvas);
        
        playerPrefab.GetComponent<Player>().JoyStick(joyStick);
    }

    private void MakeWeapon()
    {
        var weaponRes = Resources.Load<GameObject>("Prefabs/Weapons/ElementalAuraLife2");
        weapon = Instantiate(weaponRes, playerParent);
    }

}
