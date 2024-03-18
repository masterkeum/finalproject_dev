using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeController : EnemyBaseController
{
    private NavMeshPath path;

    public override void Init(int _monsterID, int _level, Player target)
    {
        base.Init(_monsterID, _level, target);

        path = new NavMeshPath();
        SetState(EnemyState.Trace);
    }
    // 상태 초기화

    private void Update()
    {
        playerDistance = DistanceToTarget();
        switch (enemyState)
        {
            case EnemyState.Trace: TraceUpdate(); break;
            case EnemyState.Attack: AttackUpdate(); break;
            case EnemyState.Die: OnDead(); break;
            default:
                SetState(EnemyState.Trace); break;
        }

    }

    private void TraceUpdate()
    {
        if (playerDistance < characterInfo.attackRange)
        {
            SetState(EnemyState.Attack);
        }

        navMeshAgent.CalculatePath(targetPlayerTransform.position, path);
        navMeshAgent.SetPath(path);
    }

    private void AttackUpdate()
    {
        if (playerDistance > characterInfo.attackRange)
        {
            SetState(EnemyState.Trace);
        }

        // 공격
        if (Time.time - lastAttackTime > characterInfo.attackSpeed)
        {
            lastAttackTime = Time.time;
            //PlayerController.instance.GetComponent<IDamagable>().TakePhysicalDamage(damage); // 데미지 처리
            animator.speed = 1;
            animator.SetTrigger(Attack);
        }

    }
}
