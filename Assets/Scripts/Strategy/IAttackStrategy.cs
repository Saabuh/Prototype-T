using UnityEngine;

namespace Prototype_S
{
    /// <summary>
    /// Interface that defines how an attack behaves.
    /// </summary>
    /// <para>
    /// Each concrete implementation of IAttackStrategy defines how an attack behaves before instantiation.
    /// it is only concerned with how an attack behaves. If the strategy spawns a projectile, any properties of that projectile should
    /// be on the prefab spawned.
    /// </para>
    public interface IAttackStrategy
    {
        void ExecuteAttack(Transform origin);
    }
}