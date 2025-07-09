using Item;
using UnityEngine;

namespace Prototype_S
{
    /**
     * Responsible for managing anything involving player/user interaction (mouse clicks, movement, etc.)
     */
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController LocalPlayerInstance { get; private set; }
        
        //fields
        public float playerSpeed = 5.0f;
        
        // This boolean would be set by your networking system (e.g., Mirror, Netcode).
        // For testing, you can set it manually in the Inspector.
        [Tooltip("Is this the player controlled by this computer?")]
        public bool isLocalPlayer = true;
        
        //events
        [SerializeField] private VoidEvent onCharacterAttack;

        //facade properties
        [SerializeField] private Inventory inventory;
        private InputReader playerInput;
        private PlayerInteraction interactor;
        private Rigidbody2D playerRb;
        
        //getter properties
        public Inventory Inventory => inventory;

        void Awake()
        {

            //check if the player we are trying to create is the one we should control
            if (isLocalPlayer)
            {

                //check if an instance of the player we are to control already exists
                if (LocalPlayerInstance != null)
                {
                    Destroy(this.gameObject);
                    return;
                }
                
                LocalPlayerInstance = this;
            }
            
            //initialize facade references
            playerInput = GetComponent<InputReader>();
            playerRb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            
            //subscribe functions to events
            playerInput.OnFire += HandleUse;

        }
        
        

        void Update()
        {
            Move();
        }

        public void Teleport(MapData mapData)
        {
            transform.position = new Vector3(mapData.mapWidth / 2, mapData.mapHeight / 2, 0);
        }
        private void Move()
        {
            Vector2 movement = new Vector2(playerInput.Horizontal, playerInput.Vertical);

            playerRb.linearVelocity = movement * playerSpeed;
            // transform.Translate(movement * (playerSpeed * Time.deltaTime));
        }

        void HandleUse(Vector2 mousePosition)
        {
            onCharacterAttack.Raise();
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
