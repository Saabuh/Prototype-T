using UnityEngine;
using UnityEngine.EventSystems;

namespace Prototype_S.UI
{
    /// <summary>
    /// Manages the internal state of a single ItemIcon
    /// </summary>
    public class ItemIconHandler : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IDraggableItem
    {
        [SerializeField] protected ItemSlotUI itemSlotUI;
        private CanvasGroup canvasGroup;
        private Transform originalParent;
        private bool isHovering;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public Transform Transform => transform;
        public ItemSlotUI ItemSlotUI => itemSlotUI;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnPickup();
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
                ItemSpawner.SpawnItem(PlayerController.LocalPlayerInstance.transform.position, itemSlotUI.ItemSlot.itemData, itemSlotUI.ItemSlot.quantity);
                
                // Reset item slot after spawning creating item entity
                itemSlotUI.Inventory.RemoveAt(itemSlotUI.SlotIndex);
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