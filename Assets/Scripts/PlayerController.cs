using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;

    [Header("Movement Variables")]
    [SerializeField] float runSpeed = 5;
    [SerializeField] float jumpSpeed = 5;
    [SerializeField] float climbSpeed = 4;
    
    void Start() // Create a reference to our own rigidbody/collider on start.
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    
    void FixedUpdate() // Run our three methods every frame
    {
        Run();
        ClimbLadder();
        FlipSprite();
    }

    void OnMove(InputValue value) { // When pressing the movement key of the input system {
        moveInput = value.Get<Vector2>(); // Get the value of the Input pressed (InputValue value).
    }

    void OnJump(InputValue value) { // When pressing the jump key of the input system.

        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return; // If not touching the ground, exit method to prevent double jumping.
        }

        if (value.isPressed) { // If we press the space key to jump {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed); // apply force to the y value of our players velocity.
        }
    }

    void Run() { // Apply the value of controller input to rigidboy's velocity multiplied by our runSpeed
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y); // Set velocity of players x without changing the y value.
        myRigidbody.velocity = playerVelocity;
    }

    void ClimbLadder() { // if Player is able to climb ladder, do so.
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) { // If not touching the ladder layer, unable to climb.
            myRigidbody.gravityScale = gravityScaleAtStart; // if not climbing a ladder, player gravity is normal
            return;
        }
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f; // if climbing a ladder, gravitiy is 0. To prevent sliding down the ladder.
    }

    void FlipSprite() { // Flip our sprite whenever we are moving in that direction.

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon; // boolean to check if we are moving.

        if (playerHasHorizontalSpeed) { // If we are moving {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f); // transform the local scale of our player to be right or left.
        }
    }
}
