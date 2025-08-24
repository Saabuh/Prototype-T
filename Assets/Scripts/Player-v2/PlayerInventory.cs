using System;
using Unity.Netcode;
using UnityEngine;

namespace Prototype_S
{

    /// <summary>
    /// Inventory wrapper responsible for PlayerInventory Behaviour/Logic
    /// </summary>

    public class PlayerInventory : NetworkBehaviour
    {
        //fields
        private NetworkList<ItemSlot> inventory;
        private ItemContainer localInventory;
        public int inventorySize = 40;
        public int selectedSlotIndex;
        
        //events
        public event Action OnInventoryItemsUpdated;
        public event Action OnNetworkReady;
        public IntegerEvent onSelectedSlotChanged;
        
        //for testing
        public ItemSlot testItemSlot1 = new ItemSlot(0, 1);
        public ItemSlot testItemSlot2 = new ItemSlot(1, 5);

        private void Awake()
        {
            inventory = new NetworkList<ItemSlot>();
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
            inventory.OnListChanged += OnInventoryUpdated;
            
            //only allow the server to write to the networkList, but provide access to inventory read logic on all clients
            if (IsServer)
            {
                for (int i = 0; i < inventorySize; i++)
                {
                    inventory.Add(new ItemSlot(-1, 0));
                }
            }

            //provide the reference to the inventory logic for all clients
            localInventory = new ItemContainer(inventory, inventorySize);
            
            if (IsOwner)
            {
                OnNetworkReady?.Invoke();
            }
            
        }

        public void OnInventoryUpdated(NetworkListEvent<ItemSlot> changeEvent)
        {

            Log.Info("Inventory change detected for " + OwnerClientId + " for networkObject " + NetworkObjectId);

            //only invoke the event if the client owns this networkObject
            //change this to c# events instead 
            if (!IsOwner)
            {
                Log.Info("Not the owner of networkObject " + NetworkObjectId + " detecting a change");
                return;
            }
            
            OnInventoryItemsUpdated?.Invoke();
        }
        public void SelectSlotIndex(int index)
        {
            selectedSlotIndex = index;
            Log.Info("slot " + selectedSlotIndex + " selected");

            if (!IsOwner)
            {
                Log.Info("Not the owner of networkObject " + NetworkObjectId + " detecting a change");
                return;
            }
            
            onSelectedSlotChanged.Raise(selectedSlotIndex);
        }

        public ItemSlot GetInventoryItemSlot(int slotIndex)
        {
            return localInventory.GetSlotByIndex(slotIndex);
        }

        [ServerRpc]
        public void AddItemServerRpc(ItemSlot itemSlot)
        {
            Log.Info("attempting adding an item for client " + OwnerClientId + ".");
            localInventory.AddItem(itemSlot);

        }

        [ServerRpc]
        public void SwapItemServerRpc(int itemSlotIndex1, int itemSlotIndex2)
        {
            Log.Info("attempting item swap for client " + OwnerClientId + ".");
            localInventory.Swap(itemSlotIndex1, itemSlotIndex2);
        }
        

        [ServerRpc]
        public void TestAddItemServerRpc()
        {
            Log.Info("attempting to add an itemSlot containing item " + ItemDatabase.Instance.GetItemByID(testItemSlot1.itemID).Name + "for clientID" + OwnerClientId);
            localInventory.AddItem(testItemSlot1);
            localInventory.AddItem(testItemSlot2);
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
