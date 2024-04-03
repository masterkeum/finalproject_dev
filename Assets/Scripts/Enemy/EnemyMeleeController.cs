using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
        if (playerDistance > characterInfo.attackRange)
        {
            SetState(EnemyState.Trace);
            
        }

        // 공격
        if (Time.time - lastAttackTime > characterInfo.attackSpeed)
        {
            lastAttackTime = Time.time;
            //PlayerController.instance.GetComponent<IDamagable>().TakePhysicalDamage(damage); // 데미지 처리
            
            // GameObject hudText = Instantiate(Resources.Load<GameObject>("Prefabs/UI/DamageText")); // 생성할 텍스트 오브젝트
            // Debug.Log("데미지텍스트 프리팹 " + hudText);
            // hudText.transform.position = hudPos.position; // 표시될 위치
            // hudText.GetComponentInChildren<DamageText>().damage = damage; // 데미지 전달
            // // player.TakePhysicalDamage(damageAmount);
            player.TakeDamage(damage);
            animator.speed = 1;
            animator.SetTrigger(Attack);
        }
        
    }
    
}
