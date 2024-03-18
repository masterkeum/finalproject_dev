using UnityEngine.AI;

public class EnemyMeleeController : EnemyBaseController
{
    private bool _isCollidingWithTarget;


    //protected void Update()
    //{
    //    Vector3 direction = targetPlayerTransform.position - transform.position;
    //    direction.Normalize(); // 방향 벡터를 단위 벡터로 만듭니다.
    //    // 이동할 위치를 계산합니다. (현재 위치 + 방향 * 속도 * 시간)
    //    Vector3 move = transform.position + direction * walkSpeed * Time.deltaTime;
    //    // 계산된 위치로 오브젝트를 이동시킵니다.
    //    transform.position = move;
    //    // 목표에 가까워졌는지 체크하고, 매우 가까워졌다면 이동을 멈추거나 다른 처리를 할 수 있습니다.
    //    if (Vector3.Distance(transform.position, targetPlayerTransform.position) < 0.001f)
    //    {
    //        // 도착했을 때의 처리를 여기에 작성합니다.
    //        Debug.Log("목표 도달!");
    //    }
    //    // 몬스터 바라보는 방향 로테이션 해줘야함
    //}


    private void FixedUpdate()
    {
        // 이동 및 공격거리판단
        //navMeshAgent.SetDestination(targetPlayerTransform.position);
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(targetPlayerTransform.position, path);
        navMeshAgent.SetPath(path);
    }

}
