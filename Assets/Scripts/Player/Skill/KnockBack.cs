using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();


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

    private void Update()
    {
        if (ps != null)
        {
            ParticleSystem.CollisionModule collisionModule = ps.collision;
            //collisionModule.radiusScale = ps.sizeOverLifetime.size.curve.;
        }
    }
}
