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
        private ItemContainerDefinition itemContainerDefinition = null;
        [SerializeField] private TextMeshProUGUI itemQuantityText = null;
        [SerializeField] private Image itemIconImage = null;
        
        //properties
        public int SlotIndex { get; private set; }
        public ItemSlot ItemSlot => itemContainerDefinition.ItemContainer.GetSlotByIndex(SlotIndex);

        public ItemContainer Inventory => itemContainerDefinition.ItemContainer;

        // void Start()
        // {
        //     SlotIndex = transform.GetSiblingIndex();
        //     UpdateSlotUI();
        // }

        public void Initialize(ItemContainerDefinition itemContainer, int slotIndex)
        {
            itemContainerDefinition = itemContainer;
            SlotIndex = slotIndex;
        }

        public void UpdateSlotUI()
        {
            
            if (ItemSlot.itemData == null)
            {
                EnableSlotUI(false);
                return;
            }

            EnableSlotUI(true);

            itemIconImage.sprite = ItemSlot.itemData.Icon;
            itemQuantityText.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
        }

        private void EnableSlotUI(bool enable)
        {
            itemIconImage.enabled = enable;
            itemQuantityText.enabled = enable;
        }

        private void OnEnable()
        {
            //make sure itemContainer is defined first
            if (itemContainerDefinition != null)
            {
                UpdateSlotUI();
            }
        }

        public void OnDrop(IDraggableItem item)
        {
            if (item == null)
            {
                return;
            }

            if (item.ItemSlotUI)
            {
                itemContainerDefinition.ItemContainer.Swap(item.ItemSlotUI.SlotIndex, SlotIndex);
            }
            
        }
    }
}
