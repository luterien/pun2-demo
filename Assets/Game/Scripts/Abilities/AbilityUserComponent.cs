using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUserComponent : MonoBehaviour
{
    public bool HasActiveAbility { get; private set; }

    public Transform projectileSpawnPoint;

    private PlayerController _playerController;

    private IAbility _ability;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_ability != null)
        {
            _ability.Tick(Time.deltaTime);

            if (_ability.IsComplete)
            {
                _ability.OnExit();
                _ability = null;
            }
        }
    }

    public void UseAbility(AbilityAsset abilityAsset)
    {
        _ability = abilityAsset.GetAbility(_playerController, projectileSpawnPoint);
        _ability.OnEnter();
    }
}