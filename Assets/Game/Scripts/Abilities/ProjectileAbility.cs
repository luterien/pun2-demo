using Photon.Pun;
using System.Collections;
using UnityEngine;

public class ProjectileAbility : IAbility
{
    public bool IsComplete { get; private set; }

    private AbilityAsset _abilityAsset;
    private Transform _spawnPoint;
    private PlayerController _playerController;

    public ProjectileAbility(PlayerController playerController, AbilityAsset abilityAsset, Transform spawnPoint)
    {
        _abilityAsset = abilityAsset;
        _spawnPoint = spawnPoint;
        _playerController = playerController;
    }

    public void OnEnter()
    {
        var effect = PhotonNetwork.Instantiate(_abilityAsset.effectPrefab.name, _spawnPoint.position, Quaternion.identity);

        if (effect.TryGetComponent(out IAbilityEffect abilityEffect))
        {
            abilityEffect.Setup(_playerController, _abilityAsset, Vector3.right);
        }

        IsComplete = true;
    }

    public void Tick(float deltaTime)
    {

    }

    public void OnExit()
    {

    }
}