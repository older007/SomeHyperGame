using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class LevelGenerator
{
    private readonly Transform parent;
    private readonly GameItem itemPrefab;
    private readonly LevelObstacle[] obstacleLevels;
    private readonly LevelObstacle[] obstacleMasks;
    private readonly IObjectPool<GameItem> objectPool;
    private readonly IUpdateProvider updateProvider;
    
    private Vector2 lastPosition;

    public LevelGenerator(Transform levelParent, LevelObstacle[] levelObstacles, LevelObstacle[] levelMasks, 
        GameItem prefab, IObjectPool<GameItem> pool, IUpdateProvider provider)
    {
        parent = levelParent;
        itemPrefab = prefab;
        obstacleLevels = levelObstacles;
        obstacleMasks = levelMasks;
        objectPool = pool;
        updateProvider = provider;
    }

    public void CreateLevel(int wavesCount)
    {
        var itemsForLevel = obstacleLevels;
        
        foreach (var levelItem in itemsForLevel)
        {
            var levelParent = new GameObject(levelItem.name);
            var mask = obstacleMasks[Random.Range(0, obstacleMasks.Length)];

            levelParent.transform.localPosition = lastPosition;
            levelParent.transform.SetParent(parent);
            
            CreateLevel(levelItem, mask, levelParent.transform);
            
            lastPosition = new Vector2(0, lastPosition.y + Constants.WaveDelta);
        }
    }

    private void CreateLevel(LevelObstacle obstacle, LevelObstacle mask , Transform itemsParent)
    {
        for (var h = 0; h < Constants.WaveSize; h++)
        {
            for (var w = Constants.WaveSize - 1; w >= 0; w--)
            {
                var coloredItem = obstacle.GetColor(w, h);
                var pos = new Vector3(w,h);

                if (CheckColor(coloredItem))
                {
                    if (mask.GetColor(w, h) != Color.clear)
                    {
                        CreateItem(Constants.FriendlyColor , itemsParent, pos); 
                    }
                    else
                    {
                        CreateItem(coloredItem, itemsParent, pos); 
                    }
                }
            }    
        }
        
        void CreateItem(Color color, Transform itemParent, Vector3 pos)
        {
            objectPool.CreateItem(itemPrefab, itemParent, pos, true,(obj) =>
            {
                obj.Init(updateProvider, objectPool);
                
                var newColor = color;
                
                obj.ChangeColor(newColor);
            });
        }
    }

    private void OnObjectCreated(GameItem obj)
    {
        
    }

    private bool CheckColor(Color32 color32)
    {
        return color32.a != 0;
    }
    
/*    private void ExecuteOnLastFrame()
    {
        if (lateUpdateQueue.Count == 0)
        {
            return;
        }

        lateUpdateQueue.Dequeue()?.Invoke();
    }*/

    private int[] GenerateIndexes(int limit)
    {
        var indexes = new int[limit];

        for (var i = 0; i < limit; i++)
        {
            var item = GenerateInt(limit);
            
            while (!indexes.Contains(item))
            {
                item = GenerateInt(limit);
            }

            indexes[i] = item;
        }

        return indexes;
    }

    private int GenerateInt(int limit)
    {
        return Random.Range(0, limit);
    }
}
