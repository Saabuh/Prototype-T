using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype_S
{
    public class ToolTipUI : MonoBehaviour
    {
        [Header("child references")]
        [SerializeField] private TextMeshProUGUI tooltipText;
        [SerializeField] private GameObject toolTipCanvas;
        [SerializeField] private Image toolTipBackground;

        void Awake()
        {
            toolTipCanvas.SetActive(false);
        }
        void Update()
        { 
            // toolTipBackground.rectTransform.position = Input.mousePosition;
            toolTipBackground.rectTransform.position = new Vector2(Input.mousePosition.x + 15, Input.mousePosition.y + 15);
        }
        
        public void ShowTooltip(ItemSlot item)
        {
            Debug.Log("Showing Tooltip...");
            
            //make the tooltip follow mouse position
            toolTipCanvas.SetActive(true);
            
        }

        public void HideTooltip()
        {
            Debug.Log("Hide Tooltip...");
            toolTipCanvas.SetActive(false);
        }
    }
}
