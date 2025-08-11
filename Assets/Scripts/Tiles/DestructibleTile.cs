using UnityEngine;
using UnityEngine.Tilemaps;

namespace Prototype_S
{
    /// <summary>
    /// class that represents properties of destructible tiles
    /// </summary>
    
    [CreateAssetMenu(fileName = "New Destructible Tile", menuName = "Tiles/Destructible Tile")]
    public class DestructibleTile : Tile
    {
        //fields
        public int maxHealth;
        public ItemData itemDrop;
        public int dropAmount;

    }
}
