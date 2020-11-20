using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovableObjectMono : MonoBehaviour
{

    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float rotationVelocity = 100f;

    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float RotationVelocity { get => rotationVelocity; set => rotationVelocity = value; }

    protected virtual void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, rotationVelocity * Time.deltaTime));
    }

    protected abstract void Move();

}
