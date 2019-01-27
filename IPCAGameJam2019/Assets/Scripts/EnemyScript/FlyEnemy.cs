﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy
{
    private Vector2 originPos;
    public GameObject Bullet;
    private bool atacknow;
    bool goingRight;
    public AudioClip aC_atk;
    public GameObject spwanPointOnj;


    protected override void Start()
    {
        base.Start();
        farsight = 12;
        knockValue = 0.25f;
        health = 20;
        originPos = this.transform.position;
        goingRight = true;
        StartCoroutine(Changedi());
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        velocity += (goingRight ? Vector2.right*4 : Vector2.left*4) * 0.01f;

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
        aS.PlayOneShot(aC_atk);
        Vector3 spawnPos = spwanPointOnj.transform.position;
        GameObject b = Instantiate(Bullet, spawnPos, Quaternion.identity);
        GameObject c = Instantiate(Bullet, spawnPos, Quaternion.identity);
        GameObject d = Instantiate(Bullet, spawnPos, Quaternion.identity);
        Vector2 velocity = new Vector2(tPlayer.transform.position.x - enRigidBody.position.x, tPlayer.transform.position.y - enRigidBody.position.y);
        velocity.Normalize();
        b.GetComponent<BulletController>().Assign(velocity);
        c.GetComponent<BulletController>().Assign(velocity-Vector2.one);
        d.GetComponent<BulletController>().Assign(velocity-Vector2.one*2);
    }
    protected override Vector2 Knockback()
    {
        originPos = this.transform.position;
        return base.Knockback();

    }

    private IEnumerator Changedi()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);
            goingRight = !goingRight;
        }

    }
}
