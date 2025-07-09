using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Prototype_S
{
    public class FoliageGenerator : MonoBehaviour
    {
        //fields
        private int mapWidth = 1500;
        private int mapHeight = 1500;
        
        [SerializeField] private float treeScale;
        [SerializeField] private float treeDensity = 0.25f;
        [SerializeField] private Tilemap foliageMap;
        [SerializeField] private Tilemap terrainMap; // Reference to the Tilemap
        [SerializeField] private TileBase treeTile; // Reference to treeTile for now
        [SerializeField] private TileBase waterTile; // Tile for water
        [SerializeField] private TileBase sandTile; // Tile for mountains

        public void UpdateMapData(MapData mapData) 
        {
            mapWidth = mapData.mapWidth;
            mapHeight = mapData.mapHeight;
            Log.Info("Updated map width/height to " + mapWidth + "x, " + mapHeight + "y");
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
        
        [ContextMenu("Remove Foliage")]
        private void RemoveTrees()
        {
            foliageMap.ClearAllTiles();
        }
        

        private bool isWater(int x, int y)
        {
            return (terrainMap.GetTile(new Vector3Int(x, y, 0)) == waterTile);
        }

        private bool isSand(int x, int y)
        {
            return (terrainMap.GetTile(new Vector3Int(x, y, 0)) == sandTile);
        }
    }
}
