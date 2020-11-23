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
    public new Collider2D collider = null;

    protected void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    protected virtual void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, rotationVelocity * Time.deltaTime));
    }

    protected abstract void Move();

    protected Collider2D CheckOcurringCollision()
    {

        var colliders = new Collider2D[5];
        var filter = new ContactFilter2D();
        if (collider.OverlapCollider(filter.NoFilter(), colliders) >= 1)
            Debug.Log("Colidiu 2");




        Collider2D closestCollider = null;
        float closestHitDistance = Mathf.Infinity;
        foreach(var otherCollider in colliders)
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


        return closestCollider;
    }

    private void GetOffCollision(Collider2D otherCollider)
    {
        var hits = new RaycastHit2D[1];
        collider.Raycast(otherCollider.ClosestPoint(transform.position), hits, MaxSpeed * Time.fixedDeltaTime);

        transform.position +=  (Vector3) hits[0].normal * maxSpeed * Time.fixedDeltaTime;
    }

}
