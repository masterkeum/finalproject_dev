using Gley.Jumpy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
    private GameObject joyStick;


    private void Awake()
    {
        _ = DataManager.Instance;
        _ = GameManager.Instance;
        _ = AccountInfo.Instance;// 사용자 계정 데이터 접근
    }

    private void Start()
    {
        // 스테이지에 맞는 필드 생성
        GenerateLevel();

        // 플레이어 생성
        MakePlayer();
        // 플레이 기본 세팅

        // 몬스터 생성
        StartStage(GameManager.Instance.stageId);
    }

    private void GenerateLevel()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Level/Level1"));
    }

    private void MakePlayer()
    {
        player = Instantiate(Resources.Load<GameObject>("Prefabs/Player/man_casual_shorts"), GameManager.Instance.playerParent);
        player.transform.position = new Vector3(0, 10, 0);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        joyStick = Instantiate(Resources.Load<GameObject>("Prefabs/Joystick/Joystick"));
        player.GetComponent<Player>().JoyStick(joyStick.GetComponentInChildren<VariableJoystick>());
    }


    private void StartStage(int stageId)
    {
        // 보스 세팅
        StartCoroutine(GenMonster());
    }

    IEnumerator GenMonster()
    {
        // 몬스터 리젠
        yield return null;

    }




}
