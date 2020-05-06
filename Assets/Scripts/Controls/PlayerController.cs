using UnityEngine;

using GameDevJam.Movement;

namespace GameDevJam.Controls
{
    [RequireComponent(typeof(CharacterController2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [Range(0, .3f)]
        [SerializeField] float moveSmoothing = 0.025f;
        [SerializeField] float jumpForce = 5f;

        CharacterController2D myController;

        float horizontalMove = 0f;
        bool isMovementlAllowed = true;

        private void Awake()
        {
            myController = GetComponent<CharacterController2D>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        void Update()
        {
            if (isMovementlAllowed)
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;

                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }
            }
        }

        private void Move()
        {
            myController.Move(horizontalMove * Time.fixedDeltaTime, moveSmoothing);
        }

        private void Jump()
        {
            myController.Jump(jumpForce);
        }

        public void EnableControl()
        {
            isMovementlAllowed = true;
        }

        public void DisableControl()
        {
            isMovementlAllowed = false;
        }

        public bool IsMovementAllowed()
        {
            return isMovementlAllowed;
        }
    }

}