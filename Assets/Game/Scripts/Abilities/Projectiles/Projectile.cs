using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IAbilityEffect, IPunObservable
{
    public float projectileSpeed = 5f;

    public Vector3 Direction { get; set; }
    public AbilityAsset AbilityAsset { get; set; }

    private bool _canBeDestroyed = false; 
    private bool _isMine = false;

    public void Setup(PlayerController playerController, AbilityAsset abilityAsset, Vector3 direction)
    {
        if (playerController.PhotonView.IsMine)
        {
            _isMine = true;

            var photonView = GetComponent<PhotonView>();
            photonView.RPC("SetupProjectile", RpcTarget.Others, abilityAsset.id, direction);

            SetupProjectile(abilityAsset.id, direction);

            StartCoroutine(DestroyWithDelay());
        }
    }

    [PunRPC]
    private void SetupProjectile(int abilityID, Vector3 direction)
    {
        Direction = direction;
        AbilityAsset = AbilityDatabase.Instance.GetAbilityAsset(abilityID);
    }

    private void Update()
    {
        transform.position += Direction * Time.deltaTime * projectileSpeed;

        if (_canBeDestroyed && _isMine)
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
        var baseDamage = AbilityAsset.damage;
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