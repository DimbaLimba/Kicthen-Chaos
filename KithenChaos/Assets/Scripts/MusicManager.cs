using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private float _volume = 0.3f;
    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();

        _volume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
        _audioSource.volume = _volume;
    }

    public void ChangedVolume()
    {
        _volume += 0.1f;
        if (_volume > 1f)
        {
            _volume = 0f;            
        }
        _audioSource.volume = _volume;

        PlayerPrefs.SetFloat("MusicVolume", _volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return _volume;
    }
}
