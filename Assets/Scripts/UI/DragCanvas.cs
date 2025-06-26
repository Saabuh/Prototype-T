using UnityEngine;

namespace Prototype_S
{
    public class DragCanvas : MonoBehaviour
    {
        
        public static DragCanvas Instance { get; private set; }
        public Canvas canvas;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }

            Instance = this;

            if (canvas == null)
            {
                canvas = GetComponent<Canvas>();
            }

        }
    }
}
