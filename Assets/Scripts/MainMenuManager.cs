using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    
    [Header("Settings")]
    [SerializeField] private Button backButton;
    [SerializeField] private Button disableButton;
    [SerializeField] private Button enableButton;
    [SerializeField] private GameObject settingsView;
    
    private void Awake()
    {
        playButton.onClick.AddListener(PlayClick);
        settingsButton.onClick.AddListener(SettingsClick);
        backButton.onClick.AddListener(BackClick);
        disableButton.onClick.AddListener(DisableSoundClick);
        enableButton.onClick.AddListener(EnableSoundClick);
        
        settingsView.SetActive(false);
    }

    private void PlayClick()
    {
        SceneManager.LoadScene(Constants.GameScene);
    }

    private void SettingsClick()
    {
        settingsView.SetActive(true);
    }

    private void BackClick()
    {
        settingsView.SetActive(false);
    }

    private void EnableSoundClick()
    {
        AudioManager.Audio.Enable();
    }

    private void DisableSoundClick()
    {
        AudioManager.Audio.Disable();
    }
}
