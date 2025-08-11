using UnityEngine;
using UnityEngine.Tilemaps;

namespace Prototype_S
{
    [CreateAssetMenu(fileName = "New Place Block Action", menuName = "Actions/Place Block Action")]
    public class PlaceBlockAction : ItemAction
    {

        [SerializeField] private int placeBlockMaxRange = 5;

        public override void StartUse(GameObject user, ItemInstance itemInstance, Vector3 mousePosition)
        {
            
            
            Log.Info("Performing Place Block Action with a max range of " + placeBlockMaxRange);
            Log.Info("Performing at mouse position x: " + mousePosition.x + " y: " + mousePosition.y);

            Tilemap destructibleTileMap = DestructibleTileManager.Instance.DestructibleTilemap;
            
            //access destructible tilemap
            Vector3Int cellPosition = destructibleTileMap.WorldToCell(mousePosition);
            Log.Info(cellPosition.x + ":" + cellPosition.y + ":" + cellPosition.z);
            
            if (itemInstance.itemData.Placeable && itemInstance.itemData is placeableItemData)
            {
                placeableItemData placeableItemData = (placeableItemData)itemInstance.itemData;
                DestructibleTile tile = placeableItemData.TileRef;
                
                //visually set the tile
                destructibleTileMap.SetTile(cellPosition, tile);
                //add the health to the tileManager
                DestructibleTileManager.Instance.tileHealth.Add(cellPosition, tile.maxHealth);

                Log.Info("tile health dictionary:");
                foreach (var val in DestructibleTileManager.Instance.tileHealth)
                {
                    Log.Info(val.Key + ":" + val.Value);
                }
                
            }
            
        }

        public override void OnUse(GameObject user, ItemInstance itemInstance, Vector3 mousePosition)
        {
            throw new System.NotImplementedException();
        }

        public override void EndUse(GameObject user, ItemInstance itemInstance, Vector3 mousePosition)
        {
            throw new System.NotImplementedException();
        }
    }
}