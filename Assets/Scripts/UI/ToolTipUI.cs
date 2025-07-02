using System;
using System.Text;
using TMPro;
using Unity.VisualScripting;
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
        
        //singleton
        public static ToolTipUI Instance { get; private set; } 
        
        void Awake()
        {
            //initialize component references
            onItemSlotHoverEnter = GetComponent<ItemSlotEventListener>();
            onItemSlotHoverExit = GetComponent<VoidListener>();
            
            //initialize singleton
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }

            Instance = this; 
            
            toolTipCanvas.SetActive(false);
        }
        void Update()
        {
            if (toolTipCanvas.activeSelf)
            {
                toolTipBackground.rectTransform.position = new Vector2(Input.mousePosition.x + 15, Input.mousePosition.y + 15);
            }
        }
        public void Show()
        {
            Log.Info("Show Tooltip....");
            
            //redraw tooltip position before showing
            toolTipBackground.rectTransform.position = new Vector2(Input.mousePosition.x + 15, Input.mousePosition.y + 15);
            toolTipCanvas.SetActive(true);
        }

        public void Hide()
        {
            Log.Info("Hide Tooltip...");
            toolTipCanvas.SetActive(false);
        }

        /// <summary>
        /// Reregisters event listeners for hovering an item slot
        /// </summary>
        /// <param name="targetFound">return true if a target is found on the pointer when reenabling hover events/param>
        public void EnableHovers(bool targetFound)
        {
            //register listeners
            onItemSlotHoverEnter.enabled = true;
            onItemSlotHoverExit.enabled = true;

            //show or hide a tooltip depending on if the pointer is currently hovering something
            if (targetFound)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
        
        public void DisableHovers()
        {
            //unregister listeners
            onItemSlotHoverEnter.enabled = false;
            onItemSlotHoverExit.enabled = false;
            
            //make sure the tooltip is not showing
            Hide();
        }

        public void PopulateTooltipInfo(ItemSlot item)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<size=24>").Append(item.itemData.name).Append("</size>\n");
            builder.Append(item.itemData.GetItemDisplayText());

            tooltipText.text = builder.ToString();
        }
        
    }
}
