using System;
using Unity.Netcode;
using UnityEngine;

namespace Prototype_S
{
    [Serializable]
    public struct ItemSlot : INetworkSerializable, System.IEquatable<ItemSlot>
    {
        public int itemID;
        public int quantity;

        public ItemSlot(int itemID, int quantity)
        {
            this.itemID = itemID;
            this.quantity = quantity;
        }
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref itemID);
            serializer.SerializeValue(ref quantity);
        }

        public bool Equals(ItemSlot other)
        {
            return itemID == other.itemID && quantity == other.quantity;
        }
        
        public static bool operator ==(ItemSlot a, ItemSlot b) { return a.Equals(b); }
        
        public static bool operator !=(ItemSlot a, ItemSlot b) { return !a.Equals(b); }
    }

}
