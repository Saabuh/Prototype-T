using UnityEngine;

namespace Prototype_S
{
    [CreateAssetMenu(fileName = "New VoidEvent" ,menuName = "GameEvents/VoidEvent")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Raise()
        {
            Raise(new Void());
        }
    }
}
