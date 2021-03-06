﻿using UnityEngine;

using GameDevJam.Controls;

namespace GameDevJam.Movement
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController2D : MonoBehaviour
    {
        [SerializeField] Collider2D groundChecker = null;
        [SerializeField] LayerMask groundLayer = new LayerMask();
        [SerializeField] float gravityScale = 10f;

        bool grounded = false;
        bool isPlayerControlling = false; //get ridof somehow to make it work even for enemies (no keybord control)
        bool isClimbing = false;

        Rigidbody2D myRigidbody;
        Animator myAnimator;
        PlayerController playerController;

        float jumpTimer = 1f;

        private void Awake()
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            myRigidbody = GetComponent<Rigidbody2D>();
            myAnimator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (groundChecker.IsTouchingLayers(groundLayer))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        }

        private void LateUpdate()
        {
            NormalizeSlope();
            if (playerController.IsMovementAllowed())
            {
                isPlayerControlling = (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f) || Input.GetButtonDown("Jump");
                Flip();

            }
            myAnimator.SetFloat("moveSpeed", Mathf.Abs(myRigidbody.velocity.x));

            UpdateJumpTimer();
        }

        public void Move(float moveSpeed, float moveSmoothing)
        {
            Vector2 targetvelocity = new Vector2(moveSpeed * 100, myRigidbody.velocity.y);
            Vector2 velocity = Vector2.zero;

            myRigidbody.velocity = targetvelocity;
        }

        public void Climb(float climSpeed, float moveSmoothing)
        {
            Vector2 targetvelocity = new Vector2( myRigidbody.velocity.x, climSpeed * 100);
            Vector2 velocity = Vector2.zero;

            myRigidbody.velocity = targetvelocity;
        }

        public void Jump(float jumpForce)
        {
            if (grounded)
            {
                jumpTimer = 0f;
                myRigidbody.velocity = myRigidbody.velocity.normalized;
                myRigidbody.AddForce(new Vector2(0f, jumpForce * 100));
            }
        }

        // only working for player now -> depending on keybord input.
        private void Flip()
        {
            if (!playerController.IsMovementAllowed())
            {
                return;
            }

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float inputTollerance = 0.1f;

            if (horizontalInput > inputTollerance)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (horizontalInput < -inputTollerance)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }

        void NormalizeSlope()
        {
            if (grounded && !isClimbing)
            {
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position, 
                    -Vector2.up, 5f,
                    groundLayer
                );  

                if (hit.collider != null && !isPlayerControlling && jumpTimer > 0.3f)
                {
                    myRigidbody.gravityScale = 0f;
                    myRigidbody.velocity = Vector2.zero;
                }
                else
                {
                    myRigidbody.gravityScale = gravityScale;
                }
            }
            else
            {
                myRigidbody.gravityScale = gravityScale;
            }
        }

        public void IsClimbing(bool state)
        {
            isClimbing = state;
        }

        private void UpdateJumpTimer()
        {
            jumpTimer += Time.deltaTime;
        }
    }

}