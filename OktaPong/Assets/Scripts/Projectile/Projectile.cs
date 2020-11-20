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
        Vector3 newPosition = transform.right * maxSpeed * Time.deltaTime;

        transform.Translate(newPosition);
    }
}
