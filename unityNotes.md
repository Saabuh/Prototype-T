# Unity Notes

## Raycasting and Canvas Interaction:

Why It Breaks on Your InventoryPanel
This is the crucial part that explains your specific situation.
Normally, you only have one GraphicRaycaster on your root Canvas object. This single "Eye" is responsible for seeing everything on that canvas.
However, if you add a Canvas component to a child object (like your InventoryPanel), you create an independent rendering and raycasting "island".
When you put a Canvas component on your InventoryPanel:
You are telling Unity: "I want this panel and all of its children to have their own, separate sorting and rendering rules." This is often done to ensure the inventory always appears on top of other UI elements.
The consequence is that the main GraphicRaycaster on the root Canvas stops looking at the children of this new Canvas. It treats the InventoryPanel as a single, opaque object.
Your InventoryPanel has now become its own self-contained UI world. It needs its own "Eye" to see what's going on inside it.
This is why you must add a GraphicRaycaster component directly onto the InventoryPanel GameObject in this scenario. This new raycaster will be responsible for handling all pointer events for the inventory slots and items contained within it.

TLDR: if you want pointer events on a canvas, 1 canvas = 1 graphic raycaster.

GetComponent<T>() actually means **"get a component that can be treated as type T"**, this includes implemented interfaces, inherited classes (child, parent).

Viewpoint B: The Facade Pattern (Recommended Best Practice)
This is the most common and robust solution. It acknowledges that having one "main" script as a central hub or "facade" for the player makes everything cleaner. The PlayerController is the most logical candidate for this role.
Logic: The PlayerController script evolves slightly. Its primary job is still movement, but it also acts as a Facade: a simple, unified interface to the more complex systems on the player object (like health, inventory, stats, etc.).


public event Action<Vector2> OnFire = delegate { };
this line basically says:
create a delegate that holds methods that have a single Vector2 parameter.
delegate { }; just means to add a empty, void method to the delegate to prevent null check errors.

the inventory still has reference to the item in the itemslot, because when i drag the icon, i am just dragging an ItemIcon with that still references the same item slot
