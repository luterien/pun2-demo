using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviourPunCallbacks
{
    public string gameVersion;
    public byte maxRoomSize = 2;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        PhotonNetwork.LocalPlayer.NickName = "You";
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
    }

    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected " + cause.ToString());
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Arena");
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxRoomSize });
    }

    public void Connect()
    {
        PhotonNetwork.JoinRandomRoom();
    }
}
