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
        PhotonNetwork.CreateRoom(null);
    }

    private void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        SpawnManager.Instance.SpawnPlayer();
    }
}
