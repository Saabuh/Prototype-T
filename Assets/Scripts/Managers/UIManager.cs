using System;
using UnityEngine;

namespace Prototype_S
{
    public class UIManager : MonoBehaviour
    {
        
        [SerializeField] private InputReader inputReader;
        [SerializeField] private GameObject inventoryPanel;

        private void OnEnable()
        {
            inputReader.OnInventoryToggle += ToggleInventory;
        }

        private void OnDisable()
        {
            inputReader.OnInventoryToggle -= ToggleInventory;
        }

        private void ToggleInventory()
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
}
