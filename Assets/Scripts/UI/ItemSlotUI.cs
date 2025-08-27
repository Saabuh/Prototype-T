using System;
using Prototype_S.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Prototype_S
{
    /// <summary>
    /// represents a single ItemSlot in the UI
    /// </summary>
    public class ItemSlotUI : MonoBehaviour, IDropTarget
    {
        
        //fields
        private PlayerInventory playerInventory;
        [SerializeField] private TextMeshProUGUI itemQuantityText = null;
        [SerializeField] private Image itemIconImage = null;
        [SerializeField] private Image itemSlotImage = null;
        private bool isSelected = false;
        
        //getter properties
        public PlayerInventory PlayerInventory => playerInventory;
        public int SlotIndex { get; private set; }
        // public ItemSlot ItemSlot => itemContainerDefinition.ItemContainer.GetSlotByIndex(SlotIndex);
        public ItemSlot ItemSlot => playerInventory.GetInventoryItemSlot(SlotIndex);
        // public ItemContainer Inventory => itemContainerDefinition.ItemContainer;
        public bool IsSelected => isSelected;

        public void Initialize(PlayerInventory inventory, int slotIndex)
        {
            this.playerInventory = inventory;
            SlotIndex = slotIndex;
        }

        public void UpdateSlotUI()
        {
            
            if (isSelected)
            {
                itemSlotImage.color = new Color(1f, 0.5f, 0f, 0.5f);
            }
            else
            {
                itemSlotImage.color = new Color(0f, 0f, 0f, 0.8f);
            }
            
            if (ItemSlot.itemID == -1)
            {
                EnableSlotUI(false);
                return;
            }
            
            EnableSlotUI(true);
            
            // itemIconImage.sprite = ItemSlot.itemInstance.itemData.Icon;
            itemIconImage.sprite = ItemDatabase.Instance.GetItemByID(ItemSlot.itemID).Icon;
            itemQuantityText.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
        }

        public void UpdateSelectedSlot(bool value)
        {
            isSelected = value;
        }

        private void EnableSlotUI(bool enable)
        {
            itemIconImage.enabled = enable;
            itemQuantityText.enabled = enable;
        }
        

        private void OnEnable()
        {
            //make sure itemContainer is defined first
            if (playerInventory != null)
            {
                UpdateSlotUI();
            }
        }

        public void OnDrop(IDraggableItem itemIcon)
        {
            if (itemIcon == null)
            {
                return;
            }

            if (itemIcon.ItemSlotUI)
            {
                playerInventory.SwapItemServerRpc(itemIcon.ItemSlotUI.SlotIndex, SlotIndex);
            }

            // if (item.ItemSlotUI)
            // {
            //     itemContainerDefinition.ItemContainer.Swap(item.ItemSlotUI.SlotIndex, SlotIndex);
            // }
            
        }
    }
}
