using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public VariableJoystick joy;

    protected Rigidbody rigid;
    protected Animator anim;
    protected Vector3 moveVec;

    protected int playerID;
    protected int level;
    protected int currentHp;
    protected int maxHp;
    private int damage;
    protected CharacterInfo characterInfo;

    private bool IsInit = false;

    public playeringameinfo playeringameinfo;
    public List<SkillTable> activeSkillSlot = new List<SkillTable>();
    public List<SkillTable> passiveSkillSlot = new List<SkillTable>();

    [SerializeField] protected Transform projectilePoint;
    

    private Vector3 screenPos;

    public TMP_Text takeDamagePoint;
    
    // 적
    public LayerMask enemyLayer;
    public float detectionRange = 15f;
    private List<Transform> nearEnemy = new List<Transform>();
    protected SkillPool skillPool;

    public List<GameObject> chaseTarget = new List<GameObject>();

    private Slider hpGuageSlider;
    private EnemyBaseController monster; 
    public GameObject hudDamageText;
    public Transform hudPos;
    
    public virtual void Init(int _player, int _level)
    {
        if (IsInit) return;

        Debug.Log("Player.Init");
        playerID = _player;
        level = _level;

        characterInfo = DataManager.Instance.characterInfoDict[playerID];

        skillPool.CreatePool(transform);


        // 플레이어 기본 스탯 초기화
        currentHp = characterInfo.hp;
        maxHp = characterInfo.hp;
        damage = characterInfo.attackPower;

        playeringameinfo = new playeringameinfo();
        playeringameinfo.attackPower = characterInfo.attackPower;
        playeringameinfo.addAttackPower = 0;
        playeringameinfo.sensoryRange = characterInfo.sensoryRange;
        playeringameinfo.attackRange = characterInfo.attackRange;
        playeringameinfo.attackSpeed = characterInfo.attackSpeed;
        playeringameinfo.moveSpeed = characterInfo.moveSpeed;

        playeringameinfo.curLevel = 1;
        playeringameinfo.sliderCurExp = 0;
        playeringameinfo.sliderMaxExp = DataManager.Instance.GetPlayerIngameLevel(2).exp;
        playeringameinfo.curExp = 0;
        playeringameinfo.totalExp = 0;
        playeringameinfo.killCount = 0;
        playeringameinfo.gold = 0;
        playeringameinfo.skillpoint = 0;

        activeSkillSlot.Add(DataManager.Instance.GetSkillTable(30000001)); // 기본스킬 지급

        IsInit = true;
    }

    private void Awake()
    {
        Debug.Log("Player.Awake");
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        skillPool = GetComponent<SkillPool>();
        hpGuageSlider = GetComponentInChildren<Slider>();
        takeDamagePoint = GetComponentInChildren<TMP_Text>();
        
        // monster = GameManager.Instance. // 프리팹된 몬스터 연결
    }

    private void Start()
    {
        // UpdateSlider();
        takeDamagePoint.gameObject.SetActive(false);
        
        
    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            // 임시방편. 바닥밑으로 떨어지면 위치이동
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }

        SkillRoutine();
        
    }


    private void FixedUpdate()
    {
        float x = joy.Horizontal;
        float z = joy.Vertical;
        //Debug.Log($"{x}, {z}");

        moveVec = new Vector3(x, 0, z) * playeringameinfo.moveSpeed * Time.deltaTime;
        rigid.MovePosition(rigid.position + moveVec);

        if (moveVec.sqrMagnitude == 0)
            return;

        Quaternion dirQuat = Quaternion.LookRotation(moveVec);
        Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
        rigid.MoveRotation(moveQuat);
    }


    //private void LateUpdate()
    //{
    //    //anim.SetFloat("Move", moveVec.sqrMagnitude); 
    //    if (chaseTarget.Count > 0)
    //    {
    //        // TODO : 적 추적 작동
    //    }
    //}


    #region Controll

    private void SkillRoutine()
    {
        // 임시
        foreach (SkillTable skill in activeSkillSlot)
        {
            switch (skill.skillType)
            {
                case "Target":
                    {
                        if (Time.time - skill.lastAttackTime > skill.coolDownTime)
                        {
                            skill.lastAttackTime = Time.time;
                            // 10f 까지 탐색? = 보스 탐지거리
                            // 가까운놈 찾아서 그방향으로 발사 일정거리 가면 사라짐
                            // 발사 방향
                            Vector3 direction = DetectEnemyDirection();
                            // 발사 작동
                            skillPool.GetPoolSkill(skill.skillId, skill.level, projectilePoint, direction);
                            skillPool.GetPoolFlash(skill.skillId, projectilePoint, direction);
                        }
                    }
                    break;

            }
        }
    }

    public void JoyStick(VariableJoystick joy)
    {
        this.joy = joy;
    }

    // public void TakeDamageNumber(Vector3 position)
    // {
    //     // todo 
    //     // 몬스터와 충돌하면 피격값을 text로 띄워준다.
    //     // 1. update 로 실시간 충돌 감지
    //     // 2. 충돌하면 text 활성화 해서 띄워준다.
    //     
    //     screenPos = Camera.main.WorldToScreenPoint(moveVec);
    //     Debug.Log("스크린투 월드 좌표: "+screenPos);
    // }

    public void TakePhysicalDamage(int damageAmount)
    {
        currentHp -= damageAmount;
        float per = (float)currentHp  /   maxHp;  
        hpGuageSlider.value = per;
        
        // takeDamagePoint.gameObject.SetActive(true);
        damageAmount = -damageAmount;
        // takeDamagePoint.text = damageAmount.ToString();
        // // 코루틴 사용
        // StartCoroutine(SetActiveFalse());
        
        GameObject hudText = Instantiate(Resources.Load<GameObject>("Prefabs/UI/DamageText")); // 생성할 텍스트 오브젝트
        Debug.Log("데미지텍스트 프리팹 " + hudText);
        hudText.transform.position = hudPos.position; // 표시될 위치
        hudText.GetComponentInChildren<DamageText>().damage = damageAmount; // 데미지 전달
        // player.TakePhysicalDamage(damageAmount);
        
        
        Debug.Log("플레이어 현재 HP" + per);
        if (currentHp <= 0)
            OnDead();
    }

    // IEnumerator SetActiveFalse()
    // {
    //     yield return new WaitForSeconds(1.0f); 
    //     takeDamagePoint.gameObject.SetActive(false);
    //     // 지금 실행하는 쓰레드를 기다릴거냐 말거냐, 턴을 넘김 null 을 넘김
    // }
    
    void OnDead()
    {
        Debug.Log("플레이어사망. 게임오버UI");
        UIManager.Instance.ShowUI<UIDefeated>();
        ++UIManager.Instance.popupUICount;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected Vector3 DetectEnemyDirection()
    {
        nearEnemy.Clear();
        // TODO: OverlapSphereNonAlloc로 변환가능하면 변환
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, enemyLayer);
        foreach (Collider col in hitColliders)
        {
            nearEnemy.Add(col.transform);
        }

        if (nearEnemy.Count > 0)
        {
            Transform nearestEnemy = GetNearestEnemy();
            Vector3 direction = (nearestEnemy.position - transform.position).normalized;
            direction.y = 0;
            return direction;
        }
        else
        {
            // 근거리 없으면 아무방향으로 발사
            // Unity의 Random 클래스를 사용하여 -1과 1 사이의 랜덤한 값으로 각 축을 설정합니다.
            float randomX = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);

            // Vector3.Normalize 함수를 사용하여 벡터를 정규화합니다.
            Vector3 randomDirection = new Vector3(randomX, 0f, randomZ).normalized;
            return randomDirection;
        }
    }

    // 가장 가까운 적을 찾는 함수
    protected Transform GetNearestEnemy()
    {
        Transform nearestEnemy = null;
        float nearestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Transform enemy in nearEnemy)
        {
            Vector3 directionToTarget = enemy.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < nearestDistanceSqr)
            {
                nearestDistanceSqr = dSqrToTarget;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    #endregion

    #region UI
    public void AddTarget(GameObject go)
    {
        chaseTarget.Add(go);
    }

    private void UpdateSlider()
    {
        playeringameinfo.sliderMaxExp = DataManager.Instance.GetPlayerIngameLevel(playeringameinfo.curLevel + 1).exp;
    }

    /// <summary>
    /// 스킬 슬롯
    /// TODO : 데이터 기본값 + 업적에 따른 오픈 개수
    /// </summary>
    /// <returns>스킬 슬롯 오픈개수</returns>
    public int CurrentOpenSkillSlotCount() //허용 슬롯의 숫자. 일단 3으로 정해놓았으나 이후 조건에 따른 값을 리턴하게 한다.
    {
        return 3;
    }

    /// <summary>
    /// 킬카운트
    /// </summary>
    public void AddKillCount()
    {
        playeringameinfo.killCount++;
    }

    /// <summary>
    /// 골드 추가
    /// </summary>
    /// <param name="addGold"></param>
    public void AddGold(int addGold)
    {
        playeringameinfo.gold += addGold;
    }

    /// <summary>
    /// 경험치 추가 및 레벨업
    /// </summary>
    /// <param name="addExp">추가 경험치</param>
    public void AddExp(int addExp)
    {

        while (addExp > 0)
        {
            PlayerIngameLevel levelData = DataManager.Instance.GetPlayerIngameLevel(playeringameinfo.curLevel + 1);
            if (levelData == null)
            {
                playeringameinfo.sliderMaxExp = 0;
                Debug.Log("만랩");
                break; // 만랩
            }

            playeringameinfo.sliderMaxExp = levelData.exp;
            if (playeringameinfo.totalExp + addExp >= levelData.totalExp)
            {
                //Debug.Log($"랩업 토탈경치 {playeringameinfo.totalExp + addExp}/{levelData.totalExp}");
                // 랩업
                addExp -= (levelData.totalExp - playeringameinfo.totalExp);
                playeringameinfo.totalExp = levelData.totalExp;
                playeringameinfo.curExp = 0;
                playeringameinfo.sliderCurExp = playeringameinfo.curExp;
                ++playeringameinfo.curLevel;
                ++playeringameinfo.skillpoint;
            }
            else
            {
                playeringameinfo.totalExp += addExp;
                playeringameinfo.curExp += addExp;
                playeringameinfo.sliderCurExp = playeringameinfo.curExp;
                addExp = 0;
            }
        }
        GameManager.Instance.UpdateUI();
        if (playeringameinfo.skillpoint > 0)
        {
            if (GameManager.Instance.gameState == GameState.IngameStart)
            {
                UIManager.Instance.ShowUI<UILevelUP>();
                ++UIManager.Instance.popupUICount;
            }
        }
    }

    #endregion
}
