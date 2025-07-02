using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Prototype_S
{
    //manages the state of a single dragged entity/item
    public class DragStateManager : MonoBehaviour
    {
        //events
        [SerializeField] private VoidEvent onDragStateEnter;
        [SerializeField] private BooleanEvent onDragStateExit;
        
        //fields
        private IDraggableItem heldItem = null;
        private bool isHolding = false;
        private Transform originalParent;
        private CanvasGroup canvasGroup;
        
        //getter properties
        public bool IsHolding => isHolding;
        
        //singleton
        public static DragStateManager Instance { get; private set; }
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
                    Release();
                }
                
                isHolding = true;
            
            }
        }

        private void OnDisable()
        {
            
        }

        public bool HoldItem(IDraggableItem item) 
        {
            //already holding an item
            if (heldItem != null)
            {
                return false;
            }
            
            heldItem = item;
            isHolding = false;
            
            //raise event to listeners
            onDragStateEnter.Raise(); 
            
            return true;
        }

        /// <summary>
        /// Releases the currently dragged entity.
        /// </summary>
        private void Release()
        {
            
            
            //build pointer event data
            PointerEventData pointerData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            IDropTarget dropTarget = null;

            //look for any drop targets
            foreach (var result in results)
            {
                dropTarget = result.gameObject.GetComponent<IDropTarget>();

                if (dropTarget != null)
                {
                    //Raise boolean event
                    onDragStateExit.Raise(true);
                    
                    dropTarget.OnDrop(heldItem);
                    heldItem.OnRelease(true);
                    break;
                }
            }

            if (dropTarget == null)
            {
                onDragStateExit.Raise(false);
                heldItem.OnRelease(false);
            }

            //reset the dragStateManager
            heldItem = null;
            isHolding = false;
            

        }
    }

}
