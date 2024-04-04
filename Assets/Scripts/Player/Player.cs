using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct PlayerInGameInfo
{
    public int curHp;
    public int maxHp;

    public int attackPower;
    public int defense;
    public float moveSpeed;
    public float critical;
    public float hpGen;


    public int addProjectileCount;
    public int addprojectilePenetration;
    public int addDefense;
    public int addHp;
    public int addRegenHp;


    public int curLevel;
    //public int maxLevel;
    public int sliderCurExp;
    public int sliderMaxExp;
    public int curExp;
    public int totalExp;
    //public int maxExp;

    public int killCount;
    public int gold;
    public int skillpoint;

    public PlayerInGameInfo(int _maxHp, int _attackPower, int _defense, float _moveSpeed,
                            float _critical, float _hpGen, int _level, int _sliderMaxExp)
    {
        maxHp = _maxHp;
        curHp = maxHp;

        attackPower = _attackPower;
        defense = _defense;
        moveSpeed = _moveSpeed;
        critical = _critical;
        hpGen = _hpGen;

        addProjectileCount = 0;
        addprojectilePenetration = 0;
        addDefense = 0;
        addHp = 0;
        addRegenHp = 0;

        curLevel = _level;
        sliderCurExp = 0;
        sliderMaxExp = _sliderMaxExp;
        curExp = 0;
        totalExp = 0;
        killCount = 0;
        gold = 0;
        skillpoint = 0;
    }
}

public class Player : MonoBehaviour
{
    public VariableJoystick joy;

    protected Rigidbody rigid;
    protected Animator anim;
    protected Vector3 moveVec;

    private bool IsInit = false;


    [SerializeField] protected Transform projectilePoint;


    private Vector3 screenPos;

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

    // 인게임 스탯
    public PlayerStatInfo playerStatInfo;
    public PlayerInGameInfo playeringameinfo;

    private int playerId;
    private int level;

    // 그룹ID / 스킬내용
    public List<SkillTable> activeSkillSlot = new List<SkillTable>(); // skillId 기준
    public List<SkillTable> passiveSkillSlot = new List<SkillTable>();
    public Dictionary<int, SkillTable> activeSkill = new Dictionary<int, SkillTable>(); // skillGroupId 기준
    private Dictionary<int, Coroutine> skillCoroutines = new Dictionary<int, Coroutine>();
    public Dictionary<int, SkillTable> passiveSkill = new Dictionary<int, SkillTable>();

    private void Awake()
    {
        Debug.Log("Player.Awake");
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        skillPool = GetComponent<SkillPool>();
        hpGuageSlider = GetComponentInChildren<Slider>();
    }

