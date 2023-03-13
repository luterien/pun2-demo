﻿using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IAbilityEffect
{
    public float projectileSpeed = 5f;

    public Vector3 Direction { get; set; }
    public PlayerController PlayerController { get; set; }
    public AbilityAsset AbilityAsset { get; set; }

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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogError("OnTriggerEnter");

        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(AbilityAsset.damage);
            PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            Debug.LogError("OnTriggerEnter TryGetComponent Failed");
        }
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(3f);
        PhotonNetwork.Destroy(gameObject);
    }
}