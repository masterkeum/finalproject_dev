using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[Serializable]
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
    public int addSkillDefense;
    public int addSkillHp;
    public int addSkillAttackPower;
    public float addSkillCollectableRange;

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
    public int gem;
    public int core;

    public int resurectionCount;

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

        addSkillDefense = 0;
        addSkillHp = 0;
        addSkillAttackPower = 0;
        addSkillCollectableRange = 2f;

        curLevel = _level;
        sliderCurExp = 0;
        sliderMaxExp = _sliderMaxExp;
        curExp = 0;
        totalExp = 0;
        killCount = 0;
        gold = 0;
        skillpoint = 0;
        gem = 0;
        core = 0;

        resurectionCount = 0;
    }
}

public class Player : MonoBehaviour
{
    public VariableJoystick joy;

    protected Rigidbody rigid;
    protected Animator anim;
    protected Vector3 moveVec;
    public GameObject chestObject;

    private bool IsInit = false;

    [SerializeField] protected Transform projectilePoint;

    public LayerMask enemyLayerMask;
    public LayerMask dropItemLayerMask;
    private Collider[] dropItemColliders;
    // 적

    public float detectionRange = 15f;
    public float detectRandomRange = 10f;

    private List<Transform> nearEnemy = new List<Transform>();
    protected SkillPool skillPool;

    public List<GameObject> chaseTarget = new List<GameObject>();
    private Slider hpGuageSlider;
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

    private Dictionary<int, GameObject> passiveObj = new Dictionary<int, GameObject>();


    protected static readonly int Hit = Animator.StringToHash("Hit");

    // 치트 스크립트
    [SerializeField] private Canvas _testUI;

    private void Awake()
    {
        Debug.Log("Player.Awake");
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        skillPool = GetComponent<SkillPool>();
        hpGuageSlider = GetComponentInChildren<Slider>();
        enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
        dropItemLayerMask = 1 << LayerMask.NameToLayer("DropItem");

        dropItemColliders = new Collider[30];
#if UNITY_EDITOR
        _testUI.gameObject.SetActive(true);
#endif

    }

    public virtual void Init(int _player, int _level)
    {
        if (IsInit) return;

        Debug.Log("Player.Init");
        playerId = _player;
        level = _level;

        playerStatInfo = GameManager.Instance.accountInfo.playerStatInfo;
        playeringameinfo = new PlayerInGameInfo(playerStatInfo.hp + playerStatInfo.addLevelHp + playerStatInfo.addHp
            , playerStatInfo.attackPower + playerStatInfo.addLevelAttack + playerStatInfo.addAttackPower
            , playerStatInfo.defense + playerStatInfo.addDefense
            , playerStatInfo.moveSpeed + playerStatInfo.addMoveSpeed
            , playerStatInfo.critical + playerStatInfo.addCritical
            , playerStatInfo.hpGen + playerStatInfo.addHpGen
            , level, DataManager.Instance.GetPlayerIngameLevel(level + 1).exp);

        SkillTable defaultSkill = DataManager.Instance.GetSkillTable(DataManager.Instance._InitParam["StartSkillId"]);
        SkillUpdate(defaultSkill);

        // Test
        //SkillUpdate(DataManager.Instance.GetSkillTable(30000541));
        //SkillUpdate(DataManager.Instance.GetSkillTable(30001055));

        IsInit = true;
    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            // 
            Vector3 newPos = transform.position;
            newPos.y = 0;
            transform.position = newPos;

            OnDead(GameState.DropDie);
            // 임시방편. 바닥밑으로 떨어지면 위치이동

            // Vector3 tmpPos = transform.position;
            // tmpPos.y = 200;
            // RaycastHit hit;
            // NavMeshHit navhit;
            // if (Physics.Raycast(new Ray(tmpPos, Vector3.down), out hit, Mathf.Infinity))
            // {
            //     if (NavMesh.SamplePosition(hit.point, out navhit, detectionRange, NavMesh.AllAreas))
            //     {
            //         transform.position = navhit.position;
            //     }
            // }
        }
        //SkillRoutine();
    }


    #region Physics

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
        //rigid.MoveRotation(moveQuat);
        chestObject.transform.rotation = dirQuat;

        // 드랍아이템 끌어오기. 이동을 해야 발동함
        // 부하가 어떻지 모르겠음. => OverlapSphereNonAlloc 로 일단 바꿈
        // 범위안에 있는것을 끌어당기면서 거의 동일한 시간에 유닛에게 들어오게 하고
        // 미리 경험치/골드를 한꺼번에 계산해서 플러스 해준다. OnTrigger에서는 그냥 없애기만하기
        int dropItemCount = Physics.OverlapSphereNonAlloc(transform.position, playeringameinfo.addSkillCollectableRange, dropItemColliders, dropItemLayerMask);
        // **** 최적화 취소. 하나씩 먹을때마다 수치 오르게 변경
        //int golds = 0;
        //int exps = 0;
        for (int i = 0; i < dropItemCount; i++)
        {
            DropCoin dropCoin = dropItemColliders[i].GetComponent<DropCoin>();
            if (dropCoin != null)
            {
                //golds += dropCoin.gold;
                //exps += dropCoin.exp;
                dropCoin.moveToPlayer();
            }
        }
        //StartCoroutine(DropItemRoutine(golds, exps));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (dropItemLayerMask == (dropItemLayerMask | (1 << other.gameObject.layer)))
        {
            // dropitem 끌어당긴것 디스트로이
            DropCoin dropCoin = other.GetComponent<DropCoin>();
            if (dropCoin != null)
            {
                AddGold(dropCoin.gold);
                AddExp(dropCoin.exp);
            }

            Destroy(other.gameObject);
        }
    }

    //IEnumerator DropItemRoutine(int golds, int exps)
    //{
    //    yield return new WaitForSeconds(0.8f); // 코인 이동속도와 맞춰야한다.
    //    AddGold(golds);
    //    AddExp(exps);
    //    yield break;
    //}
    #endregion


    #region Skill

    public void SkillUpdate(SkillTable skillData)
    {
        // 그룹 중복값 삭제
        bool skillFound = false;
        if (skillData.applyType == SkillApplyType.Active
            || skillData.applyType == SkillApplyType.Awaken)
        {
            // UI 용
            for (int i = 0; i < activeSkillSlot.Count; i++)
            {
                if (skillData.skillGroup == activeSkillSlot[i].skillGroup)
                {
                    activeSkillSlot[i] = skillData; // 참조 변경
                    skillFound = true;
                    break;
                }
            }
            if (!skillFound)
            {
                activeSkillSlot.Add(skillData);
            }

            // 스킬 새로운 로직
            if (activeSkill.ContainsKey(skillData.skillGroup))
            {
                // 스킬풀 삭제 후 새로 생성
                skillPool.DestroyDicObject(activeSkill[skillData.skillGroup].skillId);
                skillPool.AddSkillPool(skillData);
                skillPool.CreatePool(transform);

                // 레벨업
                activeSkill[skillData.skillGroup] = skillData;
                skillCoroutines[skillData.skillGroup] = StartSkillCoroutine(skillData);
            }
            else
            {
                // 스킬풀 새로 생성
                skillPool.AddSkillPool(skillData);
                skillPool.CreatePool(transform);

                // 새스킬
                activeSkill.Add(skillData.skillGroup, skillData);
                skillCoroutines.Add(skillData.skillGroup, StartSkillCoroutine(skillData));
            }
        }
        else
        {
            // UI 용
            for (int i = 0; i < passiveSkillSlot.Count; i++)
            {
                if (skillData.skillGroup == passiveSkillSlot[i].skillGroup)
                {
                    passiveSkillSlot[i] = skillData;
                    skillFound = true;
                    break;
                }
            }
            if (!skillFound)
            {
                passiveSkillSlot.Add(skillData);
            }

            // 스킬 새로운 로직
            if (passiveSkill.ContainsKey(skillData.skillGroup))
            {
                passiveSkill[skillData.skillGroup] = skillData;
                skillCoroutines[skillData.skillGroup] = StartSkillCoroutine(skillData);
            }
            else
            {
                passiveSkill.Add(skillData.skillGroup, skillData);
                skillCoroutines.Add(skillData.skillGroup, StartSkillCoroutine(skillData));

                if (skillData.prefabAddress != null)
                {
                    GameObject passiveEffect = Instantiate(Resources.Load<GameObject>(skillData.prefabAddress), transform.position + Vector3.up, Quaternion.Euler(-90f, 0f, 0f));
                    passiveEffect.transform.SetParent(transform);

                    passiveObj.Add(skillData.skillGroup, passiveEffect);
                }
            }

            switch (skillData.skillGroup)
            {
                case 30001050: // 블랙홀 크기 변경
                    {
                        float scale = 1.8f + 0.6f * (skillData.level / 2f);
                        passiveObj[skillData.skillGroup].transform.localScale = new Vector3(scale, scale, 1);
                    }
                    break;
            }
        }
    }

    public Coroutine StartSkillCoroutine(SkillTable skillData)
    {
        if (skillCoroutines.ContainsKey(skillData.skillGroup) && skillCoroutines[skillData.skillGroup] != null)
        {
            StopCoroutine(skillCoroutines[skillData.skillGroup]);
        }
        return StartCoroutine(SkillRoutine(skillData));
    }

    IEnumerator SkillRoutine(SkillTable skillData)
    {
        // 패시브의 경우 시작할때 한번 이펙트 발현
        //최종 데미지 = (기본 공격력 + 아이템 공격력 보정치) x (공격력 배율) - (방어력*방어력배율) + 스킬 추가 데미지 + (크리티컬 데미지 보정치 * 크리티컬 여부)
        int projectileTotalCount = skillData.projectileCount;
        WaitForSeconds coolDownTime = new WaitForSeconds(skillData.coolDownTime);
        WaitForSeconds castDelay = new WaitForSeconds(skillData.castDelay);

        //Debug.Log($"Coroutine started with parameter: {skillData.skillId}");
        //Debug.Log(projectileTotalCount);
        while (true)
        {
            yield return coolDownTime;

            // TODO : 계수 연산할수 있게 함수 따로 빼기
            int damage = Mathf.RoundToInt((playeringameinfo.attackPower + skillData.attackDamage) * 0.8f);

            switch (skillData.targetType)
            {
                // Active
                case SkillTargetType.Single:
                    {
                        if (skillData.skillGroup == 30000010)
                        {
                            projectileTotalCount = skillData.projectileCount + playeringameinfo.addprojectilePenetration;
                        }

                        // 발사 작동
                        for (int i = 0; i < projectileTotalCount; i++)
                        {
                            Vector3 direction = DetectEnemyDirection();
                            skillPool.GetPoolProjectileSkill(skillData.skillId, projectilePoint, direction, damage);
                            yield return castDelay;
                        }
                    }
                    break;
                case SkillTargetType.FixedDirection:
                    {
                        float projectileAngle = 360f / skillData.projectileCount;
                        float startAngle = UnityEngine.Random.Range(0f, projectileAngle);

                        for (int i = 0; i < skillData.projectileCount; i++)
                        {
                            float radAngle = (startAngle + i * projectileAngle) * Mathf.Deg2Rad;
                            Vector3 direction = new Vector3(Mathf.Cos(radAngle), 0f, Mathf.Sin(radAngle));
                            skillPool.GetPoolProjectileSkill(skillData.skillId, projectilePoint, direction, damage);
                        }
                    }
                    break;
                case SkillTargetType.RandomSingle:
                    {
                        // 번개의 일격 GroundStrike

                        // 발사 작동
                        for (int i = 0; i < projectileTotalCount; i++)
                        {
                            // 랜덤 단일 타겟
                            Vector3 enemyPos = DetectRandomEnemyPos();
                            skillPool.GetPoolGroundStrikeSkill(skillData.skillId, enemyPos, damage);
                            yield return castDelay;
                        }
                    }
                    break;
                case SkillTargetType.RandomPos:
                    {
                        // TODO : 유성낙하 
                        //if (skillData.prefabAsset == "SineVFX")
                        //{
                        //    Vector3 randomPos = DetectRandomPos();
                        //    Instantiate(Resources.Load<GameObject>(skillData.prefabAddress), randomPos, Quaternion.identity);
                        //    // 생성만
                        //}
                        //else
                        //{
                        //    // 발사 작동
                        //    for (int i = 0; i < projectileTotalCount; i++)
                        //    {
                        //        // 랜덤 포지션
                        //        Vector3 randomPos = DetectRandomPos();
                        //        skillPool.GetPoolSkyFallSkill(skillData.skillId, randomPos, damage);
                        //        yield return castDelay;
                        //    }
                        //}
                    }
                    break;
                case SkillTargetType.AOE:
                    {
                        skillPool.GetPoolAreaSkill(skillData.skillId, transform.position, damage / 10);
                        yield return castDelay;
                    }
                    break;
                case SkillTargetType.Around:
                    {
                        if (skillData.coolDownTime == 0)
                        {
                            // 발동시키고 종료
                            skillPool.GetPoolAroundSkill(skillData.skillId, transform.position, damage);
                            yield break;
                        }
                        else
                        {
                            skillPool.GetPoolAroundSkill(skillData.skillId, transform.position, damage);
                            yield return castDelay;
                        }
                    }
                    break;
                // Passive
                case SkillTargetType.AddProjectile:
                    {
                        // 범위 증가는 유니크한 스킬이라는 가정
                        playeringameinfo.addprojectilePenetration = skillData.projectileCount;
                    }
                    yield break;

                case SkillTargetType.DotHeal:
                    {
                        if (playeringameinfo.curHp < playeringameinfo.maxHp)
                        {
                            int regenAmount = Mathf.Min(skillData.regenHP, playeringameinfo.maxHp - playeringameinfo.curHp);
                            Heal(regenAmount);
                        }
                    }
                    break;
                case SkillTargetType.AddHP:
                    {
                        playeringameinfo.addSkillHp = 0;
                        foreach (SkillTable skillTable in passiveSkillSlot)
                        {
                            playeringameinfo.addSkillHp += skillTable.addHP;
                        }
                        playeringameinfo.maxHp = playerStatInfo.hp + playerStatInfo.addLevelHp + playerStatInfo.addHp + playeringameinfo.addSkillHp;

                        UpdateHPBar();
                    }
                    yield break;

                case SkillTargetType.AddDamage:
                    {
                        playeringameinfo.addSkillAttackPower = 0;
                        foreach (SkillTable skillTable in passiveSkillSlot)
                        {
                            playeringameinfo.addSkillAttackPower += skillTable.attackDamage;
                        }
                        playeringameinfo.attackPower = playerStatInfo.attackPower + playerStatInfo.addLevelAttack + playerStatInfo.addAttackPower + playeringameinfo.addSkillAttackPower;
                    }
                    yield break;

                case SkillTargetType.CollectableRange:
                    {
                        // 범위 증가는 유니크한 스킬이라는 가정
                        /*
                            (skillData.level + 1) * 2f; 원래 획득 4,6, ... = 이펙트크기 (1+level) * 1.2f
                        */
                        playeringameinfo.addSkillCollectableRange = (1.8f + 0.6f * (skillData.level / 2f)) * 2 / 1.2f;
                    }
                    yield break;

                case SkillTargetType.AddDef:
                    {
                        playeringameinfo.addSkillDefense = 0;
                        foreach (SkillTable skillTable in passiveSkillSlot)
                        {
                            playeringameinfo.addSkillDefense += skillTable.addDef;
                        }
                        playeringameinfo.defense = playerStatInfo.defense + playerStatInfo.addDefense + playeringameinfo.addSkillDefense;
                    }
                    yield break;

                default:
                    yield break;
            }
        }
    }

    #endregion

    #region Control

    public void JoyStick(VariableJoystick joy)
    {
        this.joy = joy;
    }

    public void UpdateHPBar()
    {
        float per = (float)playeringameinfo.curHp / playeringameinfo.maxHp;
        hpGuageSlider.value = per;
    }

    public void ResetPlayerHP()
    {
        playeringameinfo.curHp = playeringameinfo.maxHp;
        UpdateHPBar();
    }

    public void Heal(int healamount)
    {
        if (playeringameinfo.curHp + healamount > playeringameinfo.maxHp)
        {
            healamount = playeringameinfo.curHp + healamount - playeringameinfo.maxHp;
        }
        playeringameinfo.curHp += healamount;
        UpdateHPBar();

        Debug.Log($"Heal : {healamount}");
        GameObject hudText = Instantiate(Resources.Load<GameObject>("Prefabs/UI/DamageText"));
        hudText.transform.position = hudPos.position; // 표시될 위치
        hudText.GetComponentInChildren<DamageText>().Init(healamount, Color.green); // 초록색
    }

    private float DefenseFactor()
    {
        // 방어력이 아무리 높아도 일정량의 데미지를 받는다.
        return 300f / (300f + playeringameinfo.defense);
    }

    public void TakeDamage(int damageAmount)
    {
        int realDamage = Mathf.FloorToInt(Mathf.Max(0, damageAmount * DefenseFactor()));
        playeringameinfo.curHp -= realDamage;
        UpdateHPBar();

        // 피격 모션
        anim.SetTrigger(Hit);

        // 피격 사운드
        int ran = UnityEngine.Random.Range(1, 3);
        SoundManager.Instance.PlayBattleSound("Hit" + ran.ToString());

        realDamage = -realDamage;

        GameObject hudText = Instantiate(Resources.Load<GameObject>("Prefabs/UI/DamageText"));
        hudText.transform.position = hudPos.position; // 표시될 위치
        hudText.GetComponentInChildren<DamageText>().Init(realDamage, Color.red);

        //Debug.Log("플레이어 현재 HP" + per);
        if (playeringameinfo.curHp <= 0)
            OnDead(GameState.KillDie);
    }

    void OnDead(GameState dieState)
    {
        // 피격 사운드
        SoundManager.Instance.PlayBattleSound("Die");
        Debug.Log("플레이어사망. 게임오버UI");
        //GameManager.Instance.accountInfo.AddGold(playeringameinfo.gold);// 사망하면 포기

        var uiDefeated = UIManager.Instance.ShowUI<UIDefeated>();
        ++UIManager.Instance.popupUICount;
        // todo 추후 UIDefeated 에서 처리하게 해주기
        if (dieState == GameState.DropDie)
        {
            uiDefeated.adButton.SetActive(false);
        }
    }

    protected Vector3 DetectRandomEnemyPos()
    {
        nearEnemy.Clear();
        // TODO: OverlapSphereNonAlloc로 변환가능하면 변환
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectRandomRange, enemyLayerMask);
        foreach (Collider col in hitColliders)
        {
            nearEnemy.Add(col.transform);
        }

        if (nearEnemy.Count > 0)
        {
            // nearEnemy 에서 랜덤으로 하나만 선택
            int randomIndex = UnityEngine.Random.Range(0, nearEnemy.Count);
            return nearEnemy[randomIndex].position;
        }
        else
        {
            return DetectRandomPos();
        }
    }

    protected Vector3 DetectRandomPos()
    {
        // 적 아무 곳이나
        Vector3 randomPosition = UnityEngine.Random.onUnitSphere * detectRandomRange + transform.position;
        randomPosition.y = 100f;
        RaycastHit hit;
        NavMeshHit navhit;
        if (Physics.Raycast(new Ray(randomPosition, Vector3.down), out hit, Mathf.Infinity))
        {
            if (NavMesh.SamplePosition(hit.point, out navhit, detectRandomRange, NavMesh.AllAreas))
            {
                randomPosition = navhit.position;
            }
        }

        return randomPosition;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected Vector3 DetectEnemyDirection()
    {
        nearEnemy.Clear();
        // TODO: OverlapSphereNonAlloc로 변환가능하면 변환
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, enemyLayerMask);
        foreach (Collider col in hitColliders)
        {
            nearEnemy.Add(col.transform);
        }
        //Debug.Log($"hitColliders : {hitColliders.Length} / nearEnemy : {nearEnemy.Count}");

        if (nearEnemy.Count > 0)
        {
            Transform nearestEnemy = GetNearestEnemy();
            Vector3 direction = (nearestEnemy.position - transform.position).normalized;
            direction.y = 0;
            return direction;
        }
        else
        {
            //return UnityEngine.Random.insideUnitSphere.normalized; // 땅으로 쏨

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
        int accountLevel = GameManager.Instance.accountInfo.level;
        if (applyType == SkillApplyType.Active)
        {
            slotCount += Mathf.Min((accountLevel + 3) / 6, 3);
        }
        else if (applyType == SkillApplyType.Passive)
        {
            slotCount += Mathf.Min(accountLevel / 6, 3);
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

    public void AddCore(int amount)
    {
        playeringameinfo.core += amount;
    }


    public void RemoveChaseTarget(GameObject go)
    {
        if (chaseTarget.Contains(go))
        {
            chaseTarget.Remove(go);
            if (chaseTarget.Count == 0)
            {
                GameManager.Instance.accountInfo.AddGold(playeringameinfo.gold);
                GameManager.Instance.accountInfo.AddGem(playeringameinfo.gem);
                GameManager.Instance.accountInfo.AddCore(playeringameinfo.core);

                GameManager.Instance.selectNextStage();

                GameManager.Instance.SetState(GameState.IngameEnd);
                UIManager.Instance.ShowUI<UIGameClear>();
                ++UIManager.Instance.popupUICount;
            }
        }
    }

    public void DoubleReward()
    {
        GameManager.Instance.accountInfo.AddGold(playeringameinfo.gold);
        GameManager.Instance.accountInfo.AddGem(playeringameinfo.gem);
        GameManager.Instance.accountInfo.AddCore(playeringameinfo.core);
    }
    #endregion
}
