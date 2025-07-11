using UnityEngine;
using UnityEngine.Events;

namespace Prototype_S
{
    public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour,
        IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
    {
        [SerializeField] private E gameEvent = null;
        public E GameEvent { get { return gameEvent; } set { gameEvent = value; } }

        [SerializeField] private UER unityEventResponse = null;

        private void OnEnable()
        {
            if (gameEvent == null) { return; }

            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (gameEvent == null) return;

            GameEvent.UnregisterListener(this);
        }

        /// <summary>
        /// when invoked, must provide a parameter of type T
        /// </summary>
        /// <param name="item"></param>
        public void OnEventRaised(T item)
        {
            if (unityEventResponse != null)
            {
                unityEventResponse.Invoke(item);
            }
        }
    }
}
