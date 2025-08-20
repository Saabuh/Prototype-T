# Netcode and networking notes

General rule of thumb:
if a gameObject exists on the network, it has a complete authoritative copy of the gameObject on its own invisible copy of the game i cant see. It is headless and is the 
**Single Source of Truth**. Thus, it will try to run all logic on NetworkBehaviours, since the gameObject exists on the network, so you need guardrails.

rough process of networkObjects:
1. NetworkObject spawns on the server's instance of the game
2. NetworkObject synchronizes across clients with a copy, assigns the owner
3. Whenever an instance of a NetworkObject's method is ran (onNetworkSpawn), it runs for the server and all the clients' copies.
4. whenever a serverRPC method is ran, it will run on the server's instance of the game instead of on the client.

rough process of networkObjects and NetworkLists:
1. same
2. same
3. same
4. we initialize a networkList for each networkObject on only the server's instance of the game, because we want shared data to be server-authoritative and not modified by the client's method call.
5. networkList is populated, change is detected, change is synchronized across all clients


# Project Notes

Entity scripts represent a single entity existing in the world space

You've correctly identified a key flaw in the logic. As it's currently written, the `AddItem` method doesn't have a clear way to signal "I failed because the inventory is full." It always returns the `itemSlot`, which might still have quantity left in it, but the caller doesn't know *why*.

Your code has the right ideas—it correctly handles stacking and filling empty slots—but it needs a more robust return value and a slight restructuring to properly handle the "inventory full" case.

Let's refactor this into a much more reliable and standard inventory method.

### The Problem with the Current Logic

1.  **Ambiguous Return Value:** Returning the modified `itemSlot` is a common pattern, but it's ambiguous. If the returned `quantity` is greater than 0, was it because all stacks are full, all slots are full, or both?
2.  **No Early Exit for Full Inventory:** The method plows through both loops even if it can't place the whole item. The caller has no definitive way to know if the pickup should "fail" and leave the item on the ground.
3.  **Multiple `OnItemsUpdated.Invoke()` Calls:** You should only invoke the update event once, at the very end, after all changes have been made.

---

### The Professional Solution: A Clear Success/Failure Return

The best practice for a method like this is to change its return type to `bool` to signal clear success or failure.

*   `public bool AddItem(ItemSlot itemSlot)`
*   It returns `true` if the **entire** quantity of the `itemSlot` was successfully added to the inventory.
*   It returns `false` if **any** quantity remains (because the inventory is full).

This gives the calling code (your `ItemEntity`) a simple, unambiguous signal to know whether it should destroy itself or not.

### Refactored `AddItem` Method

Here is the revised code. It's structured to be more efficient and easier to read.

```csharp
// In your ItemContainer class

// The method now returns a bool to indicate if the ENTIRE stack was added.
public bool AddItem(ItemSlot itemSlotToAdd)
{
    // --- STAGE 1: Try to stack with existing items ---
    // First, loop through all slots to see if we can stack the item.
    for (int i = 0; i < itemSlots.Length; i++)
    {
        // Check if the slot is not empty and contains the same item type.
        if (itemSlots[i].itemData != null && itemSlots[i].itemData == itemSlotToAdd.itemData)
        {
            int spaceAvailable = itemSlots[i].itemData.MaxStack - itemSlots[i].quantity;

            // If there's space in this stack...
            if (spaceAvailable > 0)
            {
                // How much can we actually add to this stack?
                int amountToAdd = Mathf.Min(itemSlotToAdd.quantity, spaceAvailable);

                // Add to the existing stack and subtract from the stack we're trying to add.
                itemSlots[i].quantity += amountToAdd;
                itemSlotToAdd.quantity -= amountToAdd;
            }
        }
        
        // If we've successfully placed the entire stack, we're done!
        if (itemSlotToAdd.quantity == 0)
        {
            OnItemsUpdated.Invoke();
            return true; // SUCCESS: The entire item was added.
        }
    }

    // --- STAGE 2: Try to fill empty slots ---
    // If we still have items left over, find the first empty slot.
    for (int i = 0; i < itemSlots.Length; i++)
    {
        if (itemSlots[i].itemData == null)
        {
            // How much can we place in this new slot?
            int amountToAdd = Mathf.Min(itemSlotToAdd.quantity, itemSlotToAdd.itemData.MaxStack);

            // Create a new slot with the item data and the determined quantity.
            itemSlots[i] = new ItemSlot(itemSlotToAdd.itemData, amountToAdd);
            itemSlotToAdd.quantity -= amountToAdd;
        }

        // If we've successfully placed the entire stack, we're done!
        if (itemSlotToAdd.quantity == 0)
        {
            OnItemsUpdated.Invoke();
            return true; // SUCCESS: The entire item was added.
        }
    }

    // --- STAGE 3: Inventory is Full ---
    // If we've gone through both loops and there's still quantity left,
    // it means the inventory is full.
    OnItemsUpdated.Invoke(); // Still invoke, because we might have partially filled a stack.
    return false; // FAILURE: The entire item could not be added.
}
```

### How the `ItemEntity` Uses This

Now your `ItemEntity`'s `Pickup` method becomes incredibly clean and logical:

```csharp
// In ItemEntity.cs
public void Pickup(PlayerController player)
{
    if (!canBePickedUp) return;

    Inventory playerInventory = player.Inventory;
    if (playerInventory != null)
    {
        ItemSlot slotToPickUp = new ItemSlot(this.Item, this.Quantity);
        
        // The method call is now a simple, clear condition.
        if (playerInventory.ItemContainer.AddItem(slotToPickUp))
        {
            // If AddItem returns true, the pickup was a success. Destroy the world object.
            Destroy(this.gameObject);
        }
        else
        {
            // If AddItem returns false, the inventory is full. Do not destroy the world object.
            Log.Info("Inventory is full!");
            // You might want to update the world item's quantity if it was partially picked up.
            // For simplicity, we can just leave it as is for now.
        }
    }
}
```

This revised structure is more robust, easier to read, and clearly communicates success or failure, which is essential for good game logic.
