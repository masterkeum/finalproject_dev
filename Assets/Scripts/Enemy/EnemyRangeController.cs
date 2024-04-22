using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRangeController : EnemyBaseController
{
    private NavMeshPath path;

    public override void Init(int _monsterID, int _level, Player target)
    {
        base.Init(_monsterID, _level, target);

        path = new NavMeshPath();
        SetState(EnemyState.Trace);

        StartCoroutine(CheckState());
    }

    private IEnumerator CheckState()
    {
        while (true)
        {
            yield return new WaitForSeconds(trackingTerm);

            playerDistance = DistanceToTarget();
            switch (enemyState)
            {
                case EnemyState.Trace: TraceUpdate(); break;
                case EnemyState.Attack: AttackUpdate(); break;
                case EnemyState.Die: break;
                default:
                    SetState(EnemyState.Trace); break;
            }
        }
    }

    private void Update()
    {
        if (enemyState == EnemyState.Attack)
        {
            AttackUpdate();
        }
    }

    private void TraceUpdate()
    {
        if (playerDistance <= characterInfo.attackRange)
        {
            SetState(EnemyState.Attack);
            return;
        }

        navMeshAgent.CalculatePath(targetPlayerTransform.position, path);
        navMeshAgent.SetPath(path);
    }

    private void AttackUpdate()
    {
        transform.LookAt(player.transform);
        if (playerDistance > characterInfo.attackRange)
        {
            SetState(EnemyState.Trace);
        }

        // 공격
        if (Time.time - lastAttackTime > characterInfo.attackSpeed)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger(Attack);
            player.TakeDamage(damage);
        }
    }

}
