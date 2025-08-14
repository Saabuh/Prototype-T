using UnityEngine;
using UnityEngine.Tilemaps;

namespace Prototype_S
{
    public class MapQuery : MonoBehaviour
    {
        
        [Header("Data Sources")]
        [SerializeField] private Tilemap terrainMap;

        [Header("Tile Definitions")] 
        [SerializeField] private TileBase waterTile;
        [SerializeField] private TileBase sandTile;
        public static MapQuery Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        
        public bool IsTileWater(Vector3Int position)
        {
            TileBase tileAtPosition = terrainMap.GetTile(position);

            // Check if the tile at the position matches our water tile definition.
            return tileAtPosition == waterTile;
        }
        
        public bool IsTileSand(Vector3Int position)
        {
            TileBase tileAtPosition = terrainMap.GetTile(position);

            // Check if the tile at the position matches our water tile definition.
            return tileAtPosition == sandTile;
        }
    }
}