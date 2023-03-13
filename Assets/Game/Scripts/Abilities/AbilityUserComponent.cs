using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUserComponent : MonoBehaviour
{
    public bool HasActiveAbility { get; private set; }

    public Transform projectileSpawnPoint;

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    public void UseAbility(AbilityAsset abilityAsset)
    {
        var effect = PhotonNetwork.Instantiate(abilityAsset.effectPrefab.name, projectileSpawnPoint.position, Quaternion.identity);

        if (effect.TryGetComponent(out IAbilityEffect abilityEffect))
        {
            abilityEffect.Direction = Vector3.right;
            abilityEffect.PlayerController = _playerController;
            abilityEffect.AbilityAsset = abilityAsset;
        }
    }
}