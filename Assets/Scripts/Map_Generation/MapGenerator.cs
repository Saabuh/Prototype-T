using System.Collections.Generic;
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

        [SerializeField] private TileBase treeTile;
        [SerializeField] private TileBase waterTile; // Tile for water
        [SerializeField] private TileBase grassTile; // Tile for grass
        [SerializeField] private TileBase sandTile; // Tile for mountains

        public List<Biome> biomes;
        public Dictionary<BiomeType, Biome> biomesDict;
        
        [Header("Map Noise Generation Settings")]
        [SerializeField] private int mapWidth;
        [SerializeField] private int mapHeight;
        [SerializeField] private float scale;
        [SerializeField] private int octaves;
        [SerializeField] private float exponent;
        [Header("Noise Seeds")]
        [SerializeField] private int terrainSeed;
        [SerializeField] private int moistureSeed;

        [ContextMenu("Generate Map")]
        public void GenerateMap()
          {
            terrainMap.ClearAllTiles();

            biomesDict = new Dictionary<BiomeType, Biome>();
            
            //populate biome dictionary
            foreach (var biome in biomes)
            {
                biomesDict.Add(biome.BiomeType, biome);
            }
            
            //generate noise maps
            float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, scale, octaves, exponent, terrainSeed);
            float[,] moistureMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, scale, octaves, exponent, moistureSeed);
            
            //fill terrain tiles based on noise map
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    TileBase selectedTile = null;
                    float moisture = moistureMap[x, y];
                    
                    switch(noiseMap[x, y])
                    {
                        case < 0.30f:
                            selectedTile = biomesDict[BiomeType.Water].BiomeTile;
                            break;
                        case < 0.33f:
                            selectedTile = biomesDict[BiomeType.Beach].BiomeTile;
                            break;
                        default:
                            if (moisture < 0.30f) 
                            {
                                selectedTile = biomesDict[BiomeType.Desert].BiomeTile;
                            } else if (moisture < 0.45f)
                            {
                                selectedTile = biomesDict[BiomeType.Grasslands].BiomeTile;
                            } else if (moisture < 0.57f)
                            {
                                selectedTile = biomesDict[BiomeType.Rainforest].BiomeTile;
                            } else 
                            {
                                selectedTile = biomesDict[BiomeType.Tundra].BiomeTile;
                            }
                            break;
                            
                    }

                    terrainMap.SetTile(new Vector3Int(x, y, 0), selectedTile);
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