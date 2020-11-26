using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovableObjectMono : MonoBehaviour
{

    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float RotationVelocity { get => rotationVelocity; set => rotationVelocity = value; }

    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float rotationVelocity = 100f;
    private Vector2 direction = Vector2.right;

    //Members
    [HideInInspector] protected new Collider2D collider = null;

    protected void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    protected virtual void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, rotationVelocity * Time.fixedDeltaTime));
    }

    protected abstract void Move();

    protected Collider2D CheckOcurringCollision()
    {

        
        var collidersHit = new Collider2D[10];
        Cast(transform.forward * maxSpeed * Time.fixedDeltaTime * 100000, collidersHit);

        Collider2D closestCollider = null;
        float closestHitDistance = Mathf.Infinity;
        foreach(var otherCollider in collidersHit)
        {
            if(otherCollider != null)
            {
                var hitPoint = otherCollider.ClosestPoint(transform.position);

                var hitDistance = Vector2.Distance(transform.position, hitPoint);
                if (hitDistance < closestHitDistance)
                {
                    closestHitDistance = hitDistance;
                    closestCollider = otherCollider;
                }      
            }
        }

        //if (closestCollider != null)
        //    GetOffCollision(closestCollider);

        return closestCollider;
    }

    private void GetOffCollision(Collider2D otherCollider)
    {
        var hits = new RaycastHit2D[1];
        var dir = (Vector3) otherCollider.ClosestPoint(transform.position) - transform.position;
        collider.Raycast(dir, hits, MaxSpeed * Time.fixedDeltaTime);


        transform.position += (Vector3) hits[0].normal * maxSpeed * Time.fixedDeltaTime * 1.5f;
        Physics2D.SyncTransforms();


    }

    private int Cast(Vector3 direction, Collider2D[] result)
    {

        transform.position += direction;
        Physics2D.SyncTransforms();

        var filter = new ContactFilter2D();
        int collidersOverlaped = collider.OverlapCollider(filter.NoFilter(), result);

        transform.position -= direction;
        Physics2D.SyncTransforms();
        return collidersOverlaped;
    }

}
