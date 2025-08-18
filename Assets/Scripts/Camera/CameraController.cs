using UnityEngine;

namespace Prototype_S
{
    public class CameraController : MonoBehaviour
    {

        public Transform player;
        [SerializeField] private Vector3 offset;

        void LateUpdate()
        {
            if (player != null)
            {
                transform.position = player.position + offset;
            }
        }
        
    }
}
