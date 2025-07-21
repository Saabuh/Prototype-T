using System;
using UnityEngine;

namespace Prototype_S
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryCanvas;

        public void ToggleInventory()
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        }
    }
}
