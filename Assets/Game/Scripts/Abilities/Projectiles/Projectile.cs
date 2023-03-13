using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IAbilityEffect, IPunObservable
{
    public float projectileSpeed = 5f;

    public Vector3 Direction { get; set; }
    public PlayerController PlayerController { get; set; }
    public AbilityAsset AbilityAsset { get; set; }

    private bool _canBeDestroyed;

    public void Setup(PlayerController playerController, AbilityAsset abilityAsset, Vector3 direction)
    {
        PlayerController = playerController;
        Direction = direction;
        AbilityAsset = abilityAsset;

        if (PlayerController.PhotonView.IsMine)
        {
            StartCoroutine(DestroyWithDelay());
        }
    }

    private void Update()
    {
        transform.position += Direction * Time.deltaTime * projectileSpeed;

        if (_canBeDestroyed && PlayerController.PhotonView.IsMine)
        {
            _canBeDestroyed = false;

            PhotonNetwork.Destroy(gameObject);
        }
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(3f);

        _canBeDestroyed = true;
    }

    public void TriggerDamage(IDamageable damageable)
    {
        var baseDamage = 20f;
        var damage = Random.Range(baseDamage * 0.8f, baseDamage * 1.2f);

        damageable.ApplyDamage(damage);

        _canBeDestroyed = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_canBeDestroyed);
        }
        else
        {
            _canBeDestroyed = (bool)stream.ReceiveNext();
        }
    }
}