using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MovableObjectMono
{

    private InputProvider input = null;
    private CharacterController characterController = null;

    private void Awake()
    {
        input = GetComponent<InputProvider>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    protected override void Move()
    {
        if (input.VerticalInput != 0)
        {
            characterController.Move(new Vector3(0, input.VerticalInput * maxSpeed * Time.deltaTime, 0));
        }
    }

    protected override void Rotate()
    {
        if (input.HorizontalInput != 0)
        {
            transform.Rotate(new Vector3(0, 0, -input.HorizontalInput * rotationVelocity * Time.deltaTime));
        }
    }
}
