using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject muzzleParticle;

    public float disableAfterTime = 5f;
    public float hitOffset = 0f;

    public Rigidbody rb; // 리지드바디
    public SphereCollider sc;
    private RigidbodyConstraints originalConstraints;

    public Transform parentObject;
    private float originalSpeed;


    public LayerMask enemyLayerMask;
    SkillTable skillInfo;
    public float projectileSpeed = 15f; // 오브젝트 속도
    public int projectilePenetration; // 관통력

    private int damage;

    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody>();
        sc = transform.GetComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.enabled = false;

        originalConstraints = rb.constraints;


        enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
    }

    public void Init(int _skillId, int _damage)
    {
        skillInfo = DataManager.Instance.GetSkillTable(_skillId);
        damage = _damage;

        originalSpeed = skillInfo.projectileSpeed;
        projectileSpeed = 0;
        projectilePenetration = skillInfo.projectilePenetration;

    }

    private void OnEnable()
    {
        rb.constraints = originalConstraints;
        projectileSpeed = originalSpeed;
        sc.enabled = true;
        StartCoroutine(LateCall());
    }

    private void Start()
    {
        // TODO 수정필요한듯
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation);
        projectileParticle.transform.parent = transform;

        if (skillInfo.level == 5)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }

        if (muzzleParticle)
        {
            muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation);
            //Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
            StartCoroutine(ActiveFalse(muzzleParticle, 1.5f));
        }
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

                other.GetComponent<EnemyBaseController>().TakeDamage(damage);

                impactParticle = Instantiate(impactParticle, pos, rot);
                impactParticle.transform.LookAt(pos);

                StartCoroutine(ActiveFalse(projectileParticle, 3f));
                StartCoroutine(ActiveFalse(impactParticle, 5f));

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
    }

    private IEnumerator LateCall()
    {
        yield return new WaitForSeconds(disableAfterTime);
        sc.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        projectileSpeed = 0;
        yield break;
    }

    private IEnumerator ActiveFalse(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
        yield break;
    }
}
