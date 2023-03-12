using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameSceneController : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    public static GameObject LocalPlayerInstance;

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected || !PhotonNetwork.InRoom)
        {
            ReturnToMainMenu();
        }
    }

    private void Start()
    {
        if (LocalPlayerInstance == null)
        {
            var player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity, 0);
            DontDestroyOnLoad(player);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        ReturnToMainMenu();
    }

    private void ReturnToMainMenu()
    {
        Debug.Log("Disconnected");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AdjustPlayerNumbers();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        AdjustPlayerNumbers();
    }

    private void AdjustPlayerNumbers()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Arena");
        }
    }
}