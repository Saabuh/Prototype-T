using UnityEngine;

namespace Prototype_S
{
    public abstract class ItemData : ScriptableObject
   {
       [Header("Basic Info")] 
       [SerializeField] private new string name = "New Item Name";
       [SerializeField] private Sprite icon = null;
       [SerializeField] private int sellPrice = 0;
       [SerializeField] private int quantity = 1;
       [SerializeField] private int maxStack = 1;
       [SerializeField] private string rarity = "New Rarity";
       [SerializeField] private string description = "New Item Description";

       [Header("Behaviour")] 
       [SerializeField] private ItemAction itemAction;
       
       //getter properties
       public string Name => name;
       public Sprite Icon => icon;
       public int SellPrice => sellPrice;
       public int MaxStack => maxStack;
       public int Quantity => quantity;
       public string Rarity => rarity;
       public string Description => description;
       public ItemAction ItemAction => itemAction;

       public abstract string GetItemDisplayText();
   }
}
