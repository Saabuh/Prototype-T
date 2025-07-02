using System.Text;
using UnityEditor;
using UnityEngine;

namespace Prototype_S
{
    /*
     * responsible for managing interactions and coordinating components.
     */
    public class Weapon : MonoBehaviour, IUseable
    {

        [SerializeField] private WeaponData weaponData;
        [SerializeField] private IAttackStrategy attackStrategy;

        public Weapon(WeaponData weaponData)
        {
            this.weaponData = weaponData;
            attackStrategy = weaponData.AttackStrategy;
        }
        
        public void Use()
        {
            Log.Info("Weapon has been used.");
        }
    }
}