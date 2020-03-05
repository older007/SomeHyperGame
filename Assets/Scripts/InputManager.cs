using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private UpdateProvider updateProvider;
    [SerializeField] private GamePlayManager gamePlayManager;
    
    private void Start()
    {
        updateProvider.OnUpdate += OnUpdate;
    }

    private void OnUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            gamePlayManager.OnStart();
        }
    }
}