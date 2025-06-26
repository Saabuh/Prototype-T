using System;
using UnityEngine;

namespace Prototype_S
{
    public class ItemContainer : IItemContainer
    {
        private ItemSlot[] itemSlots;

        public ItemContainer(int size) => itemSlots = new ItemSlot[size];

        public Action OnItemsUpdated = delegate { };
        
        public ItemSlot GetSlotByIndex(int index) => itemSlots[index];

        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].itemData != null)
                {
                    if (itemSlots[i].itemData == itemSlot.itemData)
                    {
                        int slotRemainingSpace = itemSlots[i].itemData.MaxStack - itemSlots[i].quantity;

                        if (itemSlot.quantity <= slotRemainingSpace)
                        {
                            itemSlots[i].quantity += itemSlot.quantity;

                            itemSlot.quantity = 0;

                            OnItemsUpdated.Invoke();

                            return itemSlot;
                        }
                        else if (slotRemainingSpace > 0)
                        {
                            itemSlots[i].quantity += slotRemainingSpace;

                            itemSlot.quantity -= slotRemainingSpace;
                        }
                    }
                }
            }

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].itemData == null)
                {
                    if (itemSlot.quantity <= itemSlot.itemData.MaxStack)
                    {
                        itemSlots[i] = itemSlot;

                        itemSlot.quantity = 0;

                        OnItemsUpdated.Invoke();

                        return itemSlot;
                    }
                    else
                    {
                        itemSlots[i] = new ItemSlot(itemSlot.itemData, itemSlot.itemData.MaxStack);

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
            itemSlots[slotIndex] = new ItemSlot();
            OnItemsUpdated.Invoke();
        }

        public void Swap(int indexOne, int indexTwo)
        {
            ItemSlot firstSlot = itemSlots[indexOne];
            ItemSlot secondSlot = itemSlots[indexTwo];

            if (firstSlot == secondSlot) { return; }

            if (secondSlot.itemData != null)
            {
                if (firstSlot.itemData == secondSlot.itemData)
                {
                    int secondSlotRemainingSpace = secondSlot.itemData.MaxStack - secondSlot.quantity;

                    if (firstSlot.quantity <= secondSlotRemainingSpace)
                    {
                        itemSlots[indexTwo].quantity += firstSlot.quantity;

                        itemSlots[indexOne] = new ItemSlot();

                        OnItemsUpdated.Invoke();

                        return;
                    }
                }
            }

            itemSlots[indexOne] = secondSlot;
            itemSlots[indexTwo] = firstSlot;

            OnItemsUpdated.Invoke();
        }

        public bool HasItem(ItemData item)
        {
            foreach (ItemSlot itemSlot in itemSlots)
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

            foreach (ItemSlot itemSlot in itemSlots)
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