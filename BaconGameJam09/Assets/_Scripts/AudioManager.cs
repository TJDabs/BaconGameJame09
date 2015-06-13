using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour 
{
    [SerializeField] private AudioSource _audioSource;

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
    }

    public void PlayClip(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

}
