using Polyperfect.Common;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    Trace,
    Attack,
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

    // 일단 다 넣어놓음
    [Header("Stats")]
    [SerializeField] private int monsterID;
    [SerializeField] private int level;

    private bool IsInit = false;

    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Die = Animator.StringToHash("Die");

    [SerializeField] protected Transform targetPlayerTransform;
    // 몬스터 정보
    protected CharacterInfo characterInfo;
    protected MonsterLevel monsterLevel;

    protected NavMeshAgent navMeshAgent;
    protected Animator animator;

    // 몬스터 상태관리
    private EnemyState enemyState;
    protected float playerDistance;

    protected float sensoryRange; // 보스 탐지영역
    protected float attackRange;
    protected float attackSpeed;
    protected float lastAttackTime;

    protected void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    public virtual void Init(int _monsterID, int _level, Player target)
    {
        //Debug.Log(IsInit);
        if (IsInit) return;

        monsterID = _monsterID;
        level = _level;
        targetPlayerTransform = target.transform;

        characterInfo = DataManager.Instance.characterInfoDict[monsterID];
        monsterLevel = characterInfo.monsterLevelData[level];

        // 몬스터 스탯초기화
        navMeshAgent.speed = characterInfo.moveSpeed;
        navMeshAgent.stoppingDistance = characterInfo.attackRange;
        //Debug.Log(targetPlayerTransform.position);

        // 상태 초기화
        SetState(EnemyState.Trace);

        // 적 루틴 실행
        //StartCoroutine(CheckState());

        IsInit = true;
    }

    private void SetState(EnemyState newState)
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
                    navMeshAgent.isStopped = false;
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

    private void Update()
    {
        playerDistance = DistanceToTarget();

    }

    private void OnDead()
    {
        SetState(EnemyState.Die);

        // 죽으면 풀 반환

    }


    protected float DistanceToTarget()
    {
        // 플레이어와 거리
        return Vector3.Distance(transform.position, targetPlayerTransform.position);
    }


    //protected Vector2 DirectionToTarget()
    //{
    //    // 방향벡터
    //    return (targetPlayerTransform.position - transform.position).normalized;
    //}
}
