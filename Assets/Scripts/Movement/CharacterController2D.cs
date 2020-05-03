﻿using UnityEngine;

namespace GameDevJam.Movement
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController2D : MonoBehaviour
    {
        [SerializeField] Collider2D groundChecker = null;
        [SerializeField] LayerMask groundLayer = new LayerMask();

        bool grounded = false;
        bool isJumping = false;
        bool isPlayerControlling = false; //get ridof somehow to make it work even for enemies (no keybord control)

        Rigidbody2D myRigidbody;
        Animator myAnimator;

        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
            myAnimator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (groundChecker.IsTouchingLayers(groundLayer))
            {
                print("Grounded");
                grounded = true;
            }
            else
            {
                print("Not Grounded");
                grounded = false;
            }

            NormalizeSlope();    
        }

        private void Update()
        {
            isPlayerControlling = (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f) || Input.GetButtonDown("Jump");
            Flip();

            myAnimator.SetFloat("moveSpeed", Mathf.Abs(myRigidbody.velocity.x));
        }

        public void Move(float moveSpeed, float moveSmoothing)
        {
            Vector2 targetvelocity = new Vector2(moveSpeed * 100, myRigidbody.velocity.y);
            Vector2 velocity = Vector2.zero;

            myRigidbody.velocity = Vector2.SmoothDamp(myRigidbody.velocity, targetvelocity, ref velocity, moveSmoothing);
        }

        public void Jump(float jumpForce)
        {
            if (grounded)
            {
                myRigidbody.AddForce(new Vector2(0f, jumpForce * 100));
                isJumping = true;
            }
            else
            {
                isJumping = false;
            }
        }

        public bool GetIsJumping()
        {
            return isJumping;
        }

        // only working for player now -> depending on keybord input.
        private void Flip()
        {
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
            if (grounded)
            {
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position, 
                    -Vector2.up, 5f,
                    groundLayer
                );

                if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.01f  && !isPlayerControlling)
                {
                    myRigidbody.gravityScale = 0f;
                    myRigidbody.velocity = Vector2.zero;
                }
                else
                {
                    myRigidbody.gravityScale = 10f;
                }
            }
            else
            {
                myRigidbody.gravityScale = 10f;
            }
        }
    }

}