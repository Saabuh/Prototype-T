# Worklog

Just a note:
"solve with your fingers"
really like the "f*ck it up all the way and then do it for real" approach to creating stuff
90% of the time it's faster to f*ck up and know why than it is to think about all the ways you might f*ck up

100% agree: I code like I write my music. I dont sit and make notes on paper, I sit at the synth and tinker, make some sounds, 
twiddle knobs, play some chords, feel the music, keep it or trash it, get feedback and iterate. totally get your jam. im with you.

## Bugs
- [ ] dropping items between inventory slots causes multiple raycasts hits(?)
- [ ] adding inventory items doesnt handle full inventories properly (pass by reference as a solution? method overload for 2 versionf of AddItem if we care/dont care about remainder)
- [ ] considering moving the inputReader script onto a seperate InputReader object. reasoning is that we are attaching the UIManager to an instance of the inputReader component on the player. If the player dies (gameObject deleted),
so does the input reader temporarily.
- [x] [dragging an item and then closing the ui bugs out, item freezes on current mouse position on next inventory toggle](https://github.com/Saabuh/Prototype-T/issues/1)
- [x] [Visual duplicate item bug with inventory toggling](https://github.com/Saabuh/Prototype-T/issues/2)

# TODO List

## 27-06-2025
- [ ] improve DragStateManager's Update() logic for holding detection, setting isHolding to true in update is prone to bugs
- [ ] change dragging out of inventory into click and click again to drop
- [ ] add item tooltip ui

## 26-06-2025
- [x] [Visual duplicate item bug with inventory toggling](https://github.com/Saabuh/Prototype-T/issues/2)
- [x] [dragging an item and then closing the ui bugs out](https://github.com/Saabuh/Prototype-T/issues/1)

## 25-06-2025
- [x] add pickup to world entities
- [x] add toggling inventory ui
- [x] remove singleton from PlayerController
    - this was instead modified to handle multiplayer players. Will add a PlayerManager later on. 

## 24-06-2025
- [x] add basic drop system
- [x] add item template prefab

## 23-06-2025
- [x] finish itemcontainer logic

## 23-06-2025
- [x] adding inventory visuals/ui
- [x] finish itemcontainer logic

## 21-06-2025
- [ ] try out these games:
    - [ ] Necesse
    - [ ] Tinkerlands
    - [ ] Fields of Mistra
    - [ ] Forsaken Isle
- [x] adding event system
- [x] adding inventory visuals/ui

## 20-06-2025
- [x] adding inventory visuals/ui
- [x] retry inventory system implementation

## 19-06-2025 (Reviving the project)
- [x] retry inventory system implementation

## 31-01-2025
- [x] start inventory system
- [ ] continue with attack strategies
    - any entity (player, enemy, npc, environment) can have an attack strategy
    - should be able to combine any weaponData with any attackStrategy and have it not break(?)

## 29-01-2025
- [x] add tree colliders

## 21-01-2025
- [x] generating sample trees on terrain
- [x] fix tree sorting layers

Of course. This is a classic and essential system for any Terraria-like game. Let's design a robust, scalable, and reusable item spawning system following best practices and a component-based approach.

The core principle is **separation of concerns**. An "item" as a piece of data should be separate from an "item" as a physical object in the world. You already have the data part, so we'll focus on creating the physical representation.

We'll create a single, universal prefab for any item dropped in the world, and then initialize it with the specific item's data when it spawns.

---

### Step 1: The Item Data (The Foundation)

First, let's ensure your item data script is set up for success. The best practice for this is to use a `ScriptableObject`. If you're not already, I highly recommend it. It allows you to define item data as assets in your project, completely separate from any scene logic.

Here’s a basic `ItemData` script. You can expand this with stats, types, etc.

**`ItemData.cs`**
```csharp
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Game/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public string description;

    [Header("Visuals")]
    public Sprite icon; // The sprite for the item when it's on the ground

    // You can add any other data here: stats, max stack, type, etc.
}
```

To use this, you would right-click in your Project window -> Create -> Game -> Item Data to create new item assets like "Iron Ore," "Wood," etc.

---

### Step 2: The `ItemInWorld` Prefab (The Physical Object)

This is the core of the system. We will create one generic prefab that can represent any item.

1.  **Create the Base GameObject:**
    *   In your scene, create an empty GameObject. Name it `ItemInWorld`.
    *   Add a **`SpriteRenderer`** component. We will set the sprite in code, so you can leave it blank for now.
    *   Add a **`Rigidbody2D`** component. This will handle gravity and physics. You can adjust `Gravity Scale`, `Mass`, and `Linear Drag` to get the right "feel."
    *   Add a **`BoxCollider2D`** (or `CircleCollider2D`). This is for physical collision with the ground. Make sure `Is Trigger` is **unchecked**.

2.  **Create the Pickup Trigger:**
    *   On the same `ItemInWorld` GameObject, add a **second collider**, a **`CircleCollider2D`**.
    *   Set its `Radius` to be larger than the physics collider. This defines the player's pickup range.
    *   **Crucially, check the `Is Trigger` box on this collider.** This collider will not cause physical collisions; it will only detect when the player enters its range.

    > **Best Practice:** Using two colliders separates physical presence from interaction range. It allows you to have a small item that is easy to pick up from a distance.

Your `ItemInWorld` Inspector should now look something like this:


---

### Step 3: Composing the Behavior (The Scripts)

Now, we'll add small, single-responsibility scripts to our `ItemInWorld` GameObject. This is composition in action.

#### A. The Data Container

This script's only job is to hold a reference to the item's data and quantity.

**`ItemContainer.cs`**
```csharp
using UnityEngine;

// This component holds the data for the item stack in the world.
public class ItemContainer : MonoBehaviour
{
    public ItemData ContainedItem { get; private set; }
    public int Quantity { get; private set; }

    // This method is called by the ItemSpawner to set up the item.
    public void Initialize(ItemData itemData, int quantity)
    {
        this.ContainedItem = itemData;
        this.Quantity = quantity;

        // Notify other components on this GameObject that we've been initialized.
        // This is a clean way for components to react without being tightly coupled.
        gameObject.SendMessage("OnItemInitialized", SendMessageOptions.DontRequireReceiver);
    }
}
```
*Add this script to your `ItemInWorld` GameObject.*

#### B. The Visuals

This script listens for the initialization and updates the sprite.

**`ItemVisuals.cs`**
```csharp
using UnityEngine;

// This component updates the sprite based on the data in the ItemContainer.
[RequireComponent(typeof(SpriteRenderer), typeof(ItemContainer))]
public class ItemVisuals : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private ItemContainer itemContainer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        itemContainer = GetComponent<ItemContainer>();
    }

    // This method is called by the ItemContainer via SendMessage.
    private void OnItemInitialized()
    {
        if (itemContainer.ContainedItem != null)
        {
            spriteRenderer.sprite = itemContainer.ContainedItem.icon;
        }
    }
}
```
*Add this script to your `ItemInWorld` GameObject.*

#### C. The Pickup Logic

This script handles the player interaction using the trigger collider.

**`ItemPickup.cs`**
```csharp
using UnityEngine;

// Handles the logic for a player picking up the item.
[RequireComponent(typeof(ItemContainer))]
public class ItemPickup : MonoBehaviour
{
    private ItemContainer itemContainer;

    private void Awake()
    {
        itemContainer = GetComponent<ItemContainer>();
    }

    // This is called by Unity when another collider with a Rigidbody enters our trigger.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // We only care if the object that entered has a PlayerInventory component.
        // Use tags ("Player") if you prefer, but GetComponent is more robust.
        if (other.TryGetComponent(out PlayerInventory playerInventory))
        {
            // Attempt to add the item to the player's inventory.
            bool wasFullyPickedUp = playerInventory.AddItem(itemContainer.ContainedItem, itemContainer.Quantity);

            // If the inventory took the whole stack, destroy this world object.
            if (wasFullyPickedUp)
            {
                Destroy(gameObject);
            }
            else
            {
                // Optional: If the inventory is full and couldn't take the item,
                // you could add logic here, e.g., play a "cannot pick up" sound.
                // For now, it just stays on the ground.
            }
        }
    }
}
```
*Add this script to your `ItemInWorld` GameObject.* You will need to create a `PlayerInventory.cs` script on your player object with an `AddItem` method.

---

### Step 4: The Spawner (Putting It All Together)

Finally, we need a clean, global way to spawn these items. A static "service" class is perfect for this. It can be called from anywhere (enemy death scripts, player dropping items, breaking chests) without needing a reference to a manager object.

**`ItemSpawner.cs`**
```csharp
using UnityEngine;

public static class ItemSpawner
{
    // The prefab must be linked. A common way is to have a simple "GameManager"
    // or "ResourceManager" script hold the reference and assign it here on start.
    private static GameObject _itemInWorldPrefab;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        // Load the prefab from the "Resources" folder.
        // Make sure your ItemInWorld prefab is in a folder named "Resources".
        _itemInWorldPrefab = Resources.Load<GameObject>("ItemInWorld");
        if (_itemInWorldPrefab == null)
        {
            Debug.LogError("ItemInWorld prefab not found in Resources folder!");
        }
    }

    public static void SpawnItem(Vector3 position, ItemData itemData, int quantity = 1)
    {
        if (_itemInWorldPrefab == null || itemData == null || quantity <= 0)
        {
            Debug.LogWarning("Could not spawn item. Prefab, ItemData, or quantity is invalid.");
            return;
        }

        // Instantiate the generic item prefab at the desired position.
        GameObject itemObject = Object.Instantiate(_itemInWorldPrefab, position, Quaternion.identity);

        // Get its container and initialize it with the specific item data and quantity.
        ItemContainer container = itemObject.GetComponent<ItemContainer>();
        container.Initialize(itemData, quantity);

        // Optional: Add a small random force to make drops feel more dynamic.
        Rigidbody2D rb = itemObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 randomForce = new Vector2(Random.Range(-2f, 2f), Random.Range(2f, 4f));
            rb.AddForce(randomForce, ForceMode2D.Impulse);
        }
    }
}
```

**Final Setup for Spawner:**
1.  Drag your fully configured `ItemInWorld` GameObject from the Hierarchy into your Project window to create a prefab.
2.  Create a folder named `Resources` inside your `Assets` folder.
3.  Move the `ItemInWorld` prefab into this `Resources` folder.

### How to Use It

Now, from any script in your game, you can easily spawn an item:

```csharp
public class Enemy : MonoBehaviour
{
    public ItemData lootToDrop; // Assign your "Iron Ore" asset here in the Inspector.

    public void OnDeath()
    {
        // Spawns 1 iron ore at the enemy's position.
        ItemSpawner.SpawnItem(transform.position, lootToDrop);

        // Spawns 5 wood at the enemy's position.
        // ItemSpawner.SpawnItem(transform.position, woodItemData, 5);

        Destroy(gameObject);
    }
}
```
