using System.Text;
using UnityEngine;

namespace Prototype_S
{
    
    [CreateAssetMenu(fileName = "New Etc Item Config", menuName = "Item/Etc")]
    public class EtcData : ItemData
   {
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
