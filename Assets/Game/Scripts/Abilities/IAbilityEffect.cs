using System.Collections;
using UnityEngine;

public interface IAbilityEffect
{
    PlayerController PlayerController { get; set; }
    Vector3 Direction { get; set; }
    AbilityAsset AbilityAsset { get; set; }

    void Setup(PlayerController playerController, AbilityAsset abilityAsset, Vector3 right);
    void TriggerDamage(IDamageable damageable);
}
