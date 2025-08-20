using UnityEngine;
using UnityEngine.Serialization;

namespace Prototype_S
{
    /// <summary>
    /// class representing an immutable data container for base item data
    /// </summary>
    public abstract class ItemData : ScriptableObject
    { 
        [Header("Basic Info")] 
       [SerializeField] private new string name = "New Item Name"; 
       [SerializeField] private int itemID;
       [SerializeField] private Sprite icon = null;
       [SerializeField] private int sellPrice = 0;
       [SerializeField] private int quantity = 1;
       [SerializeField] private int maxStack = 1;
       [SerializeField] private string rarity = "New Rarity";
       [SerializeField] private string description = "New Item Description"; 
       [SerializeField] private bool placeable = false;

       [Header("Behaviour")] 
       [SerializeField] private ItemAction itemAction;
       
       //getter properties
       public string Name => name;
       public int ItemID => itemID;
       public Sprite Icon => icon;
       public int SellPrice => sellPrice;
       public int MaxStack => maxStack;
       public int Quantity => quantity;
       public string Rarity => rarity;
       public string Description => description;
       public ItemAction ItemAction => itemAction;
       public bool Placeable => placeable;

       public abstract string GetItemDisplayText();
   }
}
