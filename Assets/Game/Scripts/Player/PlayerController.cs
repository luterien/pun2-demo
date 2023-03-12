using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPunObservable, IDamageable
{
    public static event Action<PlayerController> OnLocalPlayerCreated;

    [Header("Dependencies")]
    public PlayerModel playerModelComponent;
    public AbilityUserComponent abilityUserComponent;

    public Transform projectileSpawnPoint;

    private PhotonView photonView;
    private IInputController inputController;
    private PlayerHealth playerHealth;

    private bool HasActiveAbility => abilityUserComponent.HasActiveAbility;

    private AbilityAsset RequestedAbility { get; set; }
    private bool abilityRequested;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            GameSceneController.LocalPlayerInstance = gameObject;
        }
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            playerModelComponent.Initialize();

            inputController = new PlayerInputController();
            playerHealth = new PlayerHealth(100f);

            OnLocalPlayerCreated?.Invoke(this);
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            inputController.Tick(Time.deltaTime);
            abilityUserComponent.Tick(Time.deltaTime);

            CheckForAbilityUseRequest();
            ApplyMovement();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }

    private void CheckForAbilityUseRequest()
    {
        if (HasActiveAbility)
        {
            return;
        }

        if (abilityRequested)
        {
            abilityUserComponent.UseAbility(RequestedAbility);

            abilityRequested = false;
            RequestedAbility = null;
        }
    }

    private void ApplyMovement()
    {
        GameSceneController.LocalPlayerInstance.transform.position += inputController.HorizontalAxis * Time.deltaTime * Vector3.right;
    }

    public void ApplyDamage(float damage)
    {
        playerHealth.ApplyDamage(damage);
    }

    public bool TryUseAbility(AbilityAsset abilityAsset)
    {
        if (!photonView.IsMine)
        {
            return false;        
        }
        
        if (abilityRequested)
        {
            return false;
        }

        RequestedAbility = abilityAsset;
        abilityRequested = true;

        return true;
    }
}
