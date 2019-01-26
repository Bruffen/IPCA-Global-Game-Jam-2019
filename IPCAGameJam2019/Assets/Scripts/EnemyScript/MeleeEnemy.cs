using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        farsight = 2;
        knockValue = 0.5f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform == tPlayer)
        {
            knockValeuVelocity += Knockback();

        }

    }


}
