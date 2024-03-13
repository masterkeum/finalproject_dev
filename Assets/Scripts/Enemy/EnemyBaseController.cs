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

    private EnemyState enemyState;

    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Die = Animator.StringToHash("Die");

    private float walkSpeed;
    private float runSpeed;


    private Transform targetPlayerTransform;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    protected virtual void Awake()
    {
        // 플레이어 타겟팅
        //targetPlayerTransform = GameManager.Instance.Player.transform;

        navMeshAgent = GetComponent<NavMeshAgent>();
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


    /*



    private void SetState(EnemyState newState)
    {
        enemyState = newState;
        switch (enemyState)
        {
            // 생성되자마자 걷는 애니. 항상 이동중임
            case EnemyState.Trace:
                {
                    // 플레이어 추적
                    //navMeshAgent.speed = walkSpeed;
                    //navMeshAgent.isStopped = true;
                }
                break;
            case EnemyState.Attack:
                {
                    // 공격
                    //navMeshAgent.speed = walkSpeed;
                    //navMeshAgent.isStopped = false;
                }
                break;
            case EnemyState.Die:
                {
                    // 사망
                    //navMeshAgent.speed = runSpeed;
                    //navMeshAgent.isStopped = false;
                    navMeshAgent.isStopped = true;
                }
                break;
        }

        //animator.speed = navMeshAgent.speed / walkSpeed;
    }

    private IEnumerator CheckState()
    {
        while (enemyState != EnemyState.Die)
        {
            navMeshAgent.SetDestination(targetPlayerTransform.position);
            //if (distance <= this.attackDistance) //공격 사거리
            //{
            //    this.state = eState.IDLE;
            //    yield return null;
            //    this.state = eState.ATTACK;//공격상태로 변경
            //}

        }

        yield return new WaitForSeconds(2f);

        //상태 갱신 코루틴 함수: 일정 간격으로 몬스터의 행동상태 체크
        //while (enemyState != EnemyState.Die)
        //{
        // yield return new WaitForSeconds(0.3f);
        //    if (enemyState == EnemyState.DIE) yield break;
        //    float distance = Vector3.Distance(this.transform.position, this.playerTrans.position);
        //    // Debug.Log(distance);
        //    //Debug.Log(this.state);
        //    if (distance <= this.traceDistance)//추적 사거리에 들어왔다면
        //    {
        //        this.state = eState.TRACE;//추적상태로 변경
        //        if (distance <= this.attackDistance) //공격 사거리에 들어왔다면
        //        {
        //            this.state = eState.IDLE;
        //            yield return null;
        //            this.state = eState.ATTACK;//공격상태로 변경
        //        }
        //    }
        //    else//멈춤
        //    {
        //        this.state = eState.IDLE;
        //    }
        //}

        // 네브메쉬를 제거하고 움직이게 수정필요

    }

    private void moveUpdate()
    {

        navMeshAgent.destination = targetPlayerTransform.position; // 타겟으로 이동
    }

    */


    protected float DistanceToTarget()
    {
        // 플레이어와 거리
        return Vector3.Distance(transform.position, targetPlayerTransform.position);
    }


    protected Vector2 DirectionToTarget()
    {
        // 방향벡터
        return (targetPlayerTransform.position - transform.position).normalized;
    }
}
