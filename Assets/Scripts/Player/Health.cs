using System;
using System.Collections;
using System.Collections.Generic;using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour

{
    [SerializeField] private int maxHealth = 100;
    private int health;
    public event Action OnDie;

    public bool IsDead => health == 0;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamgae(int damage)
    {
        if (health == 0) return;
        health = Mathf.Max(health - damage, 0);
        
        if(health == 0)
            OnDie?.Invoke();
        Debug.Log(health);
    }
}
