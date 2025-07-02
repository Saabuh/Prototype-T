using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype_S
{
    /// <summary>
    /// single responsibility of managing tooltip state and display info
    /// </summary>
    public class ToolTipUI : MonoBehaviour
    {
        //fields
        [Header("child references")]
        [SerializeField] private TextMeshProUGUI tooltipText;
        [SerializeField] private GameObject toolTipCanvas;
        [SerializeField] private Image toolTipBackground;
        
        //component references
        private ItemSlotEventListener onItemSlotHoverEnter;
        private VoidListener onItemSlotHoverExit;

        void Awake()
        {
            //initialize component references
            onItemSlotHoverEnter = GetComponent<ItemSlotEventListener>();
            onItemSlotHoverExit = GetComponent<VoidListener>();
            
            toolTipCanvas.SetActive(false);
        }
        void Update()
        {
            if (toolTipCanvas.activeSelf)
            {
                toolTipBackground.rectTransform.position = new Vector2(Input.mousePosition.x + 15, Input.mousePosition.y + 15);
            }
        }
        
        public void ShowTooltip(ItemSlot item)
        {
            Debug.Log("Showing Tooltip...");
            
            DisplayToolTipInfo(item);
            toolTipCanvas.SetActive(true);
            
        }

        public void HideTooltip()
        {
            Debug.Log("Hide Tooltip...");
            toolTipCanvas.SetActive(false);
        }

        public void EnableHovers()
        {
            //register listeners
            onItemSlotHoverEnter.enabled = true;
            onItemSlotHoverExit.enabled = true;
        }

        public void DisableHovers()
        {
            //unregister listeners
            onItemSlotHoverEnter.enabled = false;
            onItemSlotHoverExit.enabled = false;
            
            //make sure the tooltip is not showing
            toolTipCanvas.SetActive(false);
        }

        private void DisplayToolTipInfo(ItemSlot item)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<size=24>").Append(item.itemData.name).Append("</size>\n");
            builder.Append(item.itemData.GetItemDisplayText());

            tooltipText.text = builder.ToString();
        }
        
    }
}
