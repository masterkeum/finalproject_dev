using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class IngameScene : MonoBehaviour
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
    private GameObject bossMonster;
    private GameObject joyStick;
    private GameObject virtualCamera;
    public Light sunLight;

    private InGameHUD inGameHUD;

    private int stageId;
    List<StageInfoTable> stageMonsterList;

    private float spawnRadius = 30f; // TODO: 30은 돼야 화면밖인듯. 기본 설정도 나중에 데이터로

    private void Awake()
    {
        Debug.Log("IngameScene.Awake");
        Time.timeScale = 1.0f;
        _ = DataManager.Instance;
        _ = SoundManager.Instance;
        GameManager.Instance.Clear();
        UIManager.Instance.Clear();
        SoundManager.Instance.ChangeBackGroundMusic(Resources.Load<AudioClip>("Audio/Music/rhythm_factory"), SoundManager.Instance.musicAudioSource.volume);

        virtualCamera = GameObject.Find("Virtual Camera");
        objectPool = GetComponent<Pooling>();
        stageId = GameManager.Instance.stageId;
        stageMonsterList = DataManager.Instance.GetStageInfo(stageId);
    }

    private void Start()
    {
        Debug.Log("IngameScene.Start");
        GameManager.Instance.InGameSceneProcess();
        SoundManager.Instance.CreateBattleAudioSource();

        // 스테이지에 맞는 필드 생성
        GenerateLevel(stageId);
        SetLight(stageId);

        // 플레이어 생성
        MakePlayer(DataManager.Instance._InitParam["StartCharacterId"]);
        // 버츄얼 카메라 세팅
        VirtualCameraSetting();

        // HUD 생성
        inGameHUD = UIManager.Instance.ShowUI<InGameHUD>();

        // 몬스터 생성
        StartStage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.ShowUI<UIPause>();
            ++UIManager.Instance.popupUICount;
        }
    }

    private void GenerateLevel(int stageId)
    {
        GameObject navMeshSurface = Instantiate(Resources.Load<GameObject>(DataManager.Instance.stageListDict[stageId].levelPrefab));
        navMeshSurface.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    private void MakePlayer(int playerId)
    {
        player = Instantiate(Resources.Load<Player>(DataManager.Instance.characterInfoDict[playerId].prefabFile));
        player.transform.position = new Vector3(0, 0.5f, 0);
        player.Init(playerId, 1);

        // Rigidbody playerRigid = player.GetComponent<Rigidbody>();
        // playerRigid.constraints = RigidbodyConstraints.FreezeRotation;
        joyStick = Instantiate(Resources.Load<GameObject>("Prefabs/Joystick/Joystick"));
        player.JoyStick(joyStick.GetComponentInChildren<VariableJoystick>());

        // 쏴주는 역할
        GameManager.Instance.SetPlayer(player);
    }

    private void VirtualCameraSetting()
    {
        virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }

    private void StartStage()
    {
        foreach (StageInfoTable monsterData in stageMonsterList)
        {
            switch (monsterData.genType)
            {
                case GenType.Fixed:
                    {
                        StartCoroutine(SpawnFixedEnemyRoutine(monsterData));
                    }
                    break;
                case GenType.Circle:
                    {
                        StartCoroutine(SpawnCircleEnemyRoutine(monsterData));
                    }
                    break;
                case GenType.Random:
                default:
                    {
                        StartCoroutine(SpawnRandomEnemyRoutine(monsterData));
                    }
                    break;
            }
        }
    }

    IEnumerator SpawnFixedEnemyRoutine(StageInfoTable monsterData)
    {
        // 프리팹 세팅
        CharacterInfo monsterInfo = DataManager.Instance.GetCharacterInfo(monsterData.monsterId);
        GameObject monster = Resources.Load<GameObject>(monsterInfo.prefabFile);

        if (monsterData.genPosVecter3 != null)
        {
            if (monsterData.genPosVecter3.Length > 0)
            {
                // 좌표있으면 다른것 무시하고 단발성 생성
                GameObject go = Instantiate(monster, new Vector3(monsterData.genPosVecter3[0], monsterData.genPosVecter3[1], monsterData.genPosVecter3[2]), Quaternion.identity);
                go.GetComponent<EnemyBaseController>().Init(monsterData.monsterId, monsterData.level, player);

                if (monsterInfo.characterType == CharacterType.BossMonster)
                {
                    // 보스만 타겟팅
                    player.AddTarget(go);
                }
                yield break;
            }
        }
    }

    IEnumerator SpawnCircleEnemyRoutine(StageInfoTable monsterData)
    {
        // 프리팹 세팅
        CharacterInfo monsterInfo = DataManager.Instance.GetCharacterInfo(monsterData.monsterId);
        GameObject monster = Resources.Load<GameObject>(monsterInfo.prefabFile);

        yield return new WaitForSeconds(monsterData.genTimeStart);
        //Debug.Log($"{monsterData.monsterId} lv.{monsterData.level} : {monsterInfo.name}");
        float startTime = Time.time;

        // 몬스터 리젠
        //int spawnedCount = 0;
        while (Time.time - startTime < monsterData.genTimeEnd)
        {
            float monsterAngle = 360f / monsterData.genAmount;
            float startAngle = UnityEngine.Random.Range(0f, monsterAngle);

            for (int i = 0; i < monsterData.genAmount; i++)
            {
                float radAngle = (startAngle + i * monsterAngle) * Mathf.Deg2Rad;

                // 각도를 바탕으로 X와 Z 좌표 계산
                float x = Mathf.Cos(radAngle) * spawnRadius;
                float z = Mathf.Sin(radAngle) * spawnRadius;
                Vector3 randomPosition = player.transform.position + new Vector3(x, 200f, z);

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
                    Debug.LogWarning("몬스터 생성할 위치 못찾음.");
                }
            }
            yield return new WaitForSeconds(monsterData.genTime);
        }
    }

    IEnumerator SpawnRandomEnemyRoutine(StageInfoTable monsterData)
    {
        // 프리팹 세팅
        CharacterInfo monsterInfo = DataManager.Instance.GetCharacterInfo(monsterData.monsterId);
        GameObject monster = Resources.Load<GameObject>(monsterInfo.prefabFile);

        yield return new WaitForSeconds(monsterData.genTimeStart);
        //Debug.Log($"{monsterData.monsterId} lv.{monsterData.level} : {monsterInfo.name}");
        float startTime = Time.time;

        // 몬스터 리젠
        //int spawnedCount = 0;
        while (Time.time - startTime < monsterData.genTimeEnd)
        {
            for (int i = 0; i < monsterData.genAmount; i++)
            {
                // X와 Z 좌표상에서의 랜덤한 각도를 결정 (0에서 360도 사이)
                float randomAngle = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad;

                // 각도를 바탕으로 X와 Z 좌표 계산
                float x = Mathf.Cos(randomAngle) * spawnRadius;
                float z = Mathf.Sin(randomAngle) * spawnRadius;
                Vector3 randomPosition = player.transform.position + new Vector3(x, 200f, z);

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
                    Debug.LogWarning("몬스터 생성할 위치 못찾음.");
                }
            }
            yield return new WaitForSeconds(monsterData.genTime);
        }
    }


    #region Light
    private void SetLight(int stageId)
    {
        switch (stageId % 5)
        {
            case 2:
                SetEveningLighting();
                break;
            case 3:
                SetDawnLighting();
                break;
            case 4:
                SetMorningLighting();
                break;
            case 1:
            default:
                SetAfternoonLighting();
                break;
        }
    }

    private void SetDawnLighting()
    {
        // 색상을 점차 밝아지는 향상된 주황색으로 변경
        sunLight.color = Color.Lerp(new Color(0.3f, 0.2f, 0.1f), new Color(1f, 0.96f, 0.86f), Mathf.PingPong(Time.time, 1));

        // 낮은 Kelvin 값 (따뜻한 색조)부터 높은 Kelvin 값 (차가운 색조)까지 변화
        float temperature = Mathf.Lerp(2000f, 3500f, Mathf.PingPong(Time.time, 1));
        sunLight.colorTemperature = temperature;
        sunLight.intensity = Mathf.Lerp(0.2f, 1.2f, Mathf.PingPong(Time.time, 1)); // 밝기도 점차 증가
        sunLight.transform.rotation = Quaternion.Euler(20f, 30f, 0f); // 적절한 방향
    }

    private void SetMorningLighting()
    {
        sunLight.color = new Color(1f, 0.96f, 0.86f); // 따뜻한 색조
        sunLight.colorTemperature = 3500f; // 중간 Kelvin 값
        sunLight.intensity = 1.2f; // 밝은 강도
        sunLight.transform.rotation = Quaternion.Euler(50f, 30f, 0f); // 적절한 방향
    }

    private void SetAfternoonLighting()
    {
        sunLight.color = new Color(1f, 1f, 1f); // 중간 색조
        sunLight.colorTemperature = 5500f; // 높은 Kelvin 값
        sunLight.intensity = 1.0f; // 중간 강도
        sunLight.transform.rotation = Quaternion.Euler(90f, 0f, 0f); // 적절한 방향
    }

    private void SetEveningLighting()
    {
        sunLight.color = new Color(0.8f, 0.5f, 0.3f); // 어두운 색조
        sunLight.colorTemperature = 3500f; // 중간 Kelvin 값
        sunLight.intensity = 0.6f; // 어두운 강도
        sunLight.transform.rotation = Quaternion.Euler(160f, -30f, 0f); // 적절한 방향
    }

    #endregion
}
