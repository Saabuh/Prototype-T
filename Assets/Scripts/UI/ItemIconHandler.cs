using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Prototype_S.UI
{
    /// <summary>
    /// Manages the internal state and behaviour of a single ItemIcon
    /// </summary>
    public class ItemIconHandler : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IDraggableItem
    {
        
        //events
        [SerializeField] private ItemSlotEvent onItemSlotHoverEnter;
        [SerializeField] private VoidEvent onItemSlotHoverExit;
        
        //fields
        [SerializeField] protected ItemSlotUI itemSlotUI;
        private CanvasGroup canvasGroup;
        private Transform originalParent;
        private bool isHovering;

        //properties
        public Transform Transform => transform;
        public ItemSlotUI ItemSlotUI => itemSlotUI;
        
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        
        private void OnDisable()
        {
            if (isHovering)
            {
                onItemSlotHoverExit.Raise();
                isHovering = false;
                Debug.Log("Raising hover exit event.");
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnPickup();
                onItemSlotHoverExit.Raise();
            }
        }

        public void OnPickup()
        {
            if (DragStateManager.Instance.HoldItem(this))
            {
                originalParent = transform.parent;
                transform.SetParent(DragCanvas.Instance.transform);
                canvasGroup.blocksRaycasts = false;
            }
            
        }
        
        public void OnRelease(bool targetFound)
        {
            
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            canvasGroup.blocksRaycasts = true;

            if (!targetFound)
            {
                ItemSpawner.SpawnItem(PlayerController.LocalPlayerInstance.transform.position,
                    itemSlotUI.ItemSlot.itemData, itemSlotUI.ItemSlot.quantity);

                // Reset item slot after spawning creating item entity
                itemSlotUI.Inventory.RemoveAt(itemSlotUI.SlotIndex);
            } 
            else
            {
                //manually trigger OnPointerEnter event, swap the data, not the itemslot? in case it gets buggy
                // HandlePointerEnter();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovering = true;
            onItemSlotHoverEnter.Raise(itemSlotUI.ItemSlot);
            Debug.Log(itemSlotUI.ItemSlot.itemData);
            Debug.Log("Raising hover enter event.");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovering = true;
            onItemSlotHoverExit.Raise();
            Debug.Log("Raising hover exit event.");
        }
        
    }
}