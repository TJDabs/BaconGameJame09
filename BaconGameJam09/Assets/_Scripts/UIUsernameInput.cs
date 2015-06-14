using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIUsernameInput : MonoBehaviour
{
    [SerializeField] private Button _enterButton;
    [SerializeField] private Button _joinRandomButton;
    [SerializeField] private Button _joinNamedButton;
    [SerializeField] private InputField _inputField;
    [SerializeField] private InputField _roomNameInputField;
    [SerializeField] private Text _messageText;

    private void Awake()
    {
        _enterButton.onClick.AddListener(EnterClicked);
        _joinRandomButton.onClick.AddListener(JoinRandomClicked);
        _joinNamedButton.onClick.AddListener(JoinNamedClicked);
        _inputField.text = PhotonNetwork.playerName;
    }

    private void EnterClicked()
    {
        Debug.Log("Setting Players Name to " + _inputField.text);
        if (_inputField.text != string.Empty)
        {
            PhotonNetwork.playerName = _inputField.text;
            _messageText.text = "Saved!";
        }
    }

    private void JoinRandomClicked()
    {
        Application.LoadLevel("Game");
        PhotonConnectionManager.Instance.JoinRoom();
    }

    private void JoinNamedClicked()
    {
        if (_roomNameInputField.text != string.Empty)
        {
            Application.LoadLevel("Game");
            PhotonConnectionManager.Instance.JoinOrCreateRoom(_roomNameInputField.text);
        }
    }
}
