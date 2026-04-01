using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {
        [SerializeField] private Bullet bullet;
        [SerializeField] private float speed = 1;
        [SerializeField] private float rotationSpeed = 1;
        [SerializeField] private SpriteRenderer circle;
        private Vector2 movement;

        private void Update()
        {
            if (movement.magnitude > 0)
            {
                circle.transform.Translate(movement, Space.World);
            }                                     
        }

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            movement = virtualMoveDirection * Time.deltaTime * speed;
            //circle.transform.up = virtualMoveDirection;
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            if(virtualLookDirection != Vector2.zero)
            {
                circle.transform.up = new Vector2(virtualLookDirection.x, -virtualLookDirection.y);
                Instantiate(bullet, circle.transform.position + circle.transform.up, circle.transform.rotation);

            }
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            circle.color = Color.black;
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            circle.color = Color.blue;
        }
        
    }

}
