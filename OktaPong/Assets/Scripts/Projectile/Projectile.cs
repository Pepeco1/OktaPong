using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MovableObjectMono
{

    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        var newPosition = transform.right * maxSpeed * Time.deltaTime;
        transform.position += newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bounce(collision.GetContact(0).normal);
    }

    private void Bounce(Vector2 collisionNormal)
    {
        var newDirection = Vector2.Reflect(transform.right, collisionNormal);
        transform.right = newDirection;
    }
}
