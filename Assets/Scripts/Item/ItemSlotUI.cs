using System;
using Prototype_S.Item;
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
    public class ItemSlotUI : MonoBehaviour, IDropHandler
    {

        //fields
        [SerializeField] private Inventory inventory = null;
        [SerializeField] private TextMeshProUGUI itemQuantityText = null;
        [SerializeField] private Image itemIconImage = null;
        
        //properties
        private int SlotIndex { get; set; }
        public ItemSlot ItemSlot => inventory.ItemContainer.GetSlotByIndex(SlotIndex);

        void Start()
        {
            SlotIndex = transform.GetSiblingIndex();
            UpdateSlotUI();
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
            Debug.Log("ItemSlot.quantity:" + ItemSlot.quantity);
            itemQuantityText.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
        }

        private void EnableSlotUI(bool enable)
        {
            itemIconImage.enabled = enable;
            itemQuantityText.enabled = enable;
        }

        public void OnDrop(PointerEventData eventData)
        {
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            if (itemDragHandler == null)
            {
                return; 
            }

            if (itemDragHandler.ItemSlotUI != null)
            {
                inventory.ItemContainer.Swap(itemDragHandler.ItemSlotUI.SlotIndex, SlotIndex);
            }


        }
    }
}
