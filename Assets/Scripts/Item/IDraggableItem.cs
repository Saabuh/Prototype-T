using UnityEngine;
using UnityEngine.EventSystems;

namespace Prototype_S
{
    public interface IDraggableItem
    {
        
        Transform Transform { get; }
        
        ItemSlotUI ItemSlotUI { get; }

        void OnRelease(bool targetFound);

        void OnPickup();
    }
}