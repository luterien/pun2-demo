using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPunObservable
{
    public PlayerModel playerModelComponent;

    private PhotonView photonView;
    private IInputController inputController;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            GameSceneController.LocalPlayerInstance = this.gameObject;

            playerModelComponent.Initialize();
        }
    }

    private void Start()
    {
        inputController = new PlayerInputController();
    }

    private void Update()
    {
        inputController.Tick(Time.deltaTime);

        GameSceneController.LocalPlayerInstance.transform.position += inputController.HorizontalAxis * Time.deltaTime * Vector3.right;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
