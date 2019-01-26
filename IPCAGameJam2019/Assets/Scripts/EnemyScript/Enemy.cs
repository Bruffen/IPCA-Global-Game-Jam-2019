using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected int health;
    protected Transform tPlayer;
    protected Vector2 velocity;

    protected float knockValue;
    protected Vector2 knockVelocity;

    protected float farsight;
    protected Rigidbody2D enRigidBody;

    protected float cooldown;
    protected Animator animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        tPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        velocity = Vector2.zero;
        enRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = 10;
        cooldown = 0;

    }

    protected virtual void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0) cooldown = 0;
        if (cooldown == 0)
        {
            AiBehaviour();
        }
    }

    protected virtual void FixedUpdate()
    {
        facelayer();
        enRigidBody.position += velocity + knockVelocity;
        knockVelocity *= 0.9f;
        velocity *= 0.9f;
    }

    protected virtual Vector2 Knockback()
    {
        Vector2 finalResult;
        finalResult = (this.transform.position - tPlayer.position) * knockValue;
        return finalResult;
    }

    protected virtual bool SensePlayer()
    {
        if (Vector2.Distance(tPlayer.position, this.transform.position) > farsight) return false;
        else return true;
    }

    /***/
    protected void AiBehaviour()
    {
        if (SensePlayer() == true)
        {
            Scheme();
        }
       
    }

    private void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GetComponent<BoxCollider2D>().isTrigger=true;
            StartCoroutine(BulletDestroy());
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerAttack"))
        {
            knockVelocity += Knockback();
            takeDamage(10);
        }
        if (col.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("A saude do enemy" + health);
            takeDamage(5);
        }
    }

    IEnumerator BulletDestroy()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }

    protected virtual void Scheme()
    {
        //TODO Flip trought player

    }

    protected void facelayer() {
        if (tPlayer.position.x > transform.position.x)
        {
            //face right
            transform.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (tPlayer.position.x < transform.position.x)
        {
            transform.GetComponent<SpriteRenderer>().flipX = true;

        }
    }
}
