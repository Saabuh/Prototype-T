using UnityEngine;

namespace Prototype_S
{
    public abstract class ItemAction : ScriptableObject
    {
        public abstract void StartUse(GameObject user, ItemInstance itemInstance, Vector3 mousePosition);
        public abstract void OnUse(GameObject user, ItemInstance itemInstance, Vector3 mousePosition);
        public abstract void EndUse(GameObject user, ItemInstance itemInstance, Vector3 mousePosition);
    }
}