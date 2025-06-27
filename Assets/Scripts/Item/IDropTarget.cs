namespace Prototype_S
{
    
    /// <summary>
    /// interface for representing Drop Target Behaviour
    /// </summary>
    public interface IDropTarget
    {

        /// <summary>
        /// handles behaviour for when an IDraggableItem is dropped on it
        /// </summary>
        /// <param name="item">item dropped</param>
        void OnDrop(IDraggableItem item);

    }
}