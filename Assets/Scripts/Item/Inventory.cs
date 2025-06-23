using UnityEngine;

namespace Prototype_S
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
    public class Inventory : ScriptableObject
    {

        [SerializeField] private VoidEvent onInventoryItemsUpdated = null;

        [SerializeField] private ItemSlot testItemSlot;
        public ItemContainer ItemContainer { get; } = new ItemContainer(40);

        [ContextMenu("Test adding ItemSlot")]
        public void TestAddItemSlot()
        {
            ItemContainer.AddItem(testItemSlot);
        }

        public void OnEnable()
        {
            ItemContainer.OnItemsUpdated += onInventoryItemsUpdated.Raise;
        }

        public void OnDisable()
        {
            
            ItemContainer.OnItemsUpdated -= onInventoryItemsUpdated.Raise;
        }
    }
}