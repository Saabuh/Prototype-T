using UnityEngine;

namespace Prototype_S
{
    public abstract class ItemAction : ScriptableObject
    {
        public abstract void StartUse(GameObject user, ItemData itemData, Vector2 mousePosition);
        public abstract void OnUse(GameObject user, ItemData itemData, Vector2 mousePosition);
        public abstract void EndUse(GameObject user, ItemData itemData, Vector2 mousePosition);
    }
}