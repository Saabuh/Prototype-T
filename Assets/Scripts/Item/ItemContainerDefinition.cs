using UnityEngine;

namespace Prototype_S
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
    public class ItemContainerDefinition : ScriptableObject
    {
        
        //fields
        [SerializeField] private int containerSize = 40;

        //events
        [SerializeField] private VoidEvent onItemContainerUpdated = null;

        //misc
        [SerializeField] private ItemSlot testItemSlot;
        
        //getter properties
        public ItemContainer ItemContainer { get; private set; }
        public int ContainerSize => containerSize;

        // [ContextMenu("Test adding ItemSlot")]
        // public void TestAddItemSlot()
        // {
        //
        //     ItemInstance newItemInstance = new ItemInstance(testItemSlot.itemInstance.itemData);
        //     ItemSlot slotToAdd = new ItemSlot(newItemInstance, testItemSlot.quantity);
        //     
        //     ItemContainer.AddItem(slotToAdd);
        // }
        //
        // public void Initialize()
        // {
        //     if (ItemContainer == null)
        //     {
        //         ItemContainer = new ItemContainer(containerSize);
        //
        //         if (onItemContainerUpdated != null)
        //         {
        //             ItemContainer.OnItemsUpdated += onItemContainerUpdated.Raise;
        //         }
        //     }
        // }
        //
        // public void OnDestroyCleanup()
        // {
        //     if (ItemContainer != null && onItemContainerUpdated != null)
        //     {
        //         ItemContainer.OnItemsUpdated -= onItemContainerUpdated.Raise;
        //     }
        // }

    }
}