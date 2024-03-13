using System;
using System.Collections;
using UnityEngine;

public class GameManager : SingletoneBase<GameManager>
{
    [ReadOnly, SerializeField] private string _pidStr;
    private GameObject player;
    private VariableJoystick joyStick;


    [SerializeField] private Transform playerParent;
    [SerializeField] private Transform parentCanvas;


    // 나중에는 시리얼라이즈 해제
    [SerializeField] private int stageId = 0;

    /*
    게임매니저
        진입한 스테이지ID 가지고있게
        
        해당하는 스테이지 필드 생성
        플레이어 생성
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
        // 필드 생성

        // 플레이어 생성
        MakePlayer();

        // 플레이 기본 세팅

        // 몬스터 생성
        StartStage(stageId);
    }


    private void MakePlayer()
    {
        player = Instantiate(Resources.Load<GameObject>("Prefabs/Player/man_casual_shorts"), playerParent);
        joyStick = Instantiate(Resources.Load<VariableJoystick>("Prefabs/Joystick/VariableJoystick"), parentCanvas);
        player.GetComponent<Player>().JoyStick(joyStick);
    }

    private void StartStage(int stageId)
    {
        // 보스 세팅
        StartCoroutine(GenMonster());
    }

    IEnumerator GenMonster()
    {

        yield return null;

    }
}
