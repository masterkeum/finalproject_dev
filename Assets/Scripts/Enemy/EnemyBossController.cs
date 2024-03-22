using UnityEngine;
using UnityEngine.AI;

public class EnemyBossController : EnemyBaseController
{
    private Vector3 defaultPos;
    private NavMeshPath path;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;


    private Vector3 gizmoDestination;


    public override void Init(int _monsterID, int _level, Player target)
    {
        base.Init(_monsterID, _level, target);

        defaultPos = transform.position;

        minWanderDistance = 8f;
        maxWanderDistance = 10f;
        minWanderWaitTime = 1f;
        maxWanderWaitTime = 3f;

        path = new NavMeshPath();
        SetState(EnemyState.Wander);
    }

    private void Update()
    {
        playerDistance = DistanceToTarget();
        switch (enemyState)
        {
            case EnemyState.Idle: WanderUpdate(); break;
            case EnemyState.Wander: WanderUpdate(); break;
            case EnemyState.Trace: TraceUpdate(); break;
            case EnemyState.Attack: AttackUpdate(); break;
            case EnemyState.Flee: FleeUpdate(); break;
            case EnemyState.Die: OnDead(); break;

            default:
                SetState(EnemyState.Flee); break;
        }
    }

    private void WanderUpdate()
    {
        //플레이어 거리가 감지 범위 내로 들어왔을 경우
        if (playerDistance < characterInfo.sensoryRange)
        {
            SetState(EnemyState.Trace);
        }

        if (enemyState == EnemyState.Wander && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            SetState(EnemyState.Idle);
            animator.SetBool("IsWalking", false);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }
    }
    private void WanderToNewLocation()
    {
        if (enemyState != EnemyState.Idle)
        {
            return;
        }
        SetState(EnemyState.Wander);
        animator.SetBool("IsWalking", true);
        navMeshAgent.CalculatePath(GetWanderLocation(), path);
        navMeshAgent.SetPath(path);
    }

    private Vector3 GetWanderLocation()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(defaultPos + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        gizmoDestination = hit.position;
        return hit.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(gizmoDestination, Vector3.up);
    }

    private void TraceUpdate()
    {
        // 추적 거리 벗어나면 
        if (playerDistance > characterInfo.sensoryRange)
        {
            navMeshAgent.CalculatePath(defaultPos, path); // 원위치
            navMeshAgent.SetPath(path);
            navMeshAgent.speed = characterInfo.moveSpeed * 2;
            animator.speed = 2;
            animator.SetBool(IsWalking, true);
            SetState(EnemyState.Flee);
        }
        else
        {
            navMeshAgent.CalculatePath(targetPlayerTransform.position, path);
            navMeshAgent.SetPath(path);
            animator.SetBool(IsWalking, true);
        }

        if (playerDistance < characterInfo.attackRange)
        {
            SetState(EnemyState.Attack);
        }
    }

    private void AttackUpdate()
    {
        // 공격 범위 벗어나면
        if (playerDistance > characterInfo.attackRange)
        {
            SetState(EnemyState.Trace);
        }
        transform.Rotate(DirectionToTarget());
        if (Time.time - lastAttackTime > characterInfo.attackSpeed)
        {
            lastAttackTime = Time.time;
            //PlayerController.instance.GetComponent<IDamagable>().TakePhysicalDamage(damage); // 데미지 처리
            animator.SetTrigger(Attack);
        }
    }

    private void FleeUpdate()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            navMeshAgent.speed = characterInfo.moveSpeed;
            animator.speed = 1;
            animator.SetBool(IsWalking, false);
            SetState(EnemyState.Wander);
        }
    }

    protected override void OnDead()
    {
        base.OnDead();

        Debug.Log("게임 클리어");
        UIManager.Instance.ShowUI<UIGameClear>();
        Time.timeScale = 0f;
    }
}
