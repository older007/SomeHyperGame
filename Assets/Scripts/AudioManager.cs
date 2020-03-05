using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Audio;

    private AudioSource audioSource;
    
    private void Awake()
    {
        Audio = this;
        audioSource = GetComponent<AudioSource>();
        
        DontDestroyOnLoad(this);

        var audioStatus = PlayerPrefs.GetInt(Constants.AudioKey, 1);

        if (audioStatus == 1)
        {
            Enable();
        }
    }

    public void Disable()
    {
        audioSource.volume = 0;
        
        PlayerPrefs.SetInt(Constants.AudioKey, 0);
    }

    public void Enable()
    {
        audioSource.Stop();
        audioSource.Play();
        
        audioSource.volume = 1;
        
        PlayerPrefs.SetInt(Constants.AudioKey, 1);
    }
}
