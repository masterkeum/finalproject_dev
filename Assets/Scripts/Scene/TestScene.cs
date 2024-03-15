using Gley.Jumpy;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

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
    Pooling objectPool;

    private Player player;
    private GameObject joyStick;
    private GameObject virtualCamera;

    private int stageId;
    List<StageInfoTable> stageMonsterList;


    private float spawnRadius = 50f;

    private void Awake()
    {
        _ = DataManager.Instance;
        _ = GameManager.Instance;
        _ = AccountInfo.Instance;// 사용자 계정 데이터 접근

        virtualCamera = GameObject.Find("Virtual Camera");
        
        objectPool = GetComponent<Pooling>();
        stageId = GameManager.Instance.stageId;
        stageMonsterList = DataManager.Instance.GetStageInfo(stageId);
    }

    private void Start()
    {
        // 스테이지에 맞는 필드 생성
        GenerateLevel();

        // 플레이어 생성
        MakePlayer();
        
        // 버츄얼 카메라 세팅
        VirtualCameraSettiing();
        
        // 플레이 기본 세팅

        // 몬스터 생성
        StartStage();
    }

    private void GenerateLevel()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Level/Level1"));
    }

    private void MakePlayer()
    {
        player = Instantiate(Resources.Load<Player>("Prefabs/Player/Player"));
        player.transform.position = new Vector3(0, 10, 0);
        player.GetComponent<Rigidbody>().constraints =
            RigidbodyConstraints.FreezeRotation;
        joyStick = Instantiate(Resources.Load<GameObject>("Prefabs/Joystick/Joystick"));
        player.GetComponent<Player>().JoyStick(joyStick.GetComponentInChildren<VariableJoystick>());
    }

    private void VirtualCameraSettiing()
    {
        virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }


    private void StartStage()
    {
        // 보스 세팅


        foreach (StageInfoTable monsterData in stageMonsterList)
        {
            StartCoroutine(SpawnEnemyRoutine(monsterData));
        }
    }

    IEnumerator SpawnEnemyRoutine(StageInfoTable monsterData)
    {
        // 프리팹 세팅
        GameObject monster = Resources.Load<GameObject>("Prefabs/Enemy/MushroomA");


        yield return new WaitForSeconds(monsterData.genTimeStart);
        float startTime = Time.time;

        // 몬스터 리젠
        //int spawnedCount = 0;
        while (Time.time - startTime < monsterData.genTimeEnd)
        {
            for (int i = 0; i < monsterData.genAmount; i++)
            {
                //if (spawnedCount >= monsterData.genMax) break;

                // X와 Z 좌표상에서의 랜덤한 각도를 결정 (0에서 360도 사이)
                float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;

                // 각도를 바탕으로 X와 Z 좌표 계산
                float x = Mathf.Cos(randomAngle) * spawnRadius;
                float z = Mathf.Sin(randomAngle) * spawnRadius;
                Vector3 randomPosition = player.transform.position + new Vector3(x, 100f, z);

                // 맵크기에 따른 보정 
                randomPosition.x = Mathf.Clamp(randomPosition.x, -105, 105);
                randomPosition.z = Mathf.Clamp(randomPosition.z, -105, 105);

                Ray ray = new Ray(randomPosition, Vector3.down);
                RaycastHit hit;
                NavMeshHit navhit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (NavMesh.SamplePosition(hit.point, out navhit, spawnRadius, NavMesh.AllAreas))
                    {
                        GameObject go = Instantiate(monster, navhit.position, Quaternion.identity);
                        go.GetComponent<EnemyBaseController>().Init(monsterData.monsterId, monsterData.level, player);
                    }
                }
                else
                {
                    Debug.LogError("없어");
                }

                // TODO : 풀매니저 적용해야됨

                //spawnedCount++;
            }

            yield return new WaitForSeconds(monsterData.genTime);
        }

    }




}
