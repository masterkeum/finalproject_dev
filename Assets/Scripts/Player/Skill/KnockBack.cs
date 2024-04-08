using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    private float knockBackForce = 20f;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        Debug.Log("Effect Trigger");
        ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        foreach (ParticleSystem.Particle p in enter)
        {
            Debug.Log("Effect Trigger2");


        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"Effect Collision : {other.name}");
        EnemyBaseController EBC = other.GetComponent<EnemyBaseController>();
        Collider collider = other.GetComponent<Collider>();

        if (EBC != null && collider != null)
        {
            Vector3 knockBackDirection = (collider.transform.position - GameManager.Instance.player.transform.position + Vector3.up).normalized;
            EBC.Knockback(2f, 0.2f, knockBackDirection, knockBackForce);
        }
    }
}
