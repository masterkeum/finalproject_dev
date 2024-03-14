using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    /*
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

    private GameObject player;
    private VariableJoystick joyStick;


    private void Awake()
    {
        _ = DataManager.Instance;
        _ = GameManager.Instance;
        _ = AccountInfo.Instance;// 사용자 계정 데이터 접근
    }

    private void Start()
    {
        // 스테이지에 맞는 필드 생성

        // 플레이어 생성
        MakePlayer();
        // 플레이 기본 세팅

        // 몬스터 생성
        StartStage(GameManager.Instance.stageId);
    }


    private void MakePlayer()
    {
        player = Instantiate(Resources.Load<GameObject>("Prefabs/Player/man_casual_shorts"), GameManager.Instance.playerParent);
        joyStick = Instantiate(Resources.Load<VariableJoystick>("Prefabs/Joystick/VariableJoystick"), GameManager.Instance.parentCanvas);
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
