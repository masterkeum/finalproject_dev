using UnityEngine;

public class EnemyBossController : EnemyBaseController
{


    public override void Init(int _monsterID, int _level, Player target)
    {
        base.Init(_monsterID, _level, target);


    }




    // 거리가 가까울때만 반응

    private void FixedUpdate()
    {
        // 이동 및 공격거리판단
        //navMeshAgent.SetDestination(targetPlayerTransform.position);
        //NavMeshPath path = new NavMeshPath();
        //navMeshAgent.CalculatePath(targetPlayerTransform.position, path);
        //navMeshAgent.SetPath(path);

        //Debug.Log(playerDistance);
    }


}
