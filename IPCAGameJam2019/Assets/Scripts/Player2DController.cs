using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Vector2 velocity;
    private float maxSpeed;
    private float jumpSpeed;
    private bool grounded;
    private bool secondJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        maxSpeed = 0.25f;
        jumpSpeed = 1.0f;
        secondJump = true;
    }

    void Update()
    {
        Movement();
    }

    void FixedUpdate()
    {
        rb.position += velocity * maxSpeed;
    }

    void Movement()
    {
        velocity.x = Input.GetAxis("Horizontal");

        Jump();

        bool flipSprite = (sr.flipX ? (velocity.x > 0.0f) : (velocity.x < 0.0f));
        if (flipSprite)
        {
            sr.flipX = !sr.flipX;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && (grounded || secondJump))
        {
            velocity.y = jumpSpeed;
            if (grounded == false)
            {
                secondJump = false;
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            }
            else grounded = false;
        }
        else if (velocity.y > 0)
            velocity.y -= Time.deltaTime * (Input.GetAxisRaw("Vertical") < 0.0f ? 2.5f : 1.0f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8)
        {
            grounded = true;
            secondJump = true;
            velocity.y = 0;
        }
    }
}
