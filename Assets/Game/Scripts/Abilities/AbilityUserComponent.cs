using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUserComponent : MonoBehaviour
{
    public bool HasActiveAbility { get; private set; }

    public Transform projectileSpawnPoint;
    public Transform projectileSpawnPointLeft;

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
        var projSpawnPoint = _playerController.FacingRight ? projectileSpawnPoint : projectileSpawnPointLeft;

        _ability = abilityAsset.GetAbility(_playerController, projSpawnPoint);
        _ability.OnEnter();
    }
}