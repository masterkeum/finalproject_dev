using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseController : MonoBehaviour
{

    /*
    적 베이스 스크립트
        플레이어 타겟팅
        이동

        사망처리
    */

    private NavMeshAgent  navMeshAgent;
    private Animator animator;
    private SkinnedMeshRenderer[] meshRenderers;

    private void Awake()
    {
        // 플레이어 타겟팅

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

    }




    /*
        상속되는 파일로 옮길것
        [SerializeField] private float moveSpeed; // 이동속도
        [SerializeField] private float sensoryRange; // 플레이어 감지
        [SerializeField] private float attackRange; // 공격 범위
        [SerializeField] private float attackSpeed; // 


        [SerializeField] private int Health;
        [SerializeField] private int attackPower; // 공격력
    */



}
