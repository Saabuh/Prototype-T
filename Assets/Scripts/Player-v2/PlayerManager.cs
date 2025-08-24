using Unity.Netcode;
using UnityEngine;

namespace Prototype_S
{
    public class PlayerManager : NetworkBehaviour
    {
        [SerializeField] private GameObject playerInventoryCanvas;
        
        public override void OnNetworkSpawn()
        {
            //disable the per-player inventory ui if the client is not the owner of this networkObject

            if (IsOwner)
            {
                Log.Info("NetworkObject " + NetworkObjectId + " connected. You are the Owner.");
            }
            else
            {
                Log.Info("NetworkObject " + NetworkObjectId + " connected. You are NOT the Owner.");
            }
            
            playerInventoryCanvas.SetActive(IsOwner);
        }
    }
}
