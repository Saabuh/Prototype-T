using UnityEngine;
using UnityEngine.Tilemaps;

namespace Prototype_S
{
    [CreateAssetMenu(fileName = "New Mining Action", menuName = "Actions/Mining Action")]
    public class MiningAction : ItemAction
    {

        [SerializeField] private int miningDistance = 5;
        [SerializeField] private int miningSpeed = 2;

        public override void StartUse(GameObject user, ItemInstance itemInstance, Vector3 mousePosition)
        {
            Log.Info("Performing mining action with a mining distance of " + miningDistance + " and a mining speed of " + miningSpeed);
            Log.Info("Performing at mouse position x: " + mousePosition.x + " y: " + mousePosition.y + " z: " + mousePosition.z);

            DestructibleTileManager tileManager = DestructibleTileManager.Instance;

            if (itemInstance.itemData is WeaponData)
            {
                WeaponData weaponData = (WeaponData)itemInstance.itemData;
                
                //access destructible tilemap
                Vector3Int cellPosition = tileManager.DestructibleTilemap.WorldToCell(mousePosition);
                Log.Info(cellPosition.x + ":" + cellPosition.y + ":" + cellPosition.z);
            
                //damage tile
                tileManager.DamageTile(cellPosition, weaponData.BaseMiningDamage);
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
