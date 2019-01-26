using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        farsight = 5;
        knockValue = 0.25f;
        health = 20;
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void Scheme()
    {
        base.Scheme();
        velocity = ((Vector2)tPlayer.position - (Vector2)transform.position) / 10;
        cooldown = 2;
    }

    

}
