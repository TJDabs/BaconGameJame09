using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhotonConnectionManager : MonoBehaviour 
{
    private static PhotonConnectionManager _instance;

    public static PhotonConnectionManager Instance
    {
        get
        {
            return _instance;
        }
    }

	private void Awake() 
    {
        if (_instance == null)
        {
            _instance = this;
        }

        PhotonNetwork.logLevel = PhotonLogLevel.Informational;
        PhotonNetwork.ConnectUsingSettings(null);
        DontDestroyOnLoad(gameObject);
      //  PhotonNetworkingMessage.OnJoinedRoom
	}

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinOrCreateRoom(string roomName)
    {
        RoomOptions roomOptions = new RoomOptions() { maxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    private void OnJoinedLobby()
    {
        Debug.Log("Connected to Photon");
        Application.LoadLevel("Login");
    }

    private void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Join failed creating");
        RoomOptions roomOptions = new RoomOptions() { maxPlayers = 4 };
        PhotonNetwork.CreateRoom(System.Guid.NewGuid().ToString(), roomOptions, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        SpawnManager.Instance.SpawnPlayer();
        RacingGame.Instance.Setup();
    }
}
