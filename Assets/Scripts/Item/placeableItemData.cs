using System.Text;
using UnityEngine;

namespace Prototype_S
{
    [CreateAssetMenu(fileName = "New Useable Item ", menuName = "Item/UseableItem")]
    public class placeableItemData : ItemData
    {

        [SerializeField] private DestructibleTile tileRef;
        
        //getter properties
        public DestructibleTile TileRef => tileRef;
        
       public override string GetItemDisplayText()
       {
           StringBuilder builder = new StringBuilder();
           
           builder.Append(Rarity).AppendLine();
           builder.Append("Max Stack: ").Append(MaxStack).AppendLine();
           builder.Append("Sell Price: ").Append(SellPrice).Append(" Gold\n");
           builder.Append(Description);
           
           return builder.ToString();

       }
    }
}