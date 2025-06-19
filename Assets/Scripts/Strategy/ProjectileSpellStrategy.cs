using UnityEngine;

namespace Prototype_S
{
    /// <summary>
    /// strictly defines how a projectile spell is performed, not the spell's visuals.
    /// </summary>
    public class ProjectileSpellStrategy : ScriptableObject, IAttackStrategy
    {
    public GameObject projectilePrefab;

    public void ExecuteAttack(Transform origin)
    {
        
    }
    }
}