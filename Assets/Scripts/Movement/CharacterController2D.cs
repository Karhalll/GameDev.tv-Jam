using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        Rigidbody2D myRigidbody;
        Animator myAnimator;

        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
            myAnimator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            grounded = false;

            if (groundChecker.IsTouchingLayers(groundLayer))
            {
                grounded = true;
            }
        }

        private void Update()
        {
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

        private void Flip()
        {
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            if (playerHasHorizontalSpeed)
            {
                transform.localScale = new Vector3(Mathf.Sign(myRigidbody.velocity.x), 1f, 1f);
            }
        }
    }

}