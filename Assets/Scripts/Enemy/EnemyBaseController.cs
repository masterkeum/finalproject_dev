using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EnemyState
{
    None,
    Idle, // 보스만 가지고 있는
    Wander, // 보스만 가지고 있는
    Trace,
    Attack,
    Flee, // 보스 멀리 풀링되는경우 원위치
    Die,
    Freeze,// 멈춤
}

public class EnemyBaseController : MonoBehaviour
{
    /*
    적 베이스 스크립트
        플레이어 타겟팅

    거리가 너무 멀어지면 풀에 반환

    적이 할일
        플레이어 추적/이동
        공격
        사망
        보상

    보스
        일정 영역 플레이어 공격
        페이즈
        사망
        보상
    */

    // 몬스터 상태관리
    [ReadOnly, SerializeField] protected EnemyState enemyState;

    // 일단 다 넣어놓음
    [Header("Stats")]
    [SerializeField] private int monsterID;
    [SerializeField] private int level;

    protected float trackingTerm = 1f;
    private bool IsInit = false;

    // 애니메이션
    protected static readonly int Attack = Animator.StringToHash("Attack");
    protected static readonly int Hit = Animator.StringToHash("Hit");
    protected static readonly int Die = Animator.StringToHash("Die");
    protected static readonly int Attack2 = Animator.StringToHash("Attack2");
    protected static readonly int Taunting = Animator.StringToHash("Taunting");
    protected static readonly int IsWalking = Animator.StringToHash("IsWalking");

    protected Player player;
    [SerializeField] protected Transform targetPlayerTransform;
    // 몬스터 정보
    public CharacterInfo characterInfo;
    protected MonsterLevel monsterLevel;

    protected NavMeshAgent navMeshAgent;
    protected Rigidbody rigidBody; // 리지드바디
    protected Collider capsuleCollider;
    protected Animator animator;

    protected float playerDistance;

    protected float lastAttackTime;// 마지막 공격 시간
    private Slider hpGuageSlider;
    public GameObject hudDamageText;
    public Transform hudPos;

    // [SerializeField] private int hp;
    protected int damage;
    protected int currentHp;
    protected int maxHp;
    private DropCoin point;

    float knockBackTime;

    protected void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponentInChildren<Animator>();
        hpGuageSlider = GetComponentInChildren<Slider>();
        player = GameManager.Instance.player;
    }

    public virtual void Init(int _monsterID, int _level, Player target)
    {
        if (IsInit) return;
        monsterID = _monsterID;
        level = _level;
        targetPlayerTransform = target.transform;
        lastAttackTime = Time.time;

        characterInfo = DataManager.Instance.characterInfoDict[monsterID];
        monsterLevel = characterInfo.monsterLevelData[level];

        // 몬스터 스탯초기화
        maxHp = characterInfo.hp + monsterLevel.addHP;
        damage = characterInfo.attackPower + monsterLevel.addAP;
        currentHp = maxHp;

        navMeshAgent.speed = characterInfo.moveSpeed;
        navMeshAgent.stoppingDistance = characterInfo.attackRange / 2;

        capsuleCollider.enabled = true;
        navMeshAgent.isStopped = false;

        IsInit = true;
    }

    protected void SetState(EnemyState newState)
    {
        enemyState = newState;
        switch (enemyState)
        {
            case EnemyState.Idle:
            case EnemyState.Wander:
            case EnemyState.Trace:
                {
                    animator.speed = 1;
                    navMeshAgent.isStopped = false;
                }
                break;
            case EnemyState.Attack:
                {
                    animator.speed = 2;
                    navMeshAgent.isStopped = true;
                }
                break;
            case EnemyState.Flee:
                {
                    animator.speed = 2;
                    navMeshAgent.isStopped = false;
                }
                break;
            case EnemyState.Die:
            case EnemyState.Freeze:
                {
                    navMeshAgent.isStopped = true;
                }
                break;
            default:
                animator.speed = 1;
                break;
        }

        //animator.speed = agent.speed / walkSpeed;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHp -= damageAmount;
        float per = (float)currentHp / maxHp;
        hpGuageSlider.value = per;
        //Debug.Log("몬스터 현재 HP" + per);
        if (currentHp <= 0)
            OnDead();

        GameObject hudText = Instantiate(Resources.Load<GameObject>("Prefabs/UI/DamageText")); // 생성할 텍스트 오브젝트
        //Debug.Log("데미지텍스트 프리팹 " + hudText);
        hudText.transform.position = hudPos.position; // 표시될 위치
        hudText.GetComponentInChildren<DamageText>().Init(damageAmount, Color.white);
    }

    public void Knockback(float knockBackTerm, float startDelay, Vector3 knockbackDirection, float knockBackForce)
    {
        if (Time.time - knockBackTime > knockBackTerm && currentHp > 0)
        {
            knockBackTime = Time.time;
            StartCoroutine(KnockbackRountine(startDelay, knockbackDirection, knockBackForce));
        }
    }

    private IEnumerator KnockbackRountine(float startDelay, Vector3 knockbackDirection, float knockBackForce)
    {
        navMeshAgent.isStopped = true;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidBody.AddForce(knockbackDirection * knockBackForce, ForceMode.Impulse);
        yield return new WaitForSeconds(startDelay);
        rigidBody.velocity = Vector3.zero;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        if (gameObject.activeSelf)
            navMeshAgent.isStopped = false;
    }

    protected virtual void OnDead()
    {
        SetState(EnemyState.Die);
        // 이동 정지
        capsuleCollider.enabled = false;
        navMeshAgent.isStopped = true;

        animator.SetTrigger(Die);
        StartCoroutine(Remove());

        Vector3 genPos = transform.position;
        genPos.y = Mathf.Max(genPos.y, 0) + 2f;
        // 보상
        point = Instantiate(Resources.Load<DropCoin>("Prefabs/DropItem/RupeeGold"), genPos, Quaternion.identity);
        point.Init(monsterLevel.gold, monsterLevel.exp);

        // UI
        player.AddKillCount();
        GameManager.Instance.UpdateUI();
    }


    protected float DistanceToTarget()
    {
        // 플레이어와 거리
        return Vector3.Distance(transform.position, targetPlayerTransform.position);
    }

    protected Vector3 DirectionToTarget()
    {
        // 방향벡터
        return (targetPlayerTransform.position - transform.position).normalized;
    }

    private IEnumerator Remove()
    {
        yield return new WaitForSeconds(2);
        // TODO : 몬스터 풀로 변경. 죽으면 풀 반환
        Destroy(gameObject);
        yield break;
    }

}
