using System;
using UnityEngine;

namespace Prototype_S
{
    public class InputReader : MonoBehaviour
    {

        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        public event Action<Vector2> OnFire = delegate { };
        
        // Update is called once per frame
        void Update()
        {
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");

            if (Input.GetButtonDown("Fire1"))
            {
               Fire(); 
            }
        }

        void Fire()
        {
            //get mouse position of click and convert to world point
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            //ping OnFire event for all observers
            OnFire.Invoke(mousePosition);
        }
    }
}
