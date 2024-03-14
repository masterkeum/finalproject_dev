using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    public VariableJoystick joy;
    public float speed;

    private Rigidbody rigid;
    private Animator anim;
    private Vector3 moveVec;

    [SerializeField] private string characterType;
    [SerializeField] private string name;
    [SerializeField] private int minLevel;
    [SerializeField] private int maxLevel;
    [SerializeField] private int hp;
    [SerializeField] private int attackPower;
    [SerializeField] private float sensoryRange;
    [SerializeField] private float attackRange;
    [SerializeField] private int attackSpeed;
    [SerializeField] private int moveSpeed;
    [SerializeField] private string defaultSkill;
    [SerializeField] private string prefabFile;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float x = joy.Horizontal;
        float z = joy.Vertical;
        Debug.Log($"{x}, {z}");

        moveVec = new Vector3(x, 0, z) * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + moveVec);

        if (moveVec.sqrMagnitude == 0)
            return;

        Quaternion dirQuat = Quaternion.LookRotation(moveVec);
        Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
        rigid.MoveRotation(moveQuat);
    }

    private void LateUpdate()
    {
        anim.SetFloat("Move", moveVec.sqrMagnitude); 
    }

    public void JoyStick(VariableJoystick joy)
    {
        this.joy = joy;
    }
}
