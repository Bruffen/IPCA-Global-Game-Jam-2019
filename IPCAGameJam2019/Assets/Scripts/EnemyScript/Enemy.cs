using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected int health;
    protected Transform tPlayer;
    protected Vector2 velocity;

    protected float knockValue;
    protected Vector2 knockValeuVelocity;

    protected float farsight;
    protected Rigidbody2D enRigidBody;
    
   

    // Start is called before the first frame update
   protected virtual void Start()
    {
        tPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        velocity = Vector2.zero;
        enRigidBody = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        enRigidBody.position += velocity + knockValeuVelocity;
        knockValeuVelocity *= 0.9f;
    }

    protected virtual Vector2 Knockback()
    {
        Vector2 finalResult;
        finalResult = (this.transform.position - tPlayer.position ) * knockValue;
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
        
    }

    private void takeDamage()
    {

    }
}
