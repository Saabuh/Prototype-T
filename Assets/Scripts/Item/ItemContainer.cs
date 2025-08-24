using System;
using Unity.Netcode;
using UnityEngine;

namespace Prototype_S
{
    public class ItemContainer : IItemContainer
    {
        // private ItemSlot[] itemSlots;
        //reference to a networkList existing on a networkBehaviour
        private NetworkList<ItemSlot> itemSlots;

        public ItemContainer(NetworkList<ItemSlot> itemSlots, int size)
        {
            this.itemSlots = itemSlots;
            
            // for (int i = 0; i < size; i++)
            // {
            //     itemSlots.Add(new ItemSlot(-1, 0));
            // }
        } 

        public ItemSlot GetSlotByIndex(int index) => itemSlots[index];

        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            //simple logic for now
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (itemSlots[i].itemID == -1)
                {
                    itemSlots[i] = itemSlot;
                    return itemSlot;
                }
            }

            Log.Info("Inventory was full, cannot add item");
            return itemSlot;
            
            //check for duplicates for stacking

            //check for empty slots for new items
        }

        // public ItemSlot AddItem(ItemSlot itemSlot)
        // {
        //     for (int i = 0; i < itemSlots.Length; i++)
        //     {
        //         if (itemSlots[i].itemInstance != null)
        //         {
        //             Log.Info("Item 1: " + itemSlots[i].itemInstance.itemData);
        //             Log.Info("Item 2: " + itemSlot.itemInstance.itemData);
        //             if (itemSlots[i].itemInstance.itemData == itemSlot.itemInstance.itemData)
        //             {
        //                 int slotRemainingSpace = itemSlots[i].itemInstance.itemData.MaxStack - itemSlots[i].quantity;
        //
        //                 if (itemSlot.quantity <= slotRemainingSpace)
        //                 {
        //                     itemSlots[i].quantity += itemSlot.quantity;
        //
        //                     itemSlot.quantity = 0;
        //
        //                     OnItemsUpdated.Invoke();
        //
        //                     return itemSlot;
        //                 }
        //                 else if (slotRemainingSpace > 0)
        //                 {
        //                     itemSlots[i].quantity += slotRemainingSpace;
        //
        //                     itemSlot.quantity -= slotRemainingSpace;
        //                 }
        //             }
        //         }
        //     }
        //
        //     for (int i = 0; i < itemSlots.Length; i++)
        //     {
        //         if (itemSlots[i].itemInstance == null)
        //         {
        //             if (itemSlot.quantity <= itemSlot.itemInstance.itemData.MaxStack)
        //             {
        //                 itemSlots[i] = itemSlot;
        //                 itemSlot.quantity = 0;
        //                 break;
        //
        //                 // OnItemsUpdated.Invoke();
        //                 // return itemSlot;
        //             }
        //             else
        //             {
        //                 itemSlots[i] = new ItemSlot(itemSlot.itemInstance, itemSlot.itemInstance.itemData.MaxStack);
        //
        //                 itemSlot.quantity -= itemSlot.itemInstance.itemData.MaxStack;
        //             }
        //         }
        //     }
        //
        //     OnItemsUpdated.Invoke();
        //     
        //     //debug
        //     for (int i = 0; i < itemSlots.Length; i++)
        //     {
        //         if (itemSlots[i].itemInstance != null)
        //         {
        //             Log.Info(itemSlots[i].itemInstance.itemData.Name);
        //         }
        //     }
        //
        //     return itemSlot;
        // }

        public void RemoveItem(ItemSlot itemSlot)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int slotIndex)
        {
            itemSlots[slotIndex] = new ItemSlot(-1, 0);
        }

        // public void Swap(int indexOne, int indexTwo)
        // {
        //     throw new NotImplementedException();
        // }

        public bool HasItem(ItemData item)
        {
            throw new NotImplementedException();
        }

        public int GetTotalQuantity(ItemData item)
        {
            throw new NotImplementedException();
        }

        // public void RemoveAt(int slotIndex)
        // {
        //     itemSlots[slotIndex] = new ItemSlot();
        //     OnItemsUpdated.Invoke();
        // }

        public void Swap(int indexOne, int indexTwo)
        {
            //pass by value
            ItemSlot firstSlot = itemSlots[indexOne];
            ItemSlot secondSlot = itemSlots[indexTwo];

            // if (firstSlot == secondSlot)
            // {
            //     return;
            // }

            if (secondSlot.itemID != -1)
            {
                ItemData firstItem = ItemDatabase.Instance.GetItemByID(firstSlot.itemID);
                ItemData secondItem = ItemDatabase.Instance.GetItemByID(secondSlot.itemID);

                Log.Info(firstItem == secondItem);

                if (firstItem == secondItem)
                {
                    int secondSlotRemainingSpace = secondItem.MaxStack - secondSlot.quantity;

                    if (firstSlot.quantity <= secondSlotRemainingSpace)
                    {
                        secondSlot.quantity += firstSlot.quantity;
                        itemSlots[indexTwo] = secondSlot;
                        itemSlots[indexOne] = new ItemSlot(-1, 0);
                        
                        return;
                    }
                }
                
            }

            itemSlots[indexOne] = secondSlot;
            itemSlots[indexTwo] = firstSlot;
            
            // if (secondSlot.itemID != -1)
            // {
            //     if (firstSlot.itemInstance.itemData == secondSlot.itemInstance.itemData)
            //     {
            //         int secondSlotRemainingSpace = secondSlot.itemInstance.itemData.MaxStack - secondSlot.quantity;
            //
            //         if (firstSlot.quantity <= secondSlotRemainingSpace)
            //         {
            //             itemSlots[indexTwo].quantity += firstSlot.quantity;
            //
            //             itemSlots[indexOne] = new ItemSlot();
            //
            //             return;
            //         }
            //     }
            // }
            
            
        }
        
        // public bool HasItem(ItemData item)
        // {
        //     foreach (ItemSlot itemSlot in itemSlots)
        //     {
        //         if (itemSlot.itemInstance.itemData == item)
        //         {
        //             return true;
        //         }
        //     }
        //     return false;
        // }
        //
        // public int GetTotalQuantity(ItemData item)
        // {
        //     int totalQuantity = 0;
        //
        //     foreach (ItemSlot itemSlot in itemSlots)
        //     {
        //         if (itemSlot.itemInstance.itemData == item)
        //         {
        //             totalQuantity += itemSlot.quantity;
        //         }
        //     }
        //     return totalQuantity;
        // }
    }
}