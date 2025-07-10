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

        [ContextMenu("Test adding ItemSlot")]
        public void TestAddItemSlot()
        {
            ItemContainer.AddItem(testItemSlot);
        }

        public void Initialize()
        {
            if (ItemContainer == null)
            {
                ItemContainer = new ItemContainer(containerSize);

                if (onItemContainerUpdated != null)
                {
                    ItemContainer.OnItemsUpdated += onItemContainerUpdated.Raise;
                }
            }
        }

        public void OnDestroyCleanup()
        {
            if (ItemContainer != null && onItemContainerUpdated != null)
            {
                ItemContainer.OnItemsUpdated -= onItemContainerUpdated.Raise;
            }
        }

        // public void OnEnable()
        // {
        //     ItemContainer.OnItemsUpdated += onItemContainerUpdated.Raise;
        // }
        //
        // public void OnDisable()
        // {
        //     ItemContainer.OnItemsUpdated -= onItemContainerUpdated.Raise;
        // }
    }
}