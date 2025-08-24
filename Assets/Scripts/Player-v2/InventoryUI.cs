using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Prototype_S
{
    public class InventoryUI : MonoBehaviour
    {
        
        private PlayerInventory playerInventory;

        //fields
        [SerializeField] private GameObject itemSlotUIPrefab;
        [SerializeField] private Transform hotbarSlotUIContainer;
        [SerializeField] private Transform inventorySlotUIContainer;
        private List<ItemSlotUI> uiSlots = new List<ItemSlotUI>();
        
        
        void Awake()
        {
            //just get the reference
            playerInventory = GetComponentInParent<PlayerInventory>();
            playerInventory.OnNetworkReady += InitializeUI;
        }

        void OnDestroy()
        {
            if (playerInventory != null)
            {
                playerInventory.OnNetworkReady -= InitializeUI;
                playerInventory.OnInventoryItemsUpdated -= RefreshSlotsUI;
            }
        }

        void InitializeUI()
        {
            Log.Info("initializing UI.");
            playerInventory.OnInventoryItemsUpdated += RefreshSlotsUI;

            CreateInventorySlotsUI();
        }

        void CreateInventorySlotsUI()
        {
            for (int i = 0; i < playerInventory.inventorySize; i++)
            {
                GameObject itemSlotGo;

                //assign the first 10 slots to the hotbar UI
                if (i < 10)
                {
                    itemSlotGo = Instantiate(itemSlotUIPrefab, hotbarSlotUIContainer);
                } else 
                {
                    itemSlotGo = Instantiate(itemSlotUIPrefab, inventorySlotUIContainer);
                }
                
                ItemSlotUI itemSlotUI = itemSlotGo.GetComponent<ItemSlotUI>();

                if (i == 0)
                {
                    itemSlotUI.UpdateSelectedSlot(true);
                }
                
                itemSlotUI.Initialize(playerInventory, i);
                
                uiSlots.Add(itemSlotUI);
                
            }
            
            RefreshSlotsUI();
        }

        public void RefreshSlotsUI()
        {
            Log.Info("refreshing slots UI...");
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
