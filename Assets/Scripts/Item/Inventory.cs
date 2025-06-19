using UnityEngine;

namespace Prototype_S
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
    public class Inventory : ScriptableObject
    {

        [SerializeField] private ItemSlot testItemSlot = new ItemSlot();
        public ItemContainer ItemContainer { get; } = new ItemContainer(40);

        [ContextMenu("Test adding ItemSlot")]
        public void TestAddItemSlot()
        {
            ItemContainer.AddItem(testItemSlot);
        }

    }
}