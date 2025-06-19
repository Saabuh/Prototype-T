using UnityEngine;

namespace Player
{
    /**
     * Equipment State: Standby
     */
    public class StandbyState : IPlayerState
    {
        public IPlayerState HandleInput()
        {

            if (Input.GetButtonDown("Fire1"))
            {
                //change to attack state
                return new AttackState();
            }

            //stay in current state if held down
            return null;

        }

        public void Update(Player player)
        {
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}
