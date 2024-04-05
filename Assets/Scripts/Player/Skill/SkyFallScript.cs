using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class SkyFallScript : MonoBehaviour
{
    public float disableAfterTime = 5f;

    public VisualEffect vfx;

    public Rigidbody rb; // 리지드바디
    public SphereCollider sc;
    private RigidbodyConstraints originalConstraints;

    public Transform parentObject;

    public LayerMask enemyLayerMask;
    public LayerMask groundLayerMask;

    SkillTable skillInfo;
    public int projectilePenetration; // 관통력

    private int damage;

    private void Awake()
    {
        //Debug.Log("SkyFallScript.Awake");
        rb = transform.GetComponent<Rigidbody>();
        sc = transform.GetComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.enabled = false;
        //vfx.Stop();
        vfx.enabled = false;

        originalConstraints = rb.constraints;

        enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
    }

    public void Init(int _skillId, int _damage)
    {
        //Debug.Log("ProjectileScript.Init");
        skillInfo = DataManager.Instance.GetSkillTable(_skillId);
        damage = _damage;

        projectilePenetration = skillInfo.projectilePenetration;
    }

    private void OnEnable()
    {
        //Debug.Log("ProjectileScript.OnEnable");
        //rb.constraints = originalConstraints;
        //vfx.Reinit();
        //vfx.Play();
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
                    //rb.constraints = RigidbodyConstraints.FreezeAll;
                    sc.enabled = false;
                    StartCoroutine(VFXDelayStop(1.5f));
                    StopCoroutine(LateCall());
                    //gameObject.SetActive(false);
                }
            }
        }
        if (groundLayerMask == (groundLayerMask | (1 << other.gameObject.layer)))
        {
            --projectilePenetration;
            sc.enabled = false;
            StartCoroutine(VFXDelayStop(1.5f));
            StopCoroutine(LateCall());
            //gameObject.SetActive(false);
        }
    }

    private IEnumerator LateCall()
    {
        yield return new WaitForSeconds(disableAfterTime);
        sc.enabled = false;
        //rb.constraints = RigidbodyConstraints.FreezeAll;
        //vfx.Stop();
        vfx.enabled = false;
        gameObject.SetActive(false);
        yield break;
    }
    private IEnumerator VFXDelayStop(float delay)
    {
        yield return new WaitForSeconds(delay);
        vfx.enabled = false;
        //vfx.Stop();
        yield break;
    }

}
