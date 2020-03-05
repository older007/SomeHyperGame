using UnityEngine;
using UnityEngine.SceneManagement;

public class AppRoot
{
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        SceneManager.LoadScene(Constants.PreLoadScene);
    }
}