using UnityEngine;

namespace Prototype_S
{
    /**
     * Represents an item existing in the world space
     */
    public class ItemEntity : MonoBehaviour
    {
        public ItemData Item { get; private set; }
        public int Quantity { get; private set; }

        public void Initialize(ItemData item, int quantity)
        {
            this.Item = item;
            this.Quantity = quantity;
        }
    }
}
