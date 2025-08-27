using System.Collections;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace Prototype_S
{
    /**
     * Represents an item existing in the world space
     */
    public class ItemEntity : NetworkBehaviour, IPickupable
    {
        public NetworkVariable<int> itemID = new NetworkVariable<int>();
        public NetworkVariable<int> quantity = new NetworkVariable<int>();
        public NetworkVariable<int> pickupDelay = new NetworkVariable<int>();
        private float pickUpDelay = 0f;
        private bool canBePickedUp = false;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            //build the itemEntity locally on the client
            GetComponent<SpriteRenderer>().sprite = ItemDatabase.Instance.GetItemByID(itemID.Value).Icon;
            
            StartCoroutine(EnablePickupAfterDelay());
        }

        /// <summary>
        /// Initializes the values of the networkVariables to be syncrhonzied across the network
        /// </summary>
        /// <param name="itemData"></param>
        /// <param name="quantity"></param>
        /// <param name="pickupDelay"></param>
        public void Initialize(int itemID, int quantity, int pickupDelay) {
            
            this.itemID.Value = itemID;
            this.quantity.Value = quantity;
            this.pickupDelay.Value = pickupDelay;
        }

        public void DrawVisuals()
        {
        }

        public void Pickup(PlayerController player)
        {

            // if (!canBePickedUp)
            // {
            //     return;
            // }
            //
            // ItemContainerDefinition itemContainerDefinition = player.PlayerInventory.Inventory;
            //
            // if (itemContainerDefinition != null)
            // {
            //     //create new itemSlot to add
            //     ItemSlot itemSlotToAdd = new ItemSlot(ItemInstance, Quantity);
            //     
            //     //Add to inventory
            //     itemContainerDefinition.ItemContainer.AddItem(itemSlotToAdd);
            //     
            //     Destroy(this.gameObject);
            //     
            //     // Log.Info("item was picked up and destroyed");
            //
            // }
            
            
            
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
