using System.Collections.Generic;
using UnityEngine;

namespace Prototype_S
{
    public class ItemDatabase : MonoBehaviour
    {
        public static ItemDatabase Instance { get; private set; }

        private Dictionary<int, ItemData> itemDictionary = new Dictionary<int, ItemData>();

        void Awake()
        {

            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;

            var allItems = Resources.LoadAll<ItemData>("Items");
            
            foreach (var itemAsset in allItems)
            {
                itemDictionary[itemAsset.ItemID] = itemAsset;
            }
        }

        public ItemData GetItemByID(int itemID)
        {
            return itemDictionary[itemID];
        }
        
        
    }
}