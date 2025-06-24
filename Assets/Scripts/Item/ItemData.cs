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
       
       //getters
       public string Name => name;
       public Sprite Icon => icon;
       public int SellPrice => sellPrice;
       public int MaxStack => maxStack;

       public abstract string GetItemDisplayText();
   }
}
