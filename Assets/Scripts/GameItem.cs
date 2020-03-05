using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class GameItem : MonoBehaviour, IPoolableObject
{
    private Renderer renderer;
    private Rigidbody rigidbody;
    private IObjectPool<GameItem> objectPool;
    private IUpdateProvider provider;
    
    private bool isInited = false;
    private bool canBeDestroyed;

    private Color32 itemColor;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        canBeDestroyed = false;
        rigidbody.isKinematic = true;
    }

    private void OnBecameInvisible()
    {
        if (canBeDestroyed)
        {
            Delete();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        canBeDestroyed = true;
        rigidbody.isKinematic = false;
    }

    public void ChangeColor(Color32 color)
    {
        itemColor = color;
        renderer.material.color = color;
    }

    public void ChangeGravity()
    {
        rigidbody.isKinematic = false;
    }

    public void ReturnedToPool()
    {
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector3.zero;

        renderer.material.color = Color.clear;
        
        canBeDestroyed = false;
    }

    public void Init(IUpdateProvider updateProvider, IObjectPool<GameItem> pool)
    {
        if (isInited)
        {
            return;
        }

        provider = updateProvider;
        objectPool = pool;
        isInited = true;
        
        provider.OnLateUpdate += CheckForDestroy;
    }

    public bool IsDanger()
    {
        return itemColor != Constants.FriendlyColor;
    }

    private void CheckForDestroy()
    {
        if (transform.position.y < -32 || transform.position.x < -10 || transform.position.x > 30)
        {
            if (gameObject.activeSelf)
            {
                Delete();
            }
        }
    }

    public void Delete()
    {
        objectPool.DestroyItem(this);
    }
}