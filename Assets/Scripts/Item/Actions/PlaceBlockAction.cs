using UnityEngine;

namespace Prototype_S
{
    [CreateAssetMenu(fileName = "New Place Block Action", menuName = "Actions/Place Block Action")]
    public class PlaceBlockAction : ItemAction
    {

        [SerializeField] private int placeBlockMaxRange = 5;

        public override void StartUse(GameObject user, ItemData itemData, Vector2 mousePosition)
        {
            Log.Info("Performing Place Block Action with a max range of " + placeBlockMaxRange);
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