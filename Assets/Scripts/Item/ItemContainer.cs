using System;
using UnityEngine;

namespace Prototype_S
{
    public class ItemContainer : IItemContainer
    {
        private ItemSlot[] _itemSlots;

        public ItemContainer(int size) => _itemSlots = new ItemSlot[size];

        public Action OnItemsUpdated = delegate { };
        
        public ItemSlot GetSlotByIndex(int index) => _itemSlots[index];

        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            for (int i = 0; i < _itemSlots.Length; i++)
            {
                if (_itemSlots[i].itemData != null)
                {
                    if (_itemSlots[i].itemData == itemSlot.itemData)
                    {
                        int slotRemainingSpace = _itemSlots[i].itemData.MaxStack - _itemSlots[i].quantity;

                        if (itemSlot.quantity <= slotRemainingSpace)
                        {
                            _itemSlots[i].quantity += itemSlot.quantity;

                            itemSlot.quantity = 0;

                            OnItemsUpdated.Invoke();

                            return itemSlot;
                        }
                        else if (slotRemainingSpace > 0)
                        {
                            _itemSlots[i].quantity += slotRemainingSpace;

                            itemSlot.quantity -= slotRemainingSpace;
                        }
                    }
                }
            }

            for (int i = 0; i < _itemSlots.Length; i++)
            {
                if (_itemSlots[i].itemData == null)
                {
                    if (itemSlot.quantity <= itemSlot.itemData.MaxStack)
                    {
                        _itemSlots[i] = itemSlot;

                        itemSlot.quantity = 0;

                        OnItemsUpdated.Invoke();

                        return itemSlot;
                    }
                    else
                    {
                        _itemSlots[i] = new ItemSlot(itemSlot.itemData, itemSlot.itemData.MaxStack);

                        itemSlot.quantity -= itemSlot.itemData.MaxStack;
                    }
                }
            }

            OnItemsUpdated.Invoke();

            return itemSlot;
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