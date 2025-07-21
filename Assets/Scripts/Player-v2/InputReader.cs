using System;
using UnityEngine;

namespace Prototype_S
{
    public class InputReader : MonoBehaviour
    {
        
        //events
        [Header("Custom Event References")]
        [SerializeField] private IntegerEvent onHotbarSelect;
        [SerializeField] private Vector2Event onLeftClick;
        [SerializeField] private VoidEvent onInventoryUIToggle;

        //properties
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        //refactor to customEventSystem later
        // public event Action<Vector2> OnFire = delegate { };


        // Update is called once per frame
        void Update()
        {
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");

            if (Input.GetButtonDown("Fire1"))
            {
               Use(); 
            }

            //refactor
            if (Input.GetKeyDown(KeyCode.I))
            {
                onInventoryUIToggle.Raise();
            }

            CheckHotbarInput();
        }

        private void CheckHotbarInput()
        {
            for (int i = 0; i <= 9; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                {
                    onHotbarSelect.Raise(i);
                }
            }
        }

        void Use()
        {
            //get mouse position of click and convert to world point
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
            onLeftClick.Raise(mousePosition);
        }
    }
}
