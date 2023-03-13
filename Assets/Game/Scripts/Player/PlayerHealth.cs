using System.Collections;
using UnityEngine;

[System.Serializable]
public class PlayerHealth
{
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    public float HealthPercentage => CurrentHealth / MaxHealth;

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

    public void SetHealth(float amount)
    {
        CurrentHealth = amount;
    }
}