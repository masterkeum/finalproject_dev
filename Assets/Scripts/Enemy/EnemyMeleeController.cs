using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeController : EnemyBaseController
{
    private NavMeshPath path;
    // public GameObject hudDamageText;
    // public Transform hudPos;


    public override void Init(int _monsterID, int _level, Player target)
    {
        base.Init(_monsterID, _level, target);

        path = new NavMeshPath();
        SetState(EnemyState.Trace);

        StartCoroutine(CheckState());

    }
    // 상태 초기화
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

    //private void Update()
    //{
    //    playerDistance = DistanceToTarget();
    //    switch (enemyState)
    //    {
    //        case EnemyState.Trace: TraceUpdate(); break;
    //        case EnemyState.Attack: AttackUpdate(); break;
    //        case EnemyState.Die: break;
    //        default:
    //            SetState(EnemyState.Trace); break;
    //    }

    //}

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
        // 공격
        if (Time.time - lastAttackTime > characterInfo.attackSpeed)
        {
            lastAttackTime = Time.time;
            player.TakeDamage(damage);
            animator.speed = 1;
            animator.SetTrigger(Attack);
        }

        if (playerDistance > characterInfo.attackRange)
        {
            SetState(EnemyState.Trace);
        }
    }

}
