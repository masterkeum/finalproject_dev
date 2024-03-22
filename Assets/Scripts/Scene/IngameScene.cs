using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public struct playeringameinfo
{
    public int attackPower;
    public int addAttackPower;

    public float sensoryRange;
    public float attackRange;
    public float attackSpeed;
    public float moveSpeed;

    //
    public int curLevel;
    public int maxLevel;
    public int sliderCurExp;
    public int sliderMaxExp;
    public int curExp;
    public int totalExp;
    public int maxExp;

    public int killCount;
    public int gold;
    public int skillpoint;
}

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
    private GameObject joyStick;
    private GameObject virtualCamera;

    private InGameHUD inGameHUD;

    private int stageId;
    List<StageInfoTable> stageMonsterList;

    private float spawnRadius = 30f; // TODO: 30은 돼야 화면밖인듯. 기본 설정도 나중에 데이터로

    private void Awake()
    {
        _ = DataManager.Instance;
        GameManager.Instance.Clear();
        //_ = AccountInfo.Instance;// 사용자 계정 데이터 접근

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


        // HUD 생성
        inGameHUD = UIManager.Instance.ShowUI<InGameHUD>();

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
        player.transform.position = new Vector3(0, 0.5f, 0);
        player.Init(10000001, 1);

        // Rigidbody playerRigid = player.GetComponent<Rigidbody>();
        // playerRigid.constraints = RigidbodyConstraints.FreezeRotation;

        joyStick = Instantiate(Resources.Load<GameObject>("Prefabs/Joystick/Joystick"));
        player.JoyStick(joyStick.GetComponentInChildren<VariableJoystick>());

        // 쏴주는 역할
        GameManager.Instance.SetPlayer(player);
    }

    private void VirtualCameraSettiing()
    {
        virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }

    private void StartStage()
    {
        foreach (StageInfoTable monsterData in stageMonsterList)
        {
            StartCoroutine(SpawnEnemyRoutine(monsterData));
        }
    }

    IEnumerator SpawnEnemyRoutine(StageInfoTable monsterData)
    {
        // 프리팹 세팅
        CharacterInfo monsterInfo = DataManager.Instance.GetCharacterInfo(monsterData.monsterId);
        GameObject monster = Resources.Load<GameObject>(monsterInfo.prefabFile);

        if (monsterData.genPosVecter3.Length > 0)
        {
            // 좌표있으면 다른것 무시하고 단발성 생성
            GameObject go = Instantiate(monster, new Vector3(monsterData.genPosVecter3[0], monsterData.genPosVecter3[1], monsterData.genPosVecter3[2]), Quaternion.identity);
            go.GetComponent<EnemyBaseController>().Init(monsterData.monsterId, monsterData.level, player);

            yield break;
        }

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
                float randomAngle = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad;

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
                    Debug.LogWarning("몬스터 생성할 위치 못찾음.");
                }

                // TODO : 풀매니저 적용해야됨

                //spawnedCount++;
            }

            yield return new WaitForSeconds(monsterData.genTime);
        }

    }

}
