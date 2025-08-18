using System;
using Item;
using Unity.Netcode;
using UnityEngine;

namespace Prototype_S
{
    /**
     * Responsible for managing anything involving player/user interaction (mouse clicks, movement, etc.)
     */
    public class PlayerController : NetworkBehaviour
    {
        //static instance 
        public static PlayerController LocalInstance { get; private set; }
        
        //events
        public static event Action<PlayerController> OnLocalPlayerConnected;
        
        //fields
        public float playerSpeed = 15.0f;
        
        //facade properties
        private PlayerInventory playerInventory;
        private InputReader playerInput;
        private PlayerInteraction interactor;
        private Rigidbody2D playerRb;
        
        //getter properties
        public PlayerInventory PlayerInventory => playerInventory;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            //check if we are the owner of this gameObject on network spawn
            if (IsOwner)
            {
                
                Log.Info("Player Connected.");
                
                LocalInstance = this;
                
                //set camera follow
                CameraController cameraController = Camera.main.GetComponent<CameraController>();

                if (cameraController != null)
                {
                    cameraController.player = this.transform;
                }
                else
                {
                    Log.Info("camera controller was not found");
                }
                
                //invoke player spawn event
                OnLocalPlayerConnected?.Invoke(this);
            }
            else
            {
                //disable input if we are not the owner of this gameObject
                GetComponent<InputReader>().enabled = false;
            }
            
            
        }

        void Awake()
        {
            //initialize facade references
            playerRb = GetComponent<Rigidbody2D>();
            playerInput = GetComponent<InputReader>();
            playerInventory = GetComponent<PlayerInventory>();
        }

        void FixedUpdate()
        {

            if (!IsOwner) return;
            
            Move();
        }

        private void Move()
        {
            //read player input, apply on clientside
            Vector2 movement = new Vector2(playerInput.Horizontal, playerInput.Vertical);
            playerRb.linearVelocity = movement * playerSpeed;
            
            //apply on server side to allow synchronization and correction afterwards
            MoveServerRpc(movement); 

        }

        [ServerRpc]
        private void MoveServerRpc(Vector2 movement)
        {
            playerRb.linearVelocity = movement * playerSpeed;
        }

        public void Teleport(MapData mapData)
        {
            playerRb.position = new Vector2(mapData.mapWidth / 2, mapData.mapHeight / 2);
        }
        
        public void HandleUse(Vector3 mousePosition)
        {
            
            Log.Info("Handling use");
            
            ItemInstance item = PlayerInventory.SelectedItem;

            if (item == null || item.itemData.ItemAction == null)
            {
                Log.Info("No item selected or no item action defined");
                return;
            }
            
            item.itemData.ItemAction.StartUse(this.gameObject, item, mousePosition);
            
            
            // //determine aim direction
            // Vector2 direction = (mousePosition - (Vector2)player.transform.position).normalized;
            //
            // GameObject projectile =
            //     Instantiate(player.projectilePrefab, player.transform.position, Quaternion.identity);
            //
            // projectile.GetComponent<Projectile>().Initialize(direction);
        }
    }
}
