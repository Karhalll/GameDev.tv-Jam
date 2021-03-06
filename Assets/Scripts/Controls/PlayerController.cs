﻿using UnityEngine;

using GameDevJam.Movement;

namespace GameDevJam.Controls
{
    [RequireComponent(typeof(CharacterController2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [Range(0, .3f)]
        [SerializeField] float moveSmoothing = 0.025f;
        [SerializeField] float climbSpeed = 2f;
        [SerializeField] LayerMask climbLayer = new LayerMask();
        [SerializeField] float jumpForce = 5f;

        [SerializeField] Collider2D playerCollider = null;

        CharacterController2D myController;

        float horizontalMove = 0f;
        float verticalMove = 0f;
        bool isMovementlAllowed = true;

        private void Awake()
        {
            myController = GetComponent<CharacterController2D>();
        }

        private void FixedUpdate()
        {   
            if (!isMovementlAllowed)
            {
                horizontalMove = 0f;
            }
            else
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
            }

            if (playerCollider.IsTouchingLayers(climbLayer) && Input.GetButton("Vertical"))
            {
                if (!isMovementlAllowed)
                {
                verticalMove = 0f;
                }
                else
                {
                verticalMove = Input.GetAxisRaw("Vertical") * climbSpeed;
                }
                myController.IsClimbing(true);
            }
            else
            {
                verticalMove = 0f;
                myController.IsClimbing(false);
            }

            Move();

            if (isMovementlAllowed)
            {
                if (Input.GetButton("Jump"))
                {
                    //print("Space press");
                    Jump();
                }
            }

            if (verticalMove >= 0.1f)
            {
                Climb();
            }
        }

        private void Move()
        {
            myController.Move(horizontalMove * Time.fixedDeltaTime, moveSmoothing);
        }

        private void Climb()
        {
            myController.Climb(verticalMove * Time.fixedDeltaTime, moveSmoothing);
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