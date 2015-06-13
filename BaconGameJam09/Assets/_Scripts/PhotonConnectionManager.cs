using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhotonConnectionManager : MonoBehaviour 
{
    [SerializeField] private Text _playerCount;
    [SerializeField] private GameObject _playerPrefab;

	void Awake() 
    {
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.ConnectUsingSettings(null);

      //  PhotonNetworkingMessage.OnJoinedRoom
	}

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }


    private void Update()
    {
        _playerCount.text = PhotonNetwork.countOfPlayers.ToString();
    }

    private void OnJoinedLobby()
    {
        Debug.Log("Connected to Photon");
        PhotonNetwork.JoinRandomRoom();
    }

    private void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Join failed creating");
        PhotonNetwork.CreateRoom(null);
    }

    private void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);
    }
}
