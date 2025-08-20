using System;
using Unity.Netcode;
using UnityEngine;

namespace Prototype_S
{

    /// <summary>
    /// Responsible for PlayerInventory Behaviour/Logic
    /// </summary>

    public class PlayerInventory : NetworkBehaviour
    {
        //fields
        public NetworkList<ItemSlot> inventorySlots;
        private ItemContainer localInventory;
        public int inventorySize = 40;
        
        //events
        [SerializeField] private VoidEvent onInventoryItemsUpdated;
        
        //for testing
        public ItemSlot testItemSlot = new ItemSlot(0, 1);
        

        private void Awake()
        {
            inventorySlots = new NetworkList<ItemSlot>();
        }

        void Update()
        {
            if (!IsOwner)
            {
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.T))
            {
                TestAddItemServerRpc();
            }
        }

        public override void OnNetworkSpawn()
        {
            //Subscribe to the OnListChanged event on all clients
            inventorySlots.OnListChanged += OnInventoryItemsUpdated;
            
            //only allow the server to populate the networkList and access to inventory logic
            if (IsServer)
            {
                localInventory = new ItemContainer(inventorySlots, inventorySize);
            }
        }

        public void OnInventoryItemsUpdated(NetworkListEvent<ItemSlot> changeEvent)
        {
            Log.Info("testing event invoke on client" + OwnerClientId);
            Log.Info(changeEvent.ToString());
            
            // onInventoryItemsUpdated.Raise();
        }


        [ServerRpc]
        public void AddItemServerRpc(ItemSlot itemSlot)
        {
            Log.Info("attempting adding an item for client " + OwnerClientId + ".");
            localInventory.AddItem(itemSlot);

        }

        [ServerRpc]
        public void TestAddItemServerRpc()
        {
            Log.Info("attempting to add an itemSlot containing item " + ItemDatabase.Instance.GetItemByID(testItemSlot.itemID).Name + "for clientID" + OwnerClientId);
            localInventory.AddItem(testItemSlot);
        }

    }
    
    // public class PlayerInventory : NetworkBehaviour
    // {
    //
    //     //events
    //     [SerializeField] private IntegerEvent onSelectedSlotChanged;
    //     
    //     //fields
    //     // [Tooltip("Template used to create new inventory")]
    //     // [SerializeField] private ItemContainerDefinition containerTemplate;
    //     // public ItemContainerDefinition Inventory { get; private set; }
    //     public int selectedSlotIndex = 0;
    //     public NetworkList<ItemSlot> inventorySlots;
    //     
    //     //getter properties
    //     // public ItemInstance SelectedItem => GetInventorySlot().itemInstance;
    //     
    //     //used purely for debugging/ spawning items
    //     // [Header("reference to instantiated Inventory")] [SerializeField]
    //     // private ItemContainerDefinition inventoryRunTimeReference;
    //     
    //     private void Awake()
    //     {
    //         //create new instance of a ItemContainerDefinition using the containerTemplate 
    //         Inventory = Instantiate(containerTemplate);
    //         Inventory.Initialize();
    //         
    //         //assign the inventory debug reference
    //         inventoryRunTimeReference = Inventory;
    //     }
    //
    //     private void OnDestroy()
    //     {
    //         Inventory.OnDestroyCleanup();
    //     }
    //
    //     /// <summary>
    //     /// updates the selected slot data for a player
    //     /// </summary>
    //     /// <param name="index"></param>
    //     public void SelectSlotIndex(int index)
    //     {
    //         selectedSlotIndex = index;
    //         Log.Info("slot " + selectedSlotIndex + " selected");
    //         
    //         onSelectedSlotChanged.Raise(selectedSlotIndex);
    //     }
    //
    //     public ItemSlot GetInventorySlot()
    //     {
    //         ItemSlot itemSlot = Inventory.ItemContainer.GetSlotByIndex(selectedSlotIndex);
    //
    //         if (itemSlot.itemInstance == null)
    //         {
    //             Log.Info("item slot data is null");
    //         }
    //         else
    //         {
    //             Log.Info(itemSlot.itemInstance.itemData.Name);
    //         }
    //
    //         return itemSlot;
    //     }
    // }
}
