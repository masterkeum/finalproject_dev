using Gley.Jumpy;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    None,
    Idle, // 보스만 가지고 있는
    Wander, // 보스만 가지고 있는
    Trace,
    Attack,
    Flee, // 보스 멀리 풀링되는경우 원위치
    Die,
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
    protected CharacterInfo characterInfo;
    protected MonsterLevel monsterLevel;

    protected NavMeshAgent navMeshAgent;
    protected Collider capsuleCollider;
    protected Animator animator;

    protected float playerDistance;

    protected float lastAttackTime;// 마지막 공격 시간

    [SerializeField] private int hp;
    protected int damage;
    private DropCoin point;

    protected void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        capsuleCollider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();
        player = GameManager.Instance.player;
    }

    public virtual void Init(int _monsterID, int _level, Player target)
    {
        if (IsInit) return;
        monsterID = _monsterID;
        level = _level;
        targetPlayerTransform = target.transform;

        characterInfo = DataManager.Instance.characterInfoDict[monsterID];
        monsterLevel = characterInfo.monsterLevelData[level];

        // 몬스터 스탯초기화
        hp = characterInfo.hp;
        damage = characterInfo.attackPower;

        navMeshAgent.speed = characterInfo.moveSpeed;
        navMeshAgent.stoppingDistance = characterInfo.attackRange;

        capsuleCollider.enabled = true;
        navMeshAgent.isStopped = false;

        IsInit = true;
    }

    protected void SetState(EnemyState newState)
    {
        enemyState = newState;
        switch (enemyState)
        {
            case EnemyState.Trace:
                {
                    navMeshAgent.isStopped = false;
                }
                break;
            case EnemyState.Attack:
                {
                    navMeshAgent.isStopped = true;
                }
                break;
            case EnemyState.Die:
                {
                    navMeshAgent.isStopped = true;
                }
                break;
        }

        //animator.speed = agent.speed / walkSpeed;
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        hp -= damageAmount;
        if (hp <= 0)
            OnDead();
    }

    protected virtual void OnDead()
    {
        SetState(EnemyState.Die);
        // 이동 정지
        capsuleCollider.enabled = false;
        navMeshAgent.isStopped = true;

        animator.SetTrigger(Die);
        StartCoroutine(Remove());

        // 보상
        point = Instantiate(Resources.Load<DropCoin>("Prefabs/Coin/RupeeGold"), transform.position + Vector3.up * 2, Quaternion.identity);
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
