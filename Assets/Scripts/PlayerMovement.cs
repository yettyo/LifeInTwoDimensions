using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D ccol;
    BoxCollider2D bcol;
    float gravityScaleAtStart;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ccol = GetComponent<CapsuleCollider2D>();
        bcol = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
        //Debug.Log(moveInput);
    }

    void OnJump(InputValue value) {
        if (!bcol.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (value.isPressed) {
            rb.velocity += new Vector2(0f, jumpSpeed);
            //Debug.Log("Jump");
        }
    }

    void Run() {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        anim.SetBool("isRunning", playerHasHorizontalSpeed);
    }
    
    void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    void ClimbLadder() {
        if(!bcol.IsTouchingLayers(LayerMask.GetMask("Climbing"))) { 
            rb.gravityScale = gravityScaleAtStart;
            anim.SetBool("isClimbing", false);
            return; 
        }
        Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
        rb.velocity = climbVelocity;
        rb.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        anim.SetBool("isClimbing", playerHasVerticalSpeed);
    }
}
