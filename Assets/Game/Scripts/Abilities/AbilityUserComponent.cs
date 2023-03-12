using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUserComponent : MonoBehaviour
{
    public bool HasActiveAbility { get; private set; }

    public Transform projectileSpawnPoint;

    public void UseAbility(AbilityAsset abilityAsset)
    {
        PhotonNetwork.Instantiate(abilityAsset.effectPrefab.name, projectileSpawnPoint.position, Quaternion.identity);
    }

    public void Tick(float deltaTime)
    {
        
    }
}