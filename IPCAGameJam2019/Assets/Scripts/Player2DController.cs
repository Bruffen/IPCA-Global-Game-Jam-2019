using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private int health;

    private Vector2 velocity;
    private float maxSpeed;
    private float jumpSpeed;
    private bool grounded;
    private bool secondJump;

    private float attackCooldown;
    private float attackTimer;
    public GameObject FrontAttack;
    private Vector2 frontAttackInitalPos;
    public GameObject UpAttack;
    public GameObject DownAttack;
    public GameObject Bullet;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        health = 10;

        frontAttackInitalPos = FrontAttack.transform.localPosition;

        maxSpeed = 0.15f;
        jumpSpeed = 1.2f;
        attackCooldown = 0.4f;
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
            velocity.y -= Time.deltaTime * (Input.GetAxisRaw("Vertical") < 0.0f ? 3.0f : 1.0f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 9)
        {
            grounded = true;
            secondJump = true;
            velocity.y = 0;
        }
        /*else if (col.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1); //TODO calculate damage
        }*/
    }

    void TakeDamage(int damage)
    {
        health -= damage;
    }

    void HandleAttacks()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackCooldown)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                attackTimer = 0.0f;
                MeleeAttack();
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                attackTimer = 0.0f;
                RangedAttack();
            }
        }
    }

    void MeleeAttack()
    {
        float direction = Input.GetAxisRaw("Vertical");
        if (direction == 0.0f)
        {
            if (sr.flipX)
                FrontAttack.transform.localPosition = new Vector2(-frontAttackInitalPos.x, frontAttackInitalPos.y);
            else
                FrontAttack.transform.localPosition = frontAttackInitalPos;
            FrontAttack.SetActive(true);
        }
        else if (direction > 0.0f)
        {
            UpAttack.SetActive(true);
        }
        else
        {
            DownAttack.SetActive(true);
        }
    }

    void RangedAttack()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector3 spawnPos = this.transform.position;
        if (direction.y == 1.0f && direction.x == 0.0f)
            spawnPos += transform.up;
        else if (direction.y == -1.0f && direction.x == 0.0f)
            spawnPos -= transform.up;
        else
            spawnPos += sr.flipX ? -transform.right : transform.right;

        if (direction == Vector2.zero)
        {
            if (sr.flipX) direction = new Vector2(-1.0f, 0.0f);
            else direction = new Vector2(1.0f, 0.0f);
        }

        GameObject b = Instantiate(Bullet, spawnPos, Quaternion.identity);
        b.GetComponent<BulletController>().Assign(direction);
    }
}
