using UnityEngine;

namespace Prototype_S
{
    // Responsible for managing the player's interaction with the world.
    public class PlayerInteraction : MonoBehaviour
    {
        private PlayerController playerController;

        void Awake()
        {
            playerController = GetComponent<PlayerController>();
        }
        
        private void OnTriggerStay2D(Collider2D other) 
        {
            
            //find a component that can be treated as IPickupable, and treat it as such from here on
            IPickupable pickupItem = other.GetComponent<IPickupable>();
            if (pickupItem != null)
            {
                pickupItem.Pickup(playerController); 
            }
        }

    }
}