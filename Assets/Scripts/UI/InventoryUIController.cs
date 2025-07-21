using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private Transform inventorySlotContainer;
        [SerializeField] private Transform hotbarSlotContainer;

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
                
                //assign the first 10 slots to the hotbar UI
                GameObject itemSlotGo;
                
                if (i < 10)
                { 
                    itemSlotGo = Instantiate(itemSlotUIPrefab, hotbarSlotContainer);
                }
                else
                {
                    itemSlotGo = Instantiate(itemSlotUIPrefab, inventorySlotContainer);
                }
                
                ItemSlotUI itemSlotUI = itemSlotGo.GetComponent<ItemSlotUI>();
                
                //make the first itemslot selected on instantiation/by default
                if (i == 0)
                {
                    itemSlotUI.UpdateSelectedSlot(true);
                }

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

        public void UpdateSelectedSlot(int slotIndex)
        {
            Log.Info("Update selected slot " + slotIndex);
            foreach (var slot in uiSlots)
            {
                if (slot.SlotIndex == slotIndex)
                {
                      slot.UpdateSelectedSlot(true);
                }
                else
                {
                    slot.UpdateSelectedSlot(false);
                }

                slot.UpdateSlotUI();
            }
        }
    }
}
