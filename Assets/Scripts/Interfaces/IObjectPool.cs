using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Interfaces
{
    public interface IObjectPool<Obj> where Obj : Object, IPoolableObject
    {
        void CreateItem<T>(T prefab, Transform parent, Vector3 pos, bool localPos, Action<T> callback) where T : Obj;
        void GetItem<T>() where T : Obj;
        void DestroyItem<T>(T item) where T : GameItem;
    }
}