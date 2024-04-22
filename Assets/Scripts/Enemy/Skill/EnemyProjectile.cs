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

    public LayerMask enemyLayerMask;
    public LayerMask groundLayerMask;
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

        enemyLayerMask = 1 << LayerMask.NameToLayer("Player");
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation);
        projectileParticle.transform.parent = transform;
    }

    public void Init(int _damage, float speed)
    {
        damage = _damage;
        projectileSpeed = speed;
        sc.enabled = true;

        if (muzzleParticle)
        {
            GameObject mp = Instantiate(muzzleParticle, transform.position, transform.rotation);
            Destroy(mp, 1.5f); // Lifetime of muzzle effect.
        }
        projectileParticle.transform.position = transform.position;
        projectileParticle.transform.rotation = transform.rotation;

        StartCoroutine(LateCall());
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

                other.GetComponent<Player>().TakeDamage(damage);

                Destroy(projectileParticle, 3f);
                Destroy(ip, 5.0f);

                --projectilePenetration;
                if (projectilePenetration <= 0)
                {
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    projectileSpeed = 0;
                    sc.enabled = false;

                    StopCoroutine(LateCall());
                    Destroy(gameObject);
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

            Destroy(projectileParticle, 3f);
            Destroy(ip, 5.0f);

            rb.constraints = RigidbodyConstraints.FreezeAll;
            projectileSpeed = 0;
            sc.enabled = false;

            StopCoroutine(LateCall());
            Destroy(gameObject);
        }
    }

    private IEnumerator LateCall()
    {
        yield return new WaitForSeconds(disableAfterTime);
        Destroy(gameObject);
        yield break;
    }

}
