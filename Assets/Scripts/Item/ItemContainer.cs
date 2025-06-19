using System;
using UnityEngine;

namespace Prototype_S
{
    public class ItemContainer : IItemContainer
    {
        private ItemSlot[] _itemSlots;

        public ItemContainer(int size) => _itemSlots = new ItemSlot[size];

        public event Action OnItemsUpdated = delegate { };
        
        public ItemSlot GetSlotByIndex(int index) => _itemSlots[index];

        public void AddItem(ItemSlot newItemSlot)
        {
            for (int i = 0; i < _itemSlots.Length; i++)
            {
                if (_itemSlots[i].itemData == null)
                {
                    _itemSlots[i] = newItemSlot;
                    OnItemsUpdated.Invoke();
                    return;
                }
            }
        }

        public void RemoveItem(ItemSlot itemSlot)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int slotIndex)
        {
            throw new System.NotImplementedException();
        }

        public void Swap(int indexOne, int indexTwo)
        {
            throw new System.NotImplementedException();
        }

        public bool HasItem(ItemData item)
        {
            foreach (ItemSlot itemSlot in _itemSlots)
            {
                if (itemSlot.itemData == item)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetTotalQuantity(ItemData item)
        {
            int totalQuantity = 0;

            foreach (ItemSlot itemSlot in _itemSlots)
            {
                if (itemSlot.itemData == item)
                {
                    totalQuantity += itemSlot.quantity;
                }
            }
            return totalQuantity;
        }
    }
}