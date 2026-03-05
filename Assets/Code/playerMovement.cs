using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float groundCheckDistance = 0.12f;
    public Vector2 groundCheckOffset = new Vector2(0f, -0.5f);
    public LayerMask groundLayer;
    private bool isFacingRight = true;

    public bool canDash = true;
    public bool isDashing;
    private float dashingPower = 18f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.15f;
    private bool hasBeenInAir = false;
    private float nextCooldown;

    public TrailRenderer tr;

    public Rigidbody2D rb;
    private float horizInput;
    private bool isGrounded;
    private Animator animator;

    public float fallMult = 2.5f;
    public float smallJumpMult = 2f;
    private float gravity;
    public float jumpDelay = 0.25f;
    private float jumpTimer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravity = rb.gravityScale;
    }

    void Update()
    {

        if (isDashing)
        {
            return;
        }

        horizInput = Input.GetAxis("Horizontal");

        Flip();

        Vector2 rayOrigin = groundCheck != null ? (Vector2)groundCheck.position : (Vector2)transform.position + groundCheckOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }

        if (Input.GetButtonDown("Dash") && canDash)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            Vector2 dashDir = new Vector2(x, y);

            if (dashDir == Vector2.zero)
                dashDir = new Vector2(transform.localScale.x, 0);

            hasBeenInAir = false;
            nextCooldown = Time.time + dashingCooldown;

            StartCoroutine(Dash(dashDir));
        }

        //Update animator parameters
        if (animator != null) 
        {
            animator.SetFloat("moveInput", Mathf.Abs(horizInput));
            animator.SetBool("isGrounded", isGrounded);
        }

        if (!isGrounded)
        {
            hasBeenInAir = true;
        }

        if (isGrounded && Time.time >= nextCooldown)
        {
            if (hasBeenInAir || rb.linearVelocity.y == 0)
            {
                canDash = true;
            }
        }
    }

    void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = gravity * fallMult;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = gravity * smallJumpMult;
        }
        else
        {
            rb.gravityScale = gravity;
        }

        rb.linearVelocity = new Vector2(horizInput * speed, rb.linearVelocity.y);

        if(jumpTimer > Time.time && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpTimer = 0;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
        else
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + (Vector3)groundCheckOffset, transform.position + (Vector3)groundCheckOffset + Vector3.down * groundCheckDistance);
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizInput < 0f || !isFacingRight && horizInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private IEnumerator Dash(Vector2 direction)
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = direction.normalized * dashingPower;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = originalGravity;
        isDashing = false;
    }
}