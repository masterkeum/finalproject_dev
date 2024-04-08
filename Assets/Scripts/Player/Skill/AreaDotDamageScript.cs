using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AreaDotDamageScript : MonoBehaviour
{
    private float disableAfterTime = 2f;

    // 범위 도트 딜
    private SphereCollider sc;

    private Transform parentObject;

    private LayerMask enemyLayerMask;

    SkillTable skillInfo;
    private int damage;

    private float skillRad = 1.5f;
    private float damageInterval = 0.2f;
    private float skillRange = 1.2f;

    public float knockBackForce = 20f;

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

        StartCoroutine(LateCall());
        StartCoroutine(DotDeal());
    }

    private void OnTransformParentChanged()
    {
        parentObject = transform.parent;
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
                //Vector3 knockBackDirection = (collider.transform.position - transform.position + Vector3.up*2).normalized;
                //EBC.Knockback(disableAfterTime, damageInterval, knockBackDirection, knockBackForce);
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
