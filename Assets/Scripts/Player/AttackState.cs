using Item;
using Prototype_S;
using UnityEngine;

namespace Player
{
    /**
     * Equipment State: Attacking
     */
    [System.Serializable]
    public class AttackState : IPlayerState
    {
        
        public IPlayerState HandleInput()
        {
            if (Input.GetButtonUp("Fire1"))
            {
                //change to standby state
                return new StandbyState();
            }
            //stay in current state
            return null;
        }

        public void Update(Player player)
        {
            if (player.cooldown <= 0)
            {
                Fire(player);
                // Log.Info("Fire");
                player.cooldown = player.cooldownDuration;
            }     
        }

        public void Fire(Player player)
        {

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)player.transform.position).normalized;
            
            //debug
            Log.Info("Direction: " + direction);

            GameObject projectile =
                Object.Instantiate(player.projectilePrefab, player.transform.position, Quaternion.identity);
            
            projectile.GetComponent<Projectile>().Initialize(direction);

        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}