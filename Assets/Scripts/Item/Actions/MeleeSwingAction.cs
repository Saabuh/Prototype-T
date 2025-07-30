using UnityEngine;

namespace Prototype_S
{
    [CreateAssetMenu(fileName = "New Melee Swing Action", menuName = "Actions/Melee Swing Action")]
    public class MeleeSwingAction : ItemAction
    {

        [SerializeField] private int swingArc = 90;
        [SerializeField] private int swingDistance = 15;

        public override void StartUse(GameObject user, ItemData itemData)
        {
            Log.Info("Performing Melee Swing Action with an Arc of " + swingArc + " and swing distance of " + swingDistance);
        }

        public override void OnUse(GameObject user, ItemData itemData)
        {
            throw new System.NotImplementedException();
        }

        public override void EndUse(GameObject user, ItemData itemData)
        {
            throw new System.NotImplementedException();
        }
    }
}
