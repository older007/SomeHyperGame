using System;

namespace Interfaces
{
    public interface IUpdateProvider
    {
        event Action OnLateUpdate;
        event Action OnUpdate;
    }
}