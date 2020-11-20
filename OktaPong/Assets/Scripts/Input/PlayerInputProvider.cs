using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputProvider : InputProvider
{
    public override void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 moveValue = ctx.ReadValue<Vector2>();

        horizontalInput = moveValue.x;
        verticalInput = moveValue.y;
    }

    public override void OnShoot(InputAction.CallbackContext ctx)
    {
        shootInput = ctx.ReadValue<float>() > 0.5;
    }

}
