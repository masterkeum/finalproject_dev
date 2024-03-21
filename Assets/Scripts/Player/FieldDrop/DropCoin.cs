using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoin : MonoBehaviour
{
    private int gold;
    private int exp;
    private bool IsInit = false;
    private GameObject player;
    [SerializeField] protected Transform targetPlayerTransform;
    private float distance;

    public void Init(int _gold, int _exp, GameObject target)
    {
        if (IsInit) return;
        gold = _gold;
        exp = _exp;
        targetPlayerTransform = target.transform;

        IsInit = true;
    }

    private void Update()
    {
        DistanceToTarget();
    }


    protected void DistanceToTarget()
    {
        // 플레이어와 거리
        distance = Vector3.Distance(transform.position, targetPlayerTransform.position);
        if (distance < 2.0f)
        {
            // 획득값 플레이어에게 던져주기


            Destroy(gameObject);
        }
    }

}
