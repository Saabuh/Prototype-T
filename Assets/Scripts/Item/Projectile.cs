using UnityEngine;

namespace Item
{
    public class Projectile : MonoBehaviour
    {

        private float lifetimer;
        private Rigidbody2D rb;

        public float speed = 50.0f;
        public float lifetime = 1.0f;

        public void Initialize(Vector2 direction)
        {
            lifetimer = lifetime;
            rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * speed;
        }

        void Update()
        {
            lifetimer -= Time.deltaTime;
            
            if (lifetimer <= 0)
            {
                Destroy(gameObject);
            }
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
    }
}
