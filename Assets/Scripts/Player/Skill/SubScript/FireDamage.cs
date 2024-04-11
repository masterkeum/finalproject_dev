using UnityEngine;

public class FireDamage : MonoBehaviour
{
    private LayerMask enemyLayerMask;
    private int damage;

    private void Awake()
    {
        enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
    }

    public void Init(int _damage)
    {
        damage = _damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (enemyLayerMask == (enemyLayerMask | (1 << other.gameObject.layer)))
        {
            Debug.Log("hellfire 활성화");
            other.GetComponent<EnemyBaseController>().TakeDamage(damage);
        }
    }

}
