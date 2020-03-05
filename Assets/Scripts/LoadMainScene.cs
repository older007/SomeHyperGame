using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene(Constants.MainMenuScene);
    }
}
