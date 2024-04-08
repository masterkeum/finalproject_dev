using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class GroundStrikeScript : MonoBehaviour
{
    public float disableAfterTime = 5f;

    public VisualEffect vfx;
    public SphereCollider sc;

    public Transform parentObject;

    public LayerMask enemyLayerMask;
    public LayerMask groundLayerMask;

    SkillTable skillInfo;
    public int projectilePenetration; // 관통력

    private int damage;

    private void Awake()
    {
        //Debug.Log("SkyFallScript.Awake");
        sc = transform.GetComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.enabled = false;
        vfx.enabled = false;

        enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
    }

    public void Init(int _skillId, int _damage)
    {
        //Debug.Log("ProjectileScript.Init");
        skillInfo = DataManager.Instance.GetSkillTable(_skillId);
        damage = _damage;

        projectilePenetration = skillInfo.projectilePenetration;

        switch (skillInfo.skillId)
        {
            case 30000521: // [초월]천둥의 격노
                transform.localScale = new Vector3(2f, 4f, 2f);
                break;
            default:
                transform.localScale = new Vector3(1f, 3f, 1f);
                break;
        }
    }

    private void OnEnable()
    {
        //Debug.Log("ProjectileScript.OnEnable");
        sc.enabled = true;
        vfx.enabled = true;

        StartCoroutine(LateCall());
    }

    void OnTransformParentChanged()
    {
        parentObject = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyLayerMask == (enemyLayerMask | (1 << other.gameObject.layer)))
        {
            if (projectilePenetration > 0)
            {
                other.GetComponent<EnemyBaseController>().TakeDamage(damage);

                --projectilePenetration;
                if (projectilePenetration <= 0)
                {
                    sc.enabled = false;
                    StartCoroutine(VFXDelayStop(1.5f));
                    StopCoroutine(LateCall());
                }
            }
        }
        if (groundLayerMask == (groundLayerMask | (1 << other.gameObject.layer)))
        {
            --projectilePenetration;
            sc.enabled = false;
            StartCoroutine(VFXDelayStop(1.5f));
            StopCoroutine(LateCall());
        }
    }

    private IEnumerator LateCall()
    {
        yield return new WaitForSeconds(disableAfterTime);
        sc.enabled = false;
        vfx.enabled = false;
        gameObject.SetActive(false);
        yield break;
    }
    private IEnumerator VFXDelayStop(float delay)
    {
        yield return new WaitForSeconds(delay);
        vfx.enabled = false;
        yield break;
    }

}
