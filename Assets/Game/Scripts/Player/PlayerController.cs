using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPunObservable, IDamageable
{
    public event Action<PlayerController> OnPlayerKilled;

    public PhotonView PhotonView => photonView;
    public PlayerHealth PlayerHealth => playerHealth;

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
        if (!PhotonView.IsMine)
        {
            return;
        }

        inputController.Tick(Time.deltaTime);

        CheckForAbilityUseRequest();
        ApplyMovement();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (playerHealth == null)
        {
            return;
        }

        if (stream.IsWriting)
        {
            stream.SendNext(playerHealth.CurrentHealth);
        }
        else
        {
            playerHealth.SetHealth((float)stream.ReceiveNext());
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
        var isDead = playerHealth.ApplyDamage(damage);

        if (isDead)
        {
            OnPlayerKilled?.Invoke(this);
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!PhotonView.IsMine)
        {
            return;
        }

        if (other.TryGetComponent(out IAbilityEffect abilityEffect))
        {
            abilityEffect.TriggerDamage(this);
        }
    }
}
