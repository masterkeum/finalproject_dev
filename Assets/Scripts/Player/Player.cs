using UnityEngine;

public class Player : MonoBehaviour
{
    public VariableJoystick joy;
    public float speed;

    private Rigidbody rigid;
    private Animator anim;
    private Vector3 moveVec;

    private int PlayerID;
    private int level;
    private int hp;
    private CharacterInfo characterInfo;
    
    private bool IsInit = false;

    public virtual void Init(int _player, int _level)
    {
        if (IsInit) return;
        PlayerID = _player;
        level = _level;

        characterInfo = DataManager.Instance.characterInfoDict[PlayerID];
        
        hp = characterInfo.hp;
        
        IsInit = true;
    }
    
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            // 임시방편. 바닥밑으로 떨어지면 위치이동
            transform.position = Vector3.zero;
        }
    }


    private void FixedUpdate()
    {
        float x = joy.Horizontal;
        float z = joy.Vertical;
        //Debug.Log($"{x}, {z}");

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
        //anim.SetFloat("Move", moveVec.sqrMagnitude); 
    }

    public void JoyStick(VariableJoystick joy)
    {
        this.joy = joy;
    }
    
    public void TakePhysicalDamage(int damageAmount)
    {
        
        hp -= damageAmount;
        if (hp <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("플레이어사망");
    }
}
