using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour 
{
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioClip _musicClip;
    [SerializeField] private AudioClip _victoryMusicClip;

    private static AudioManager _instance;

    public static AudioManager Instance
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
        DontDestroyOnLoad(gameObject);
    }

    public void PlayClip(AudioClip clip)
    {
        _sfxAudioSource.PlayOneShot(clip);
    }

    public void PlayMusic()
    {
        _musicAudioSource.clip = _musicClip;
        _musicAudioSource.loop = true;
        _musicAudioSource.Play();
    }

    public void StopMusic()
    {
        _musicAudioSource.Stop();
    }

    public void PlayVictoryMusic()
    {
        _musicAudioSource.clip = _victoryMusicClip;
        _musicAudioSource.loop = false;
        _musicAudioSource.Play();
    }
}
