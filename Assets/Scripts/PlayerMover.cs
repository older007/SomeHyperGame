using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private UpdateProvider updateProvider;
    [SerializeField] private GamePlayManager gamePlayManager;

    private float counter = 0;
    
    private void Start()
    {
        updateProvider.OnUpdate += OnUpdate;
    }

    private void OnUpdate()
    {
        if (gamePlayManager.IsDead)
        {
            return;
        }

        counter += Time.deltaTime;
        
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var xPos = pos.x;

        transform.position = Vector3.Lerp(transform.position, new Vector3(xPos, transform.position.y, 0), counter);
    }

    private void OnCollisionEnter(Collision other)
    {
        var item = other.gameObject.GetComponent<GameItem>();

        if (item.IsDanger())
        {
            gamePlayManager.IsDead = true;
            
            gamePlayManager.OnDead();
        }
    }
}
