using System;
using Interfaces;
using UnityEngine;

public class UpdateProvider : MonoBehaviour, IUpdateProvider
{
    private void Update()
    {
        OnUpdate?.Invoke();
    }

    private void LateUpdate()
    {
        OnLateUpdate?.Invoke();
    }

    public event Action OnLateUpdate;
    public event Action OnUpdate;
}