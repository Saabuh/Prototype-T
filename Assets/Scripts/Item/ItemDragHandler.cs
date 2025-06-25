using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Prototype_S
{
    public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected ItemSlotUI itemSlotUI;
        private CanvasGroup canvasGroup;
        private Transform originalParent;
        private bool isHovering;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void onDisable()
        {
            if (isHovering)
            {
                isHovering = false;
            }
        }

        public ItemSlotUI ItemSlotUI => itemSlotUI;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                originalParent = transform.parent;
                transform.SetParent(transform.parent.parent);
                canvasGroup.blocksRaycasts = false;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.position = Input.mousePosition;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.SetParent(originalParent);
                transform.localPosition = Vector3.zero;
                canvasGroup.blocksRaycasts = true;

                
                //drop the item if its released outside of a canvas graphic
                if (eventData.hovered.Count == 0)
                {
                    ItemSpawner.SpawnItem(PlayerController.LocalPlayerInstance.transform.position, itemSlotUI.ItemSlot.itemData, itemSlotUI.ItemSlot.quantity);
                    
                    // Reset item slot after spawning creating item entity
                    itemSlotUI.Inventory.RemoveAt(itemSlotUI.SlotIndex);
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovering = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovering = false;
        }
    }
}