using System.Collections;
using UnityEngine;

namespace Prototype_S
{
    /**
     * Represents an item existing in the world space
     */
    public class ItemEntity : MonoBehaviour, IPickupable
    {
        public ItemData Item { get; private set; }
        public int Quantity { get; private set; }
        private float pickUpDelay = 0f;
        private bool canBePickedUp = false;

        private void Start()
        {
            StartCoroutine(EnablePickupAfterDelay());
        }
        public void Initialize(ItemData item, int quantity, float pickupDelay) {
            
            this.Item = item;
            this.Quantity = quantity;
            this.pickUpDelay = pickupDelay;
        }

        public void Pickup(PlayerController player)
        {

            if (!canBePickedUp)
            {
                return;
            }

            Inventory inventory = player.Inventory;

            if (inventory != null)
            {
                //create new itemSlot to add
                ItemSlot itemSlotToAdd = new ItemSlot(Item, Quantity);
                
                //Add to inventory
                inventory.ItemContainer.AddItem(itemSlotToAdd);
                
                Destroy(this.gameObject);
                
                // Log.Info("item was picked up and destroyed");

            }
            
            
            
        }

        private IEnumerator EnablePickupAfterDelay()
        {
            canBePickedUp = false;

            //new WaitForSeconds Object, return to Coroutine scheduler, then yield till complete
            yield return new WaitForSeconds(pickUpDelay);
            
            canBePickedUp = true;
            // Log.Info("Item can now be picked up.");
        }

    }
}
