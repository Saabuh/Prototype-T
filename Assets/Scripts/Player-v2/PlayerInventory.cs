using System;
using UnityEngine;

namespace Prototype_S
{
    public class PlayerInventory : MonoBehaviour
    {

        //fields
        [Tooltip("Template used to create new inventory")]
        [SerializeField] private ItemContainerDefinition containerTemplate;
        public ItemContainerDefinition Inventory { get; private set; }

        //used purely for debugging/ spawning items
        [Header("reference to instantiated Inventory")] [SerializeField]
        private ItemContainerDefinition inventoryRunTimeReference;
        private void Awake()
        {
            //create new instance of a ItemContainerDefinition using the containerTemplate 
            Inventory = Instantiate(containerTemplate);
            Inventory.Initialize();
            
            //assign the inventory debug reference
            inventoryRunTimeReference = Inventory;

        }

        private void OnDestroy()
        {
            Inventory.OnDestroyCleanup();
        }
    }
}
