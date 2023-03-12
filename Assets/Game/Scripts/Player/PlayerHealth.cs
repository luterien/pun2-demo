using System.Collections;
using UnityEngine;

public class PlayerHealth
{
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    public PlayerHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        if (damage > CurrentHealth)
        {
            CurrentHealth = 0f;
            return;
        }

        CurrentHealth -= damage;
    }
}