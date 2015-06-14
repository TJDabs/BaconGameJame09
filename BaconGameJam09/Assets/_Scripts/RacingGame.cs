using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RacingGame : Photon.MonoBehaviour 
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _againButton;
    [SerializeField] private Text _goLabel;
    [SerializeField] private Text _timerLabel;

    private bool _runTimer;
    private float _timer;

    public bool GameStarted;

    private static RacingGame _instance;

    public static RacingGame Instance
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
    }

    public void Setup()
    {
        GameStarted = false;
        _timer = 0;
        _runTimer = false;
        _againButton.onClick.AddListener(AgainButtonClicked);
    }

    public void TryShowStartButton()
    {
        if (PhotonNetwork.isMasterClient)
        {
            _startButton.gameObject.SetActive(true);
            _startButton.onClick.AddListener(StartButtonClicked);
        }
        else
        {
            _startButton.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (_runTimer)
        {
            _timer += Time.deltaTime;
            _timerLabel.text = _timer.ToString("#.#");
        }
    }

    private void StartButtonClicked()
    {
        Debug.Log("Clicked Start");
        _startButton.gameObject.SetActive(false);
        photonView.RPC("StartGame", PhotonTargets.All);
    }

    private void AgainButtonClicked()
    {
        Debug.Log("Clicked Again");
        AudioManager.Instance.StopMusic();
        PhotonNetwork.LeaveRoom();
        Application.LoadLevel("Login");
    }

    [RPC]
    private void StartGame()
    {
        Debug.Log("Starting Game!");
        GameStarted = true;
        _runTimer = true;
        StartCoroutine(ShowGoText());
    }

    private IEnumerator ShowGoText()
    {
        _goLabel.enabled = true;
        yield return new WaitForSeconds(1.0f);
        _goLabel.enabled = false;
    }

    public void EndGame(string winner)
    {
        photonView.RPC("EndGameRPC", PhotonTargets.All, winner);
    }

    [RPC]
    private void EndGameRPC(string winner)
    {
        Debug.Log("Winner is " + winner);
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayVictoryMusic();

        _goLabel.text = "Winner is " + winner + "!";
        _goLabel.enabled = true;
        GameStarted = false;
        _runTimer = false;
        _againButton.gameObject.SetActive(true);
    }
}
