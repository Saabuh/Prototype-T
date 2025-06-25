namespace Prototype_S
{
    /// <summary>
    /// interface for representing something that is pickupable
    /// </summary>
    public interface IPickupable
    {
        
        /// <summary>
        /// Attempts to add an item to an inventory by picking up.
        /// </summary>
        /// <param name="player">player to give the item to.</param>
        void Pickup(PlayerController player);
    }
}
