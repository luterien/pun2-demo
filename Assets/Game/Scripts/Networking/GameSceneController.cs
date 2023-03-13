using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameSceneController : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
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

            DontDestroyOnLoad(player);
        }
        else if (_playerController == null)
        {
            _playerController = LocalPlayerInstance.GetComponent<PlayerController>();
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

    private IEnumerator CheckForAISpawn()
    {
        yield return new WaitForSeconds(5f);

        _checkForAISpawn = false;

        Debug.Log("Spawn AI");
    }
}