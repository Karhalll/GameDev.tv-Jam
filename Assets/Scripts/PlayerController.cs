using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameDevJam.Movement;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [Range(0, .3f)]
    [SerializeField] float moveSmoothing = 0.025f;
    [SerializeField] float jumpForce = 5f;

    CharacterController2D myController;

    float horizontalMove = 0f;

    private void Awake() 
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 144;

        myController = GetComponent<CharacterController2D>(); 
    }

    private void FixedUpdate() 
    {
        Move();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
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
}
