using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    public float velocityVertical = 10f;

    private InputProvider input = null;
    private CharacterController characterController = null;

    private void Awake()
    {
        input = GetComponent<InputProvider>();
        characterController = GetComponentInChildren<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(input.VerticalInput != 0)
        {
            characterController.Move(new Vector3(0, input.VerticalInput * velocityVertical * Time.deltaTime, 0));
        }
    }
}
