using UnityEngine;

namespace Prototype_S
{
    public class DebugManager : MonoBehaviour
    {

        [SerializeField] private KeyCode toggleLogging = KeyCode.Semicolon;
        
        void Update()
        {
            if (Input.GetKeyDown(toggleLogging))
            {
                Log.IsEnabled = !Log.IsEnabled;

                if (Log.IsEnabled)
                {
                    Debug.Log("Logging has been ENABLED");
                }
                else
                {
                    Debug.Log("Logging has been DISABLED");
                }
            }
        }
    }
}
