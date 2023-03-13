using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Abilities/Create New")]
public class AbilityAsset : ScriptableObject
{
    [Header("Basic Settings")]
    public string abilityName;
    public Sprite abilityIcon;

    [Header("Ability Settings")]
    public int cooldown;
    public GameObject effectPrefab;

    [Header("Damage Settings")]
    public float damage;

    [Space(5)]
    public List<AbilityTag> tags;
}