using UnityEngine;

public class DropCoin : MonoBehaviour
{
    private LayerMask groundLayerMask;

    public int gold { get; private set; }
    public int exp { get; private set; }

    private bool IsMoving = false;
    private Vector3 targetPosition;
    private float moveSpeed = 10f; // 동전 이동 속도
    private float timeToReachPlayer = 1f; // 플레이어에게 도달하는 데 걸리는 시간

    private void Awake()
    {
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        transform.position = GetGroundPosition(transform.position) + Vector3.up;
    }

    public void Init(int _gold, int _exp)
    {
        gold = _gold;
        exp = _exp;
    }

    private void FixedUpdate()
    {
        if (IsMoving)
        {
            targetPosition = GameManager.Instance.player.transform.position;
            // 현재 위치에서 플레이어 위치까지 일정 시간 안에 도달하도록 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime / timeToReachPlayer);
        }
    }
    public void moveToPlayer()
    {
        //gold = 0;
        //exp = 0;
        IsMoving = true;
    }


    public Vector3 GetGroundPosition(Vector3 startPosition)
    {
        RaycastHit hit;
        // 바닥으로 레이
        if (Physics.Raycast(startPosition, Vector3.down, out hit, Mathf.Infinity, groundLayerMask))
        {
            return hit.point;
        }
        // 바닥이 아닌 
        return startPosition;
    }

}
