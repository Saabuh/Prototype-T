namespace Prototype_S
{
    /// <summary>
    /// interface for representing container-like behaviour
    /// </summary>
    public interface IItemContainer
    {
       ItemSlot AddItem(ItemSlot itemSlot);
       void RemoveItem(ItemSlot itemSlot);
       void RemoveAt(int slotIndex);
       void Swap(int indexOne, int indexTwo);
       bool HasItem(ItemData item);
       int GetTotalQuantity(ItemData item);
    }
}