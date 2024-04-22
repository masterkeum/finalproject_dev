using System.Collections;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject muzzleParticle;

    public float disableAfterTime = 5f;
    private float hitOffset = 1f;

    public Rigidbody rb; // 리지드바디
    public SphereCollider sc;
    private RigidbodyConstraints originalConstraints;

    public Transform parentObject;
    private float originalSpeed;


    public LayerMask enemyLayerMask;
    public LayerMask groundLayerMask;
    SkillTable skillInfo;
    public float projectileSpeed = 15f; // 오브젝트 속도
    public int projectilePenetration; // 관통력

    private int damage;

    private void Awake()
    {
        //Debug.Log("ProjectileScript.Awake");

        rb = transform.GetComponent<Rigidbody>();
        sc = transform.GetComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.enabled = false;

        originalConstraints = rb.constraints;

        enemyLayerMask = 1 << LayerMask.NameToLayer("Player");
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation);
        projectileParticle.transform.parent = transform;
        projectileParticle.SetActive(false);
    }

    public void Init(int _skillId, int _damage)
    {
        //Debug.Log("ProjectileScript.Init");
        skillInfo = DataManager.Instance.GetSkillTable(_skillId);
        damage = _damage;

        originalSpeed = skillInfo.projectileSpeed;
        projectileSpeed = 0;
        projectilePenetration = skillInfo.projectilePenetration;

        //gameObject.SetActive(true);

        projectileParticle.transform.position = transform.position;
        projectileParticle.transform.rotation = transform.rotation;
        projectileParticle.SetActive(true);

        switch (skillInfo.skillId)
        {
            case 30000015: // 화염구 lv5
            case 30000501: // 화염구 초월
                transform.localScale = new Vector3(2, 2, 2);
                break;
            default:
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                break;
        }
    }

    private void OnEnable()
    {
        //Debug.Log("ProjectileScript.OnEnable");
        rb.constraints = originalConstraints;
        projectileSpeed = originalSpeed;
        sc.enabled = true;

        if (muzzleParticle)
        {
            GameObject mp = Instantiate(muzzleParticle, transform.position, transform.rotation);
            Destroy(mp, 1.5f); // Lifetime of muzzle effect.
        }

        StartCoroutine(LateCall());
    }

    void OnTransformParentChanged()
    {
        parentObject = transform.parent;
    }

    private void FixedUpdate()
    {
        if (projectileSpeed != 0)
        {
            rb.velocity = transform.forward * projectileSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyLayerMask == (enemyLayerMask | (1 << other.gameObject.layer)))
        {
            if (projectilePenetration > 0)
            {
                Vector3 triggerEnterPoint = other.ClosestPointOnBounds(transform.position);
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, triggerEnterPoint);
                Vector3 pos = triggerEnterPoint + new Vector3(0, hitOffset, 0);

                GameObject ip = Instantiate(impactParticle, pos, rot);

                other.GetComponent<EnemyBaseController>().TakeDamage(damage);

                // 특정 스킬의 경우 애프터이펙트 발생
                if (skillInfo.prefabAfterEffect != null)
                {
                    GameObject AE = Instantiate(Resources.Load<GameObject>(skillInfo.prefabAfterEffect), pos, Quaternion.Euler(-90f, 0f, 0f));
                    Destroy(AE, AE.GetComponent<ParticleSystem>().main.duration);
                }

                StartCoroutine(ActiveFalse(projectileParticle, 3f));
                Destroy(ip, 5.0f);

                --projectilePenetration;
                if (projectilePenetration <= 0)
                {
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    projectileSpeed = 0;
                    sc.enabled = false;

                    StopCoroutine(LateCall());
                    gameObject.SetActive(false);
                }

            }
        }

        if (groundLayerMask == (groundLayerMask | (1 << other.gameObject.layer)))
        {
            // 지면과 충돌
            Vector3 triggerEnterPoint = other.ClosestPointOnBounds(transform.position);
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, triggerEnterPoint);
            Vector3 pos = triggerEnterPoint + new Vector3(0, hitOffset, 0);


            GameObject ip = Instantiate(impactParticle, pos, rot);

            StartCoroutine(ActiveFalse(projectileParticle, 3f));
            Destroy(ip, 5.0f);

            rb.constraints = RigidbodyConstraints.FreezeAll;
            projectileSpeed = 0;
            sc.enabled = false;

            StopCoroutine(LateCall());
            gameObject.SetActive(false);
        }
    }

    private IEnumerator LateCall()
    {
        yield return new WaitForSeconds(disableAfterTime);
        sc.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        projectileSpeed = 0;
        gameObject.SetActive(false);
        yield break;
    }

    private IEnumerator ActiveFalse(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
        yield break;
    }

}
