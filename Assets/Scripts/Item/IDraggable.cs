using UnityEngine;
using UnityEngine.EventSystems;

namespace Prototype_S
{
    public interface IDraggable
    {
        
        Transform Transform { get; }

        void OnDrop(PointerEventData eventData);

        void OnPickup();
    }
}