using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputProvider : MonoBehaviour, Controls.IGameplayActions
{
    public float HorizontalInput { get => horizontalInput; set => horizontalInput = value; }
    public float VerticalInput { get => verticalInput; set => verticalInput = value; }
    public bool ShootInput { get => shootInput; set => shootInput = value; }


    protected float horizontalInput = 0f;
    protected float verticalInput = 0f;
    protected bool shootInput = false;


    public abstract void OnMove(InputAction.CallbackContext ctx);
    public abstract void OnShoot(InputAction.CallbackContext ctx);
}