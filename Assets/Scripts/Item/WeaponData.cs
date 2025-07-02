using System.Text;
using UnityEngine;

namespace Prototype_S
{
    /*
     * Responsible for any kind of weapon config applied to a weapon.
     */
    [CreateAssetMenu(fileName = "New Weapon Config", menuName = "Item/Weapon")]
    public class WeaponData : ItemData
    {
        [Header("Weapon Info")]
        [SerializeField] private int manaCost = 0;
        [SerializeField] private float cooldown = 0;
        [SerializeField] private float damage = 0;
        [SerializeField] private IAttackStrategy _attackStrategy;
        [SerializeField] private GameObject projectilePrefab;

        //getters
        public IAttackStrategy AttackStrategy => _attackStrategy;

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