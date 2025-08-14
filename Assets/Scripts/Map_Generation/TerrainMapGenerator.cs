using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Prototype_S
{
    public class TerrainMapGenerator : BaseMapGenerator
    {
        [Header("Additional Seeds")]
        [SerializeField] protected int moistureSeed;

        //fields
        public List<Biome> biomes;
        public Dictionary<BiomeType, Biome> biomesDict;
        
        public override void Generate()
          {
            drawMap.ClearAllTiles();

            biomesDict = new Dictionary<BiomeType, Biome>();
            
            //populate biome dictionary
            foreach (var biome in biomes)
            {
                biomesDict.Add(biome.BiomeType, biome);
            }
            
            //generate noise maps
            float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, scale, octaves, exponent, seed, true);
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

                    drawMap.SetTile(new Vector3Int(x, y, 0), selectedTile);
                }
            }
            
            //Raise Map Generated Event
            RaiseMapGeneratedEvent(new MapData(mapWidth, mapHeight));

        }

    }
}