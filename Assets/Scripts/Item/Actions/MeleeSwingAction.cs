using UnityEngine;

namespace Prototype_S
{
    [CreateAssetMenu(fileName = "New Melee Swing Action", menuName = "Actions/Melee Swing Action")]
    public class MeleeSwingAction : ItemAction
    {

        [SerializeField] private int swingArc = 90;
        [SerializeField] private int swingDistance = 15;

        public override void StartUse(GameObject user, ItemData itemData, Vector2 mousePosition)
        {
            Log.Info("Performing Melee Swing Action with an Arc of " + swingArc + " and swing distance of " + swingDistance);
            Log.Info("Performing at mouse position x: " + mousePosition.x + " y: " + mousePosition.y);
        }

        public override void OnUse(GameObject user, ItemData itemData, Vector2 mousePosition)
        {
            throw new System.NotImplementedException();
        }

        public override void EndUse(GameObject user, ItemData itemData, Vector2 mousePosition)
        {
            throw new System.NotImplementedException();
        }
    }
}
