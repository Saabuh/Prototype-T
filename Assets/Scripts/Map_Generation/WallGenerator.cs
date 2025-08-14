using UnityEngine;
using UnityEngine.Tilemaps;

namespace Prototype_S
{
    public class WallGenerator : BaseMapGenerator
    {

        //temp
        [SerializeField] private DestructibleTile destructibleTile;
        [SerializeField] private Tilemap terrainMap;
        public override void Generate()
          {
              
            drawMap.ClearAllTiles();
            DestructibleTileManager.Instance.tileHealth.Clear();
            
            //generate noise maps
            float[,] wallNoiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, scale, octaves, exponent, seed);

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {

                    if (wallNoiseMap[x, y] > 0.55f)
                    {
                        Vector3Int tilePosition = new Vector3Int(x, y, 0);
                        if (!MapQuery.Instance.IsTileWater(tilePosition) && !MapQuery.Instance.IsTileSand(tilePosition))
                        {
                            //visually set the tile
                            drawMap.SetTile(tilePosition, destructibleTile);
                            //add tile health
                            DestructibleTileManager.Instance.tileHealth.Add(new Vector3Int(x, y, 0), destructibleTile.maxHealth);
                        }
                            
                    }
                    
                }
            }
            

        }
    }
}