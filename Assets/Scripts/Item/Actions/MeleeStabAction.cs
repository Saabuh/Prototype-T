using UnityEngine;

namespace Prototype_S
{
    [CreateAssetMenu(fileName = "New Melee Stab Action", menuName = "Actions/Melee Stab Action")]
    public class MeleeStabAction : ItemAction
    {
        [SerializeField] private int stabDistance;
        public override void StartUse(GameObject user, ItemInstance itemInstance, Vector3 mousePosition)
        {
            Log.Info("Performing Melee Stab Action with an distance of " + stabDistance);
        }

        public override void OnUse(GameObject user, ItemInstance itemInstance, Vector3 mousePosition)
        {
            throw new System.NotImplementedException();
        }

        public override void EndUse(GameObject user, ItemInstance itemInstance, Vector3 mousePosition)
        {
            throw new System.NotImplementedException();
        }
    }
}