using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI; // Required for the Button class

namespace Prototype_S
{

    public class NetworkManagerUI : MonoBehaviour
    {
        [SerializeField] private Button hostButton;
        [SerializeField] private Button serverButton;
        [SerializeField] private Button clientButton;

        private void Awake()
        {
            // Add listeners to the buttons to call the correct methods when clicked
            hostButton.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartHost();
            });

            serverButton.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartServer();
            });

            clientButton.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartClient();
            });
        }

        // A simple method to hide the buttons panel after a choice is made
        private void HideButtons()
        {
            // This will disable the GameObject this script is attached to.
            // If you have a separate panel for the buttons, you can disable that instead.
            gameObject.SetActive(false);
        }
    }
}
