using UnityEngine;
using UnityEngine.Events;

public class Projectile : MovableObjectMono
{

    public UnityAction onCollide = null;

    #region Unity functions
    private void FixedUpdate()
    {
        Move();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Bounce(collision.GetContact(0).normal);

        onCollide?.Invoke();
    }
    #endregion

    protected override void Move()
    {
        var newPosition = transform.right * MaxSpeed * Time.deltaTime;
        transform.position += newPosition;
    }

    private void Bounce(Vector2 collisionNormal)
    {
        var newDirection = Vector2.Reflect(transform.right, collisionNormal);
        transform.right = newDirection;
    }

}