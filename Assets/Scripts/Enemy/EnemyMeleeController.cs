using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeController : EnemyBaseController
{
    private bool _isCollidingWithTarget;

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        // 이동 및 공격거리판단
        navMeshAgent.SetDestination(targetPlayerTransform.position);

    }


}
