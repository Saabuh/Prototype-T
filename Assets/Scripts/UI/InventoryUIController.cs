using System;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype_S
{
    /// <summary>
    /// Single responsibility as coordinating behaviour between the inventory and the inventory ui
    /// </summary>
    public class InventoryUIController : MonoBehaviour
    {
        // player inventory reference
        [SerializeField] private PlayerInventory playerInventory;
        
        // The prefab for a single UI slot 
        [SerializeField] private GameObject itemSlotUIPrefab;
        
        // The parent Transform for all the slot UI objects
        [SerializeField] private Transform slotContainer;

        private List<ItemSlotUI> uiSlots = new List<ItemSlotUI>();

        private void Start()
        {

            if (playerInventory == null)
            {
                Log.Info("PlayerInventory is not yet defined");
                return;
            }
            
            CreateInventorySlots();
        }

        /// <summary>
        /// instantiates and initializes the inventorySlot prefabs
        /// </summary>
        private void CreateInventorySlots()
        {
            //grab a reference to the instantiated player inventory
            ItemContainerDefinition containerDefinition = playerInventory.Inventory;
            int containerSize = containerDefinition.ContainerSize;
            
            //create an itemslot gameObject for each slot
            for (int i = 0; i < containerSize; i++)
            {
                GameObject itemSlotGo = Instantiate(itemSlotUIPrefab, slotContainer);
                ItemSlotUI itemSlotUI = itemSlotGo.GetComponent<ItemSlotUI>();

                itemSlotUI.Initialize(containerDefinition, i);
                
                uiSlots.Add(itemSlotUI);

            }
            
            UpdateAllSlots();
        }

        public void UpdateAllSlots()
        {
            foreach (var slot in uiSlots)
            {
                slot.UpdateSlotUI();
            }
        }
    }
}
