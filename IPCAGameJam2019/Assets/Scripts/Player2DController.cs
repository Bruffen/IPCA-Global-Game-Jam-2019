using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Player2DController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public PostProcessProfile ppp;
    private ColorGrading colorGrading;
    private float dessatureTimer;
    private float maxSaturation = 25.0f;
    private float minSaturation;
    private bool dessature = false;

    private int health;
    private float invulnerabilityTimer;

    private Vector2 velocity;
    private Vector2 knockbackVelocity;
    private float maxSpeed;
    private float jumpSpeed;
    private bool grounded;
    private bool moving;
    private bool secondJump;
    private bool flipped;

    private float attackCooldown;
    private float attackTimer;

    public GameObject FrontAttack;
    private Vector2 frontAttackInitalPos;
    private SpriteRenderer frontAttackSr;
    private Animator frontAttackAnim;

    public GameObject UpAttack;
    private Animator upAttackAnim;

    public GameObject DownAttack;
    private Animator downAttackAnim;

    public GameObject Bullet;

    public AudioSource As;
    public AudioClip tacaoDir, tacaoEsq, swordSound, shoeTrowSound, ladingSound;
    public AudioClip dano1, dano2, dano3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ppp.TryGetSettings(out colorGrading);
        dessatureTimer = 0.0f;
        colorGrading.saturation.value = -50.0f;
        minSaturation = colorGrading.saturation.value;

        health = 10;

        frontAttackInitalPos = FrontAttack.transform.localPosition;
        frontAttackSr = FrontAttack.GetComponent<SpriteRenderer>();
        frontAttackAnim = FrontAttack.GetComponent<Animator>();

        upAttackAnim = UpAttack.GetComponent<Animator>();

        downAttackAnim = DownAttack.GetComponent<Animator>();

        maxSpeed = 0.15f;
        jumpSpeed = 1.2f;
        attackCooldown = 0.4f;
        secondJump = true;
    }

    void Update()
    {
        Movement();
        HandleAttacks();
        SetAnimatorParams();
        if (dessature) Dessature();
        invulnerabilityTimer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.position += velocity * maxSpeed + knockbackVelocity;
        knockbackVelocity *= 0.9f;
    }

    void Movement()
    {
        velocity.x = Input.GetAxis("Horizontal");

        Jump();

        bool flipSprite = (flipped ? (velocity.x > 0.0f) : (velocity.x < 0.0f));
        if (flipSprite)
        {
            flipped = !flipped;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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

    void SetAnimatorParams()
    {
        anim.SetBool("grounded", grounded);
        anim.SetBool("moving", velocity != Vector2.zero);
        anim.SetBool("sideAtk", FrontAttack.activeSelf);
        anim.SetBool("downAtk", DownAttack.activeSelf);
        anim.SetBool("upAtk", UpAttack.activeSelf);
    }

    void TakeDamage(int damage)
    {
        switch (Random.Range(1, 4))
        {
            case 1:
                As.PlayOneShot(dano1);
                break;
            case 2:
                As.PlayOneShot(dano2);
                break;
            case 3:
                As.PlayOneShot(dano3);
                break;
        }


        health -= damage;
    }

    void HandleAttacks()
    {
        DisableAttacks();

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

    void DisableAttacks()
    {
        if (FrontAttack.activeSelf)
        {
            if (frontAttackAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                FrontAttack.SetActive(false);
            }
        }
        if (UpAttack.activeSelf)
        {
            if (upAttackAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                UpAttack.SetActive(false);
            }
        }
        if (DownAttack.activeSelf)
        {
            if (downAttackAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                DownAttack.SetActive(false);
            }
        }
    }

    void MeleeAttack()
    {
        float direction = Input.GetAxisRaw("Vertical");
        As.PlayOneShot(swordSound);
        if (direction == 0.0f)
        {
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
        As.PlayOneShot(shoeTrowSound);
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector3 spawnPos = this.transform.position;
        if (direction.y == 1.0f && direction.x == 0.0f)
            spawnPos += transform.up * 2.5f;
        else if (direction.y == -1.0f && direction.x == 0.0f)
            spawnPos -= transform.up;
        else
            spawnPos += flipped ? -transform.right : transform.right;

        if (direction == Vector2.zero)
        {
            if (flipped) direction = new Vector2(-1.0f, 0.0f);
            else direction = new Vector2(1.0f, 0.0f);
        }

        GameObject b = Instantiate(Bullet, spawnPos, Quaternion.identity);
        b.GetComponent<BulletController>().Assign(direction);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 9)
        {
            if (!grounded) As.PlayOneShot(ladingSound);
            grounded = true;
            secondJump = true;
            velocity.y = 0;
        }
        if (invulnerabilityTimer > 0.5f)
        {
            invulnerabilityTimer = 0.0f;
            if (col.gameObject.CompareTag("Enemy"))
            {
                knockbackVelocity += Knockback(col.gameObject.transform);
                TakeDamage(5);
            }
            if (col.gameObject.CompareTag("BulletEnemy"))
            {
                TakeDamage(1);
                Destroy(col.gameObject);
            }
        }
        //else if (col.gameObject.CompareTag("Enemy"))
        //{
        //  TakeDamage(1); //TODO calculate damage
        //}
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            colorGrading.saturation.value = 10.0f;
            dessature = true;
            if (DownAttack.activeSelf)
                knockbackVelocity += Knockback(col.gameObject.transform);
        }

    }

    private Vector2 Knockback(Transform target)
    {
        return (this.transform.position - target.position) * 0.35f;
    }

    /**Sound section*/
    private void PlayStepDir()
    {
        As.PlayOneShot(tacaoDir);
    }
    private void PlayStepEsq()
    {
        As.PlayOneShot(tacaoEsq);
    }

    private void Dessature()
    {
        dessatureTimer += Time.deltaTime;
        colorGrading.saturation.value = (1 - dessatureTimer) * maxSaturation + dessatureTimer * minSaturation;
        if (dessatureTimer >= 1.0f)
        {
            dessatureTimer = 0.0f;
            dessature = false;
        }
    }
}
