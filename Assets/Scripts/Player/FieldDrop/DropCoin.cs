using Gley.Jumpy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoin : MonoBehaviour
{
    private int gold;
    private int exp;
    private bool IsInit = false;
    private Player player;
    private float distance;

    /*
        나중에 자석아이템이 들어가면 풀이 있긴 해야할듯.
    */

    public void Init(int _gold, int _exp)
    {
        if (IsInit) return;
        transform.position = GetGroundPosition(transform.position) + Vector3.up;

        gold = _gold;
        exp = _exp;
        player = GameManager.Instance.player;
        IsInit = true;
    }

    private void Update()
    {
        DistanceToTarget();
    }

    private void DistanceToTarget()
    {
        // 플레이어와 거리
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 2.0f)
        {
            Debug.Log($"경험치 획득 : {exp}, {gold}");
            // 획득값 플레이어에게 던져주기
            player.AddExp(exp);
            player.AddGold(gold);

            Destroy(gameObject);
        }
    }

    public Vector3 GetGroundPosition(Vector3 startPosition)
    {
        RaycastHit hit;
        // 바닥으로 레이
        if (Physics.Raycast(startPosition, Vector3.down, out hit))
        {
            return hit.point;
        }

        // 바닥이 아닌 
        return startPosition;
    }

}
