using System;
using UnityEngine;

namespace Prototype_S
{
    public class PlayerInventory : MonoBehaviour
    {

        //fields
        [SerializeField] private ItemContainerDefinition containerTemplate;
        public ItemContainerDefinition Inventory { get; private set; }
        private void Awake()
        {
            Inventory = Instantiate(containerTemplate);
            Inventory.Initialize();
        }
    }
}
