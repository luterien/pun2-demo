using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPunObservable, IDamageable
{
    public static event Action<PlayerController> OnLocalPlayerCreated;

    public PhotonView PhotonView => photonView;

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
        playerModelComponent.Initialize(PhotonView.IsMine);

        inputController = new PlayerInputController();
        playerHealth = new PlayerHealth(100f);
    }

    private void Update()
    {
        inputController.Tick(Time.deltaTime);

        CheckForAbilityUseRequest();
        ApplyMovement();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

        }
        else
        {

        }
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
