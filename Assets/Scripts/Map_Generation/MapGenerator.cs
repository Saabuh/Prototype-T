using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Prototype_S
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private float treeScale;
        [SerializeField] private float treeDensity = 0.25f;

        [SerializeField] private Tilemap foliageMap;
        [SerializeField] private Tilemap terrainMap; // Reference to the Tilemap
        [SerializeField] private Tilemap waterMap; // Reference to the Tilemap

        [SerializeField] private TileBase treeTile;
        [SerializeField] private TileBase waterTile; // Tile for water
        [SerializeField] private TileBase grassTile; // Tile for grass
        [SerializeField] private TileBase sandTile; // Tile for mountains
        
        [Header("Map Noise Generation Settings")]
        [SerializeField] private int mapWidth;
        [SerializeField] private int mapHeight;
        [SerializeField] private float scale;
        [SerializeField] private int octaves;
        [SerializeField] private float exponent;
        [SerializeField] private int seed;
        

        [ContextMenu("Generate Map")]
        public void GenerateMap()
          {
            terrainMap.ClearAllTiles();
            waterMap.ClearAllTiles();

            //generate terrain noise map
            float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, scale, octaves, exponent, seed);

            //fill terrain tiles based on noise map
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    Tilemap selectedTileMap;
                    TileBase selectedTile;

                    switch (noiseMap[x, y])
                    {
                        case < 0.20f:
                            selectedTile = waterTile;
                            selectedTileMap = waterMap;
                            break;
                        case < 0.26f:
                            selectedTile = sandTile;
                            selectedTileMap = terrainMap;
                            break;
                        default:
                            selectedTile = grassTile;
                            selectedTileMap = terrainMap;
                            break;
                    }

                    selectedTileMap.SetTile(new Vector3Int(x, y, 0), selectedTile);
                }
            }
        }

        [ContextMenu("Generate Trees")]
        private void TreeGenerator()
        {
            foliageMap.ClearAllTiles();
            float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, treeScale);

            //generate trees based on noise map
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    float density = Random.Range(0.05f, treeDensity);
                    if (!isWater(x, y) && !isSand(x, y) && noiseMap[x, y] < density)
                    {
                        foliageMap.SetTile(new Vector3Int(x, y, 0), treeTile);
                    }
                }
            }
        }
        
        

        private bool isWater(int x, int y)
        {
            return (waterMap.GetTile(new Vector3Int(x, y, 0)) == waterTile);
        }

        private bool isSand(int x, int y)
        {
            return (terrainMap.GetTile(new Vector3Int(x, y, 0)) == sandTile);
        }
    }
}