using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InputProvider : MonoBehaviour
{

    public int ID { get => id; set => id = value; }
    public bool Permission { get => permission; set => permission = value; }

    public float HorizontalInput { get => horizontalInput; set => horizontalInput = value; }
    public float VerticalInput { get => verticalInput; set => verticalInput = value; }
    public bool ShootInput { get => shootInput; set => shootInput = value; }


    [SerializeField] private int id = -1;
    private bool permission = false; 

    protected float horizontalInput = 0f;
    protected float verticalInput = 0f;
    protected bool shootInput = false;

    //Events
    public UnityAction onTurnChangeEvent = null;

    public abstract void OnMove(InputValue value);
    public abstract void OnShoot(InputValue value);
    public void TriggerTurnChangeEvent()
    {
        onTurnChangeEvent?.Invoke();
    }

    public void DelayedTriggerChangeEvent()
    {
      //  StartCoroutine(DelayedTrigger);
    }

    private void DelayedTrigger()
    {

    }
}