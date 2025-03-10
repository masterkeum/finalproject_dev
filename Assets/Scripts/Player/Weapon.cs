using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private int damage;
    private float knockback;

    private List<Collider> alreadyColliderWith = new List<Collider>();

    private List<Weapon> weapons;

    private void OnEnable()
    {
        alreadyColliderWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;
        if (alreadyColliderWith.Contains(other)) return;
        
        alreadyColliderWith.Add(other);

        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamgae(damage);
        }

        // if (other.TryGetComponent(out ForceReceiver forceReceiver))
        // {
        //     Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
        //     forceReceiver.AddForce(direction * knockback);
        // }
    }

    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}
