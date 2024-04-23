using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRangeController : EnemyBaseController
{
    private NavMeshPath path;

    private GameObject projectile;

    public override void Init(int _monsterID, int _level, Player target)
    {
        base.Init(_monsterID, _level, target);

        path = new NavMeshPath();
        SetState(EnemyState.Trace);

        StartCoroutine(CheckState());

        switch (_monsterID)
        {
            // 시간되면 데이터로 빼야함
            case 20000005:
                projectile = Resources.Load<GameObject>("Prefabs/Enemy/Skill/AcidMissileOBJ");
                break;
            case 20000007:
                projectile = Resources.Load<GameObject>("Prefabs/Enemy/Skill/BoneMissileOBJ");
                break;
            case 20000008:
                projectile = Resources.Load<GameObject>("Prefabs/Enemy/Skill/Sub/FlamethrowerPointyYellow");
                break;
        }
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
        // 공격
        if (Time.time - lastAttackTime > characterInfo.attackSpeed)
        {
            lastAttackTime = Time.time;
            PlayEffect();
            //animator.SetTrigger(Attack);
            //player.TakeDamage(damage);
        }

        if (playerDistance > characterInfo.attackRange)
        {
            SetState(EnemyState.Trace);
        }
    }

    private void PlayEffect()
    {
        Vector3 direction = DirectionToTarget();
        GameObject newEffect = Instantiate(projectile, transform.position + Vector3.up, Quaternion.LookRotation(direction));
        newEffect.GetComponent<EnemyProjectile>().Init(damage, 20f);
        Destroy(newEffect, 3f); // 일정 시간 후에 이펙트를 파괴
        animator.SetTrigger(Attack);
    }
}
