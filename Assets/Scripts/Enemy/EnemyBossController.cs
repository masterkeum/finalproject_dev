using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static SkillPool;

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
    private GameObject arrowPrefab;

    private GameObject effectBossAttack;
    private GameObject effectBossAttack2;
    private float secondAttackTime;
    private float secondAttackCooldown = 5f;
    private float dashForce = 10f;

    private float timer = 0f;
    private float advSensoryRange = 0f;

    public override void Init(int _monsterID, int _level, Player target)
    {
        base.Init(_monsterID, _level, target);

        defaultPos = transform.position;

        minWanderDistance = 8f;
        maxWanderDistance = 10f;
        minWanderWaitTime = 0f;
        maxWanderWaitTime = 1f;

        path = new NavMeshPath();
        SetState(EnemyState.Wander);

        StartCoroutine(CheckState());
        FindBossArrow();

        switch (_monsterID)
        {
            case 20100001:
                effectBossAttack = Resources.Load<GameObject>("Prefabs/Enemy/Skill/SlashAttack");
                effectBossAttack2 = Resources.Load<GameObject>("Prefabs/Enemy/Skill/BossDash");
                break;
            case 20100002:
                effectBossAttack = Resources.Load<GameObject>("Prefabs/Enemy/Skill/SkullMissilePurpleOBJ");
                break;
            case 20100003:
                effectBossAttack = Resources.Load<GameObject>("Prefabs/Enemy/Skill/SlashWideAttack");
                break;
        }
    }

    private void FindBossArrow()
    {
        arrowPrefab = Instantiate(Resources.Load<GameObject>("Prefabs/UI/GameObject"), player.transform);
        arrowPrefab.GetComponent<FindBossArrow>().bossPos = transform;
    }

    private void Update()
    {
        if (enemyState == EnemyState.Attack)
        {
            AttackUpdate();
        }
        timer += Time.deltaTime;
        if (timer > 600)
        {
            advSensoryRange = 240f;
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
                case EnemyState.Idle: WanderUpdate(); break;
                case EnemyState.Wander: WanderUpdate(); break;
                case EnemyState.Trace: TraceUpdate(); break;
                case EnemyState.Attack: AttackUpdate(); break;
                case EnemyState.Flee: FleeUpdate(); break;
                case EnemyState.Die: break;

                default:
                    SetState(EnemyState.Flee); break;
            }
        }
    }

    private void WanderUpdate()
    {
        //플레이어 거리가 감지 범위 내로 들어왔을 경우
        if (playerDistance < characterInfo.sensoryRange + advSensoryRange)
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
        try
        {
            navMeshAgent.CalculatePath(GetWanderLocation(), path);
            navMeshAgent.SetPath(path);
        }
        catch
        {
            Debug.LogWarning("보스 경로 못찾음");
        }
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
        if (playerDistance > characterInfo.sensoryRange + advSensoryRange)
        {
            navMeshAgent.CalculatePath(defaultPos, path); // 원위치
            navMeshAgent.SetPath(path);
            navMeshAgent.speed = characterInfo.moveSpeed * 2;
            animator.SetBool(IsWalking, true);
            SetState(EnemyState.Flee);
        }
        else
        {
            if (Time.time - secondAttackTime >= secondAttackCooldown)
            {
                Debug.Log("Dash");
                secondAttackTime = Time.time;
                Vector3 direction = (player.transform.position - transform.position).normalized;
                StartCoroutine(DashRountine(1.5f, direction));
            }
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
        transform.LookAt(player.transform);
        //transform.Rotate(DirectionToTarget());
        if (Time.time - lastAttackTime > characterInfo.attackSpeed)
        {
            lastAttackTime = Time.time;
            PlayEffect();
        }

        // 공격 범위 벗어나면
        if (playerDistance > characterInfo.attackRange)
        {
            SetState(EnemyState.Trace);
        }
    }

    private void PlayEffect()
    {
        Vector3 direction = DirectionToTarget();
        switch (characterInfo.uid)
        {
            case 20100002:
                {
                    GameObject newEffect = Instantiate(effectBossAttack, transform.position + Vector3.up, Quaternion.LookRotation(direction));
                    newEffect.GetComponent<EnemyProjectile>().Init(damage, 30f);
                    Destroy(newEffect, 5f); // 일정 시간 후에 이펙트를 파괴
                    animator.SetTrigger(Attack);
                }
                break;
            case 20100001:
            case 20100003:
            default:
                {
                    // 슬래시 0.9
                    GameObject newEffect = Instantiate(effectBossAttack, transform.position + Vector3.up, Quaternion.LookRotation(direction));
                    Destroy(newEffect, 0.9f); // 일정 시간 후에 이펙트를 파괴
                    player.TakeDamage(damage);
                    animator.SetTrigger(Attack);
                }
                break;
        }
    }

    private IEnumerator DashRountine(float delay, Vector3 dashDirection)
    {
        transform.LookAt(player.transform);
        GameObject newEffect = Instantiate(effectBossAttack2, transform.position, Quaternion.LookRotation(-dashDirection));

        capsuleCollider.isTrigger = true;
        navMeshAgent.speed = characterInfo.moveSpeed * 3;
        //navMeshAgent.velocity = dashDirection*2;
        float tmpAcc = navMeshAgent.acceleration;
        navMeshAgent.acceleration = 20f;
        animator.speed = 2;

        yield return new WaitForSeconds(delay);
        Destroy(newEffect);
        capsuleCollider.isTrigger = false;
        navMeshAgent.speed = characterInfo.moveSpeed;
        navMeshAgent.acceleration = tmpAcc;
        animator.speed = 1;
        yield break;
    }

    private void FleeUpdate()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            navMeshAgent.speed = characterInfo.moveSpeed;
            animator.SetBool(IsWalking, false);
            SetState(EnemyState.Wander);
        }
    }

    protected override void OnDead()
    {
        base.OnDead();
        Destroy(arrowPrefab);
        Debug.Log("보스 하나 죽음");
        player.AddCore(monsterLevel.core);
        player.RemoveChaseTarget(gameObject);
    }
}
