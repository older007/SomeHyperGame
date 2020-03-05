using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMover : MonoBehaviour
{
    [SerializeField] private GamePlayManager gamePlayManager;
    
    private void Update()
    {
        if (gamePlayManager.IsMove())
        {
            Move();
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.down * 0.05f);
    }
}
