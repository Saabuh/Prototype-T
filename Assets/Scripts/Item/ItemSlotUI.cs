using System;
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
            inventory.ItemContainer.OnItemsUpdated += UpdateSlotUI;
            UpdateSlotUI();
        }

        private void UpdateSlotUI()
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

        public void OnDrop(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}
