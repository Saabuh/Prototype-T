using System;
using UnityEngine;

namespace Prototype_S
{
    [Serializable]
    public struct ItemSlot
    {
        public ItemData itemData;
        public int quantity;

        public ItemSlot(ItemData itemData, int quantity)
        {
            this.itemData = itemData;
            this.quantity = quantity;
        }
    }

}
