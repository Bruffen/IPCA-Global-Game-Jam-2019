﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Vector2 velocity;
    float speed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        speed = 0.3f;
    }

    public void Assign(Vector2 vel)
    {
        velocity = vel;
    }

    void Update()
    {
        StartCoroutine(Life());
    }

    void FixedUpdate()
    {
        rb.position += velocity * speed;
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 9)
            Destroy(this.gameObject);
    }
}