    public virtual void Init(int _player, int _level)
    {
        if (IsInit) return;

        Debug.Log("Player.Init");
        playerId = _player;
        level = _level;

        playerStatInfo = GameManager.Instance.accountInfo.playerStatInfo;
        playeringameinfo = new PlayerInGameInfo(playerStatInfo.hp + playerStatInfo.addHp
            , playerStatInfo.attackPower + playerStatInfo.addAttackPower
            , playerStatInfo.defense + playerStatInfo.addDefense
            , playerStatInfo.moveSpeed + playerStatInfo.addMoveSpeed
            , playerStatInfo.critical + playerStatInfo.addCritical
            , playerStatInfo.hpGen + playerStatInfo.addHpGen
            , level, DataManager.Instance.GetPlayerIngameLevel(level + 1).exp);

        SkillTable defaultSkill = DataManager.Instance.GetSkillTable(DataManager.Instance._InitParam["StartSkillId"]);
        SkillUpdate(defaultSkill);

        IsInit = true;
    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            // 임시방편. 바닥밑으로 떨어지면 위치이동
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
        //SkillRoutine();
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


    #region Controll

    public void SkillUpdate(SkillTable skilldata)
    {
        // 그룹 중복값 삭제
        bool skillFound = false;
        if (skilldata.applyType == SkillApplyType.Active
            || skilldata.applyType == SkillApplyType.Awaken)
        {
            // UI 용
            for (int i = 0; i < activeSkillSlot.Count; i++)
            {
                if (skilldata.skillGroup == activeSkillSlot[i].skillGroup)
                {
                    activeSkillSlot[i] = skilldata; // 참조 변경
                    skillFound = true;
                    break;
                }
            }
            if (!skillFound)
            {
                activeSkillSlot.Add(skilldata);
            }

            // 스킬 새로운 로직
            if (activeSkill.ContainsKey(skilldata.skillGroup))
            {
                // 스킬풀 삭제 후 새로 생성
                skillPool.DestroyDicObject(activeSkill[skilldata.skillGroup].skillId);
                skillPool.AddSkillPool(skilldata);
                skillPool.CreatePool(transform);
                // 레벨업
                activeSkill[skilldata.skillGroup] = skilldata;
                skillCoroutines[skilldata.skillGroup] = StartSkillCoroutine(skilldata);
            }
            else
            {
                // 스킬풀 새로 생성
                skillPool.AddSkillPool(skilldata);
                skillPool.CreatePool(transform);

                // 새스킬
                activeSkill.Add(skilldata.skillGroup, skilldata);
                skillCoroutines.Add(skilldata.skillGroup, StartSkillCoroutine(skilldata));
            }
        }
        else
        {
            // UI 용
            for (int i = 0; i < passiveSkillSlot.Count; i++)
            {
                if (skilldata.skillGroup == passiveSkillSlot[i].skillGroup)
                {
                    passiveSkillSlot[i] = skilldata;
                    skillFound = true;
                    break;
                }
            }
            if (!skillFound)
            {
                passiveSkillSlot.Add(skilldata);
            }

            // 스킬 새로운 로직
            if (passiveSkill.ContainsKey(skilldata.skillGroup))
            {
                passiveSkill[skilldata.skillGroup] = skilldata;
                //skillCoroutines[skilldata.skillGroup] = StartSkillCoroutine(skilldata);
            }
            else
            {
                passiveSkill.Add(skilldata.skillGroup, skilldata);
                //skillCoroutines.Add(skilldata.skillGroup, StartSkillCoroutine(skilldata));
            }
        }
    }

    public Coroutine StartSkillCoroutine(SkillTable skilldata)
    {
        if (skillCoroutines.ContainsKey(skilldata.skillGroup))
        {
            StopCoroutine(skillCoroutines[skilldata.skillGroup]);
        }
        return StartCoroutine(SkillRoutine(skilldata));
    }

    IEnumerator SkillRoutine(SkillTable skilldata)
    {
        int damage = 50;
        Debug.Log($"Coroutine started with parameter: {skilldata.skillId}");
        while (true)
        {
            switch (skilldata.targetType)
            {
                case SkillTargetType.Single:
                    {
                        yield return new WaitForSeconds(skilldata.coolDownTime);

                        Vector3 direction = DetectEnemyDirection();
                        // 발사 작동
                        skillPool.GetPoolSkill(skilldata.skillId, projectilePoint, direction, damage);
                    }
                    break;
                default:
                    yield break;
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

    public void TakeDamage(int damageAmount)
    {
        playeringameinfo.curHp -= damageAmount;
        float per = (float)playeringameinfo.curHp / playeringameinfo.maxHp;
        hpGuageSlider.value = per;

        damageAmount = -damageAmount;

        GameObject hudText = Instantiate(Resources.Load<GameObject>("Prefabs/UI/DamageText")); // 생성할 텍스트 오브젝트
        //Debug.Log("데미지텍스트 프리팹 " + hudText);
        hudText.transform.position = hudPos.position; // 표시될 위치
        hudText.GetComponentInChildren<DamageText>().damage = damageAmount; // 데미지 전달
                                                                            // player.TakePhysicalDamage(damageAmount);


        //Debug.Log("플레이어 현재 HP" + per);
        if (playeringameinfo.curHp <= 0)
            OnDead();
    }

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
            float randomX = UnityEngine.Random.Range(-1f, 1f);
            float randomZ = UnityEngine.Random.Range(-1f, 1f);

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

    /// <summary>
    /// 스킬 슬롯
    /// TODO : 데이터 기본값 + 업적에 따른 오픈 개수
    /// </summary>
    /// <returns>스킬 슬롯 오픈개수</returns>
    public int CurrentOpenSkillSlotCount(SkillApplyType applyType) //허용 슬롯의 숫자. 일단 3으로 정해놓았으나 이후 조건에 따른 값을 리턴하게 한다.
    {
        int slotCount = 3;
        if (applyType == SkillApplyType.Active)
        {
            slotCount += Mathf.Max((playeringameinfo.curLevel + 3) / 6, 3);
        }
        else if (applyType == SkillApplyType.Passive)
        {
            slotCount += Mathf.Max(playeringameinfo.curLevel / 6, 3);
        }
        return slotCount;
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
                //Debug.Log($"랩업 토탈경치 {totalExp + addExp}/{levelData.totalExp}");
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
