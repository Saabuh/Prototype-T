using System;
using UnityEngine;

namespace Prototype_S
{
    [Serializable]
    public struct ItemSlot
    {
        public ItemInstance itemInstance;
        public int quantity;

        public ItemSlot(ItemInstance itemInstance, int quantity)
        {
            this.itemInstance = itemInstance;
            this.quantity = quantity;
        }
        public static bool operator ==(ItemSlot a, ItemSlot b) { return a.Equals(b); }

        public static bool operator !=(ItemSlot a, ItemSlot b) { return !a.Equals(b); }
    }

}
