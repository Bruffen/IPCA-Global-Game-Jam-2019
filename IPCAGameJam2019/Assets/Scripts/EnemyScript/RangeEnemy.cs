using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    private Vector2 originPos;
    public GameObject Bullet;
    private bool atacknow;
    protected override void Start()
    {
        base.Start();
        farsight = 8;
        knockValue = 0.25f;
        health = 20;
        atacknow = true;
    }

    protected override void Update() {
        base.Update();
        if(atacknow)
        transform.position = Vector2.Lerp(originPos + Vector2.left, originPos + Vector2.right, Mathf.Abs(Mathf.Cos(Time.time)));
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (SensePlayer())
        {
            animator.SetBool("PlayerNear", true);
        }
        else animator.SetBool("PlayerNear", false);
    }
    protected override void Scheme()
    {
        base.Scheme();
        Shot();
        cooldown = 5;

    }
    private void Shot()
    {
        Vector3 spawnPos = this.transform.position;
        GameObject b = Instantiate(Bullet, spawnPos, Quaternion.identity);
        Vector2 velocity = new Vector2(tPlayer.transform.position.x - enRigidBody.position.x, tPlayer.transform.position.y - enRigidBody.position.y);
        velocity.Normalize();
        b.GetComponent<BulletController>().Assign(velocity);
    }
}
