using System.Collections;
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

    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Die = Animator.StringToHash("Die");

    // 몬스터 정보
    protected CharacterInfo characterInfo;
    protected MonsterLevel monsterLevel;

    private EnemyState enemyState;


    private float walkSpeed;
    private float runSpeed;


    //private Transform targetPlayerTransform;
    private Animator animator;

    protected virtual void Awake()
    {
        characterInfo = DataManager.Instance.characterInfoDict[monsterID];
        monsterLevel = characterInfo.monsterLevelData[level];

        //navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        Init();
    }

    private void Init()
    {
        // 상태 초기화
        //SetState(EnemyState.Trace);

        // 적 루틴 실행
        //StartCoroutine(CheckState());
    }

    private void Update()
    {

    }

    //protected float DistanceToTarget()
    //{
    //    // 플레이어와 거리
    //    return Vector3.Distance(transform.position, targetPlayerTransform.position);
    //}


    //protected Vector2 DirectionToTarget()
    //{
    //    // 방향벡터
    //    return (targetPlayerTransform.position - transform.position).normalized;
    //}
}
