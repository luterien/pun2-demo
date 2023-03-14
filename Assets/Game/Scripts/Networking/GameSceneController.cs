using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameSceneController : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public GameObject aiPrefab;

    public AbilitySlotManager abilitySlotManager;

    public static GameObject LocalPlayerInstance;

    private PlayerController _playerController;

    private bool _checkForAISpawn;
    private Coroutine _checkForAISpawnTimerCoroutine;

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected || !PhotonNetwork.InRoom)
        {
            ReturnToMainMenu();
        }

        if (PhotonNetwork.IsMasterClient)
        {
            _checkForAISpawn = true;
            _checkForAISpawnTimerCoroutine = StartCoroutine(CheckForAISpawn());
        }
    }

    private void Start()
    {
        if (LocalPlayerInstance == null)
        {
            var player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity, 0);
            _playerController = player.GetComponent<PlayerController>();
            _playerController.OnPlayerKilled += HandlePlayerKilled;

            DontDestroyOnLoad(player);
        }
        else if (_playerController == null)
        {
            _playerController = LocalPlayerInstance.GetComponent<PlayerController>();
            _playerController.OnPlayerKilled += HandlePlayerKilled;
        }

        abilitySlotManager.Initialize(_playerController);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        ReturnToMainMenu();
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (_checkForAISpawn && _checkForAISpawnTimerCoroutine != null)
        {
            _checkForAISpawn = false;
            StopCoroutine(_checkForAISpawnTimerCoroutine);
        }

        OnPlayersUpdated();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        OnPlayersUpdated();
    }

    private void OnPlayersUpdated()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Arena");
        }
    }

    private IEnumerator CheckForAISpawn()
    {
        yield return new WaitForSeconds(5f);

        _checkForAISpawn = false;

        Debug.Log("Spawn enemy AI");
    }

    private void HandlePlayerKilled(PlayerController playerController)
    {
        _playerController.OnPlayerKilled -= HandlePlayerKilled;

        PhotonNetwork.LeaveRoom();
        ReturnToMainMenu();
    }
}