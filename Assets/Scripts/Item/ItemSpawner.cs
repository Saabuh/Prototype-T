using UnityEngine;

namespace Prototype_S
{
    //Itemspawner that can be called from anywhere in the code to spawn an item into the game world.
    public static class ItemSpawner
    {
        private static GameObject _worldItemPrefab;
        
        //one time method to link the template worldItemPrefab to the item spawner
        public static void Initialize(GameObject prefab)
        {  
            _worldItemPrefab = prefab;
        }

        public static void SpawnItem(Vector3 position, ItemInstance itemInstance , int quantity, float pickupDelay = 1f)
        {
            
             // --- Safety Checks ---
            if (_worldItemPrefab == null)
            {
                Log.Info("Cannot spawn item because the ItemInWorld prefab was not loaded.");
            }
            
            if (itemInstance == null)
            {
                Log.Info("Tried to spawn an item with null ItemData.");
            }

            //instantiate a blank canvas, a empty world item prefab
            GameObject itemObject = Object.Instantiate(_worldItemPrefab, position, Quaternion.identity);
            
            //grab components
            ItemEntity itemEntity = itemObject.GetComponent<ItemEntity>();
            SpriteRenderer spriteRenderer = itemObject.GetComponent<SpriteRenderer>();
            
            //populate prefab
            itemEntity.Initialize(itemInstance, quantity, pickupDelay);
            spriteRenderer.sprite = itemInstance.itemData.Icon;

        }

    }
}
