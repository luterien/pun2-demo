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
            abilityEffect.Setup(_playerController, abilityAsset, Vector3.right);
        }
    }
}