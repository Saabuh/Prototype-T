using System;
using UnityEngine;

namespace Prototype_S
{
    
    /// <summary>
    /// Responsible for PlayerInventory Behaviour/Logic
    /// </summary>
    public class PlayerInventory : MonoBehaviour
    {

        //fields
        [Tooltip("Template used to create new inventory")]
        [SerializeField] private ItemContainerDefinition containerTemplate;
        public ItemContainerDefinition Inventory { get; private set; }
        public int selectedSlotIndex = 0;
        
        //events
        [SerializeField] private IntegerEvent onSelectedSlotChanged;
        
        //used purely for debugging/ spawning items
        [Header("reference to instantiated Inventory")] [SerializeField]
        private ItemContainerDefinition inventoryRunTimeReference;
        
        private void Awake()
        {
            //create new instance of a ItemContainerDefinition using the containerTemplate 
            Inventory = Instantiate(containerTemplate);
            Inventory.Initialize();
            
            //assign the inventory debug reference
            inventoryRunTimeReference = Inventory;
        }

        private void OnDestroy()
        {
            Inventory.OnDestroyCleanup();
        }

        /// <summary>
        /// updates the selected slot data for a player
        /// </summary>
        /// <param name="index"></param>
        public void SelectSlotIndex(int index)
        {
            selectedSlotIndex = index;
            Log.Info("slot " + selectedSlotIndex + " selected");
            
            onSelectedSlotChanged.Raise(selectedSlotIndex);
        }

        public ItemSlot GetInventorySlot()
        {
            ItemSlot itemSlot = Inventory.ItemContainer.GetSlotByIndex(selectedSlotIndex);

            if (itemSlot.itemData == null)
            {
                Log.Info("item slot data is null");
            }
            else
            {
                Log.Info(itemSlot.itemData.Name);
            }

            return itemSlot;
        }
    }
}
