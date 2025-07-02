using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class Player : MonoBehaviour
    {

        private List<IPlayerState> _activeStates = new List<IPlayerState>();

        public float playerSpeed = 5.0f;
        public float cooldown = 0f;
        public float cooldownDuration = 3.0f;
        public GameObject projectilePrefab;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _activeStates.Add(new WalkState());
            _activeStates.Add(new StandbyState());
        }

        // Update is called once per frame
        void Update()
        {
            //update all active states (Player, Equipment)
            for (int i = 0; i < _activeStates.Count; i++)
            {
                IPlayerState newState = _activeStates[i].HandleInput();

                if (newState != null)
                {
                    _activeStates[i].Exit();
                    // Log.Info("Exiting " + _activeStates[i].GetType().Name);
                    _activeStates[i] = newState;
                    _activeStates[i].Enter();
                    // Log.Info("Entering " + _activeStates[i].GetType().Name);
                }
                
                _activeStates[i].Update(this);

            }

            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }

        }
    }
}
