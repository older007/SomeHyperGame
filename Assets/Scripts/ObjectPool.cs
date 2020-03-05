using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool : MonoBehaviour, IObjectPool<GameItem>
{
    private IUpdateProvider updateProvider;

    private readonly int objectLimit = Constants.LevelSize;
    private readonly Queue<Action> pool = new Queue<Action>();
    private readonly Queue<GameItem> itemPoll = new Queue<GameItem>();
    private int objectsOnScene = 0;
    private int objectsInPool = 0;
    
    public void Init(IUpdateProvider provider)
    {
        updateProvider = provider;
        
        updateProvider.OnUpdate += OnUpdate;
    }

    private void OnUpdate()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        if (pool.Count == 0 || objectsOnScene == objectLimit)
        {
            return;
        }

        pool.Dequeue()?.Invoke();
    }

    public void CreateItem<T>(T prefab, Transform parent, Vector3 pos, bool localPos, Action<T> callback) where T : GameItem
    {
        pool.Enqueue(() =>
        {
            var local = localPos;

            if (itemPoll.Count != 0)
            {
                var item = itemPoll.Dequeue() as T;

                item.gameObject.SetActive(true);
                item.transform.SetParent(parent);
            
                if (local)
                {
                    item.transform.localPosition = pos;
                }
                else
                {
                    item.transform.position = pos;
                }

                objectsOnScene += 1;
                objectsInPool -= 1;

                callback?.Invoke(item);

                return;
            }
            
            var obj = Instantiate(prefab, parent);

            if (local)
            {
                obj.transform.localPosition = pos;
            }
            else
            {
                obj.transform.position = pos;
            }

            callback?.Invoke(obj);

            objectsOnScene += 1;
        });
    }

    public void DestroyItem<T>(T item) where T : GameItem
    {
        item.ReturnedToPool();
        item.transform.SetParent(transform);
        item.gameObject.SetActive(false);
        item.transform.position = Vector3.zero;
        
        itemPoll.Enqueue(item);
        
        objectsInPool += 1;
        objectsOnScene -= 1;
    }

    public void GetItem<T>() where T : GameItem
    {
        //todo in future
    }
}