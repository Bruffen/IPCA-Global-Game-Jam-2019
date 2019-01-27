using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        farsight = 8;
        knockValue = 0.25f;
        health = 30;
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void Scheme()
    {
        base.Scheme();
        velocity = ((Vector2)tPlayer.position - (Vector2)transform.position) / 30;
        cooldown = 1;
    }

    protected override void OnTriggerEnter2D(Collider2D col) {
        base.OnTriggerEnter2D(col);

        if (col.gameObject.CompareTag("PlayerAttack") || col.gameObject.CompareTag("Bullet"))
        {
            
            this.gameObject.transform.localScale *= 0.7f;
            GameObject g =Instantiate(this.gameObject);
            if (g.transform.localScale.x < 0.2f) Destroy(g);
            g.GetComponent<MeleeEnemy>().health = this.health;
           
         
        }

    }




}
