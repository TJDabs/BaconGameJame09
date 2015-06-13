using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIUsernameInput : MonoBehaviour
{
    [SerializeField] private Button _enterButton;
    [SerializeField] private InputField _inputField;

    private void Awake()
    {
        _enterButton.onClick.AddListener(EnterClicked);
    }

    private void EnterClicked()
    {
        Debug.Log("Setting Players Name to " + _inputField.text);
        if (_inputField.text != string.Empty)
        {
            PhotonNetwork.playerName = _inputField.text;
            Application.LoadLevel("Game");
            PhotonConnectionManager.Instance.JoinRoom();
        }
    }
      
}
