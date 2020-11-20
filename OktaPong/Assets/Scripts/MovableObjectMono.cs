using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovableObjectMono : MonoBehaviour
{

    [SerializeField] protected Vector2 movement = new Vector2(10f, 10f);
    [SerializeField] protected float rotationVelocity = 100f;

    protected virtual void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, rotationVelocity * Time.deltaTime));
    }

    protected abstract void Move();

}
