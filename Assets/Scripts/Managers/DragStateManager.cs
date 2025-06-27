using UnityEngine;

namespace Prototype_S
{
    //manages the state of a single dragged entity/item
    public class DragStateManager : MonoBehaviour
    {
        
        public static DragStateManager Instance { get; private set; }
        private IDraggable heldItem = null;
        private bool isHolding = false;
        private Transform originalParent;
        private CanvasGroup canvasGroup;
        
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }

            Instance = this;
        }

        void Update()
        {
            if (heldItem != null)
            {
                heldItem.Transform.position = Input.mousePosition;

                if (Input.GetMouseButtonDown(0) && isHolding)
                {
                    DropItem();
                }

                isHolding = true;

            }
        }
        
        public bool HoldItem(IDraggable item) 
        {
            //already holding an item
            if (heldItem != null)
            {
                return false;
            }
            
            heldItem = item;
            return true;
        }

        private void DropItem()
        {
            heldItem.OnDrop();
        }
    }

}
