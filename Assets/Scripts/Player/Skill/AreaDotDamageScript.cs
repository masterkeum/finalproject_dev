using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static Gley.MobileAds.Events;

public class AreaDotDamageScript : MonoBehaviour
{
    private float disableAfterTime = 2f;

    // 범위 도트 딜
    private SphereCollider sc;
    private float initialSize = 0f;
    private float timeElapsed = 0f;
    public float targetSize = 1.5f;

    private Transform parentObject;

    private LayerMask enemyLayerMask;

    SkillTable skillInfo;
    private int damage;

    private float skillRad = 1.5f;
    private float damageInterval = 0.2f;
    private float skillRange = 1.2f;
    private float knockBackForce = 5f;


    private void Awake()
    {
        //Debug.Log("AreaDotDamageScript.Awake");
        sc = transform.GetComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.enabled = false;
        skillRad = sc.radius;
        enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
    }

    public void Init(int _skillId, int _damage)
    {
        //Debug.Log("AreaDotDamageScript.Init");
        skillInfo = DataManager.Instance.GetSkillTable(_skillId);
        damage = _damage;

        sc.radius = 0f;
        timeElapsed = 0f;
        switch (skillInfo.skillGroup)
        {
            case 30000050: // 생츄어리 크기 변경
                {
                    float scale = 1.2f * (skillInfo.level + 1);
                    skillRange = scale;
                    transform.localScale = new Vector3(scale, 1, scale);
                }
                break;
        }

        if (skillInfo.skillId == 30000541)
        {
            // [초월]생츄어리 군주
            damageInterval = 1.5f;
        }
    }

    private void OnEnable()
    {
        //Debug.Log("ProjectileScript.OnEnable");
        sc.enabled = true;

        StartCoroutine(SCSize());
        StartCoroutine(LateCall());
        StartCoroutine(DotDeal());
    }

    private void OnTransformParentChanged()
    {
        parentObject = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyLayerMask == (enemyLayerMask | (1 << other.gameObject.layer)))
        {
            // TODO: 넉백 랜덤으로 추가하기

            System.Random random = new System.Random();
            int num = random.Next(0, 101);
            if (num <= 90)
            {
                return;
            }

            Debug.Log($"OnTriggerEnter : {other.name}");
            EnemyBaseController EBC = other.GetComponent<EnemyBaseController>();
            Collider collider = other.GetComponent<Collider>();

            if (EBC != null && collider != null)
            {
                Vector3 knockBackDirection = (collider.transform.position - GameManager.Instance.player.transform.position + Vector3.up).normalized;
                EBC.Knockback(2f, 0.2f, knockBackDirection, knockBackForce);
            }
        }
    }

    private IEnumerator SCSize()
    {
        while (timeElapsed < disableAfterTime)
        {
            //float newSize = Mathf.Lerp(initialSize, targetSize, timeElapsed / disableAfterTime);
            float t = timeElapsed / disableAfterTime;
            float curveT = 1 - Mathf.Pow(1 - t, 3); // easeOutCubic 커브
            float newSize = Mathf.Lerp(initialSize, targetSize, curveT);

            sc.radius = newSize;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator DotDeal()
    {
        while (true)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, skillRange * skillRad, enemyLayerMask);
            foreach (Collider collider in hitColliders)
            {
                // 적에게 데미지 입히기
                EnemyBaseController EBC = collider.GetComponent<EnemyBaseController>();
                EBC.TakeDamage(damage);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private IEnumerator LateCall()
    {
        yield return new WaitForSeconds(disableAfterTime);
        sc.enabled = false;
        gameObject.SetActive(false);
        yield break;
    }

}
