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
        knockValue = 0.25f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerAttack"))
        {
            knockVelocity += Knockback();
        }
    }
}
