using UnityEngine;

namespace Prototype_S
{
    [CreateAssetMenu(fileName = "New Mining Action", menuName = "Actions/Mining Action")]
    public class MiningAction : ItemAction
    {

        [SerializeField] private int miningDistance = 5;
        [SerializeField] private int miningSpeed = 2;

        public override void StartUse(GameObject user, ItemData itemData, Vector2 mousePosition)
        {
            Log.Info("Performing mining action with a mining distance of " + miningDistance + " and a mining speed of " + miningSpeed);
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
