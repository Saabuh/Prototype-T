using UnityEngine;

namespace Prototype_S
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}
