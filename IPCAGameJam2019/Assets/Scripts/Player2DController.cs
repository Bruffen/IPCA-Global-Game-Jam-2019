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

    private float attackCooldown;
    private float attackTimer;
    public GameObject FrontAttack;
    public GameObject UpAttack;
    public GameObject DownAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        maxSpeed = 0.25f;
        jumpSpeed = 1.0f;
        attackCooldown = 1.0f;
        secondJump = true;
    }

    void Update()
    {
        Movement();
        HandleAttacks();
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
        if (col.gameObject.layer == 9)
        {
            grounded = true;
            secondJump = true;
            velocity.y = 0;
        }
    }

    void HandleAttacks()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackCooldown)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Attack1");
                attackTimer = 0.0f;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Attack2");
                attackTimer = 0.0f;
            }
        }
    }
}
