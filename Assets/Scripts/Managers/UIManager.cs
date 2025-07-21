using System;
using UnityEngine;

namespace Prototype_S
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryCanvas;


        private void Start()
        {
            //disable ui on start
            inventoryCanvas.SetActive(false);
        }

        public void ToggleInventory()
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        }
    }
}
