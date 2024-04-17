using System.Collections.Generic;
using UnityEngine;

public class FindBossArrow : MonoBehaviour

{
    // [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform aimPivot;
    // [SerializeField] private SpriteRenderer characterRenderer;
    private List<GameObject> chaseTarget = new List<GameObject>();
    // 2d -> 3d
    // 마우스 입력 -> 보스 방향값만 보내주기
    public Transform bossPos;
    
    private void Update()
    {
        
        //Debug.Log("==플레이어=="+playerPos);

        if (bossPos == null)
        {
            return;   
        }
        
        aimPivot.LookAt(bossPos);
        
        
        //Debug.Log("==보스=="+bossPos);
        
        // Vector3 direction = (bossPos.position - playerPos.position).normalized;
        
        //Debug.Log("==direction=="+direction);
        // float angle = Vector3.Angle(playerPos.forward, direction);
        // Quaternion lookRotation = Quaternion.LookRotation(direction);

        // aimPivot.position = playerPos.position + direction;
        
        
        // aimPivot.rotation = lookRotation;
    }
    
    

    // public void OnAim(Vector3 newAimDirection)
    // {
    //     RotateArm(newAimDirection);
    // }
    //
    // private void RotateArm(Vector3 direction)
    // {
    //     float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //
    //     armRenderer.flipY = Mathf.Abs(rotZ) > 90f; // 90도 넘어가면 플립하게 
    //     characterRenderer.flipX = armRenderer.flipY;
    //     armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    // }
}

