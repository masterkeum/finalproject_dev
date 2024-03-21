using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{

    public VariableJoystick joy;
    public float speed;

    private Rigidbody rigid;
    private Animator anim;
    private Vector3 moveVec;
    private PlayerIngameData playerIngameData;

    private int PlayerID;
    private int level;
    private int hp;
    private CharacterInfo characterInfo;

    private bool IsInit = false;

    public int curLevel;
    public int maxLevel;
    public int sliderCurExp;
    public int sliderMaxExp;
    public int curExp;
    public int totalExp;
    public int maxExp;
    public int killCount;
    public int gold;

    public List<SkillTable> activeSkillSlot = new List<SkillTable>();
    public List<SkillTable> passiveSkillSlot = new List<SkillTable>();





    [SerializeField] private Transform projectilePoint;
    // 적
    public LayerMask enemyLayer;
    public float detectionRange = 15f;
    private List<Transform> nearEnemy = new List<Transform>();
    private SkillPool skillPool;

    public virtual void Init(int _player, int _level)
    {
        if (IsInit) return;
        Debug.Log("Player.Init");
        PlayerID = _player;
        level = _level;

        characterInfo = DataManager.Instance.characterInfoDict[PlayerID];

        hp = characterInfo.hp;

        skillPool.CreatePool(transform);
        IsInit = true;
    }


    private void Awake()
    {
        Debug.Log("Player.Awake");
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        playerIngameData = GetComponent<PlayerIngameData>();
        skillPool = GetComponent<SkillPool>();
    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            // 임시방편. 바닥밑으로 떨어지면 위치이동
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }

        SkillRoutine();
    }

    private void FixedUpdate()
    {
        float x = joy.Horizontal;
        float z = joy.Vertical;
        //Debug.Log($"{x}, {z}");

        moveVec = new Vector3(x, 0, z) * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + moveVec);

        if (moveVec.sqrMagnitude == 0)
            return;

        Quaternion dirQuat = Quaternion.LookRotation(moveVec);
        Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
        rigid.MoveRotation(moveQuat);
    }

    private void LateUpdate()
    {
        //anim.SetFloat("Move", moveVec.sqrMagnitude); 
    }

    public void JoyStick(VariableJoystick joy)
    {
        this.joy = joy;
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        hp -= damageAmount;
        if (hp <= 0)
            OnDead();
    }

    void OnDead()
    {
        Debug.Log("플레이어사망. 게임오버UI");
    }


    private void SkillRoutine()
    {
        // 임시
        foreach (SkillTable skill in playerIngameData.activeSkillSlot)
        {
            switch (skill.skillType)
            {
                case "Target":
                    {
                        if (Time.time - skill.lastAttackTime > skill.coolDownTime)
                        {
                            skill.lastAttackTime = Time.time;
                            // 10f 까지 탐색? = 보스 탐지거리
                            // 가까운놈 찾아서 그방향으로 발사 일정거리 가면 사라짐
                            // 발사 방향
                            Vector3 direction = DetectEnemyDirection();
                            // 발사 작동
                            skillPool.GetPoolSkill(skill.skillId, projectilePoint, direction);
                            skillPool.GetPoolFlash(skill.skillId, projectilePoint, direction);
                        }
                    }
                    break;

            }
        }
    }

    private Vector3 DetectEnemyDirection()
    {
        nearEnemy.Clear();
        // TODO: OverlapSphereNonAlloc로 변환가능하면 변환
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, enemyLayer);
        foreach (Collider col in hitColliders)
        {
            nearEnemy.Add(col.transform);
        }

        if (nearEnemy.Count > 0)
        {
            Transform nearestEnemy = GetNearestEnemy();
            Vector3 direction = (nearestEnemy.position - transform.position).normalized;
            direction.y = 0;
            return direction;
        }
        else
        {
            // 근거리 없으면 아무방향으로 발사
            // Unity의 Random 클래스를 사용하여 -1과 1 사이의 랜덤한 값으로 각 축을 설정합니다.
            float randomX = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);

            // Vector3.Normalize 함수를 사용하여 벡터를 정규화합니다.
            Vector3 randomDirection = new Vector3(randomX, 0f, randomZ).normalized;
            return randomDirection;
        }
    }

    // 가장 가까운 적을 찾는 함수
    Transform GetNearestEnemy()
    {
        Transform nearestEnemy = null;
        float nearestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Transform enemy in nearEnemy)
        {
            Vector3 directionToTarget = enemy.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < nearestDistanceSqr)
            {
                nearestDistanceSqr = dSqrToTarget;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

}
