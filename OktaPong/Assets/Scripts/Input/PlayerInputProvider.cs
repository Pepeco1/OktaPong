using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputProvider : InputProvider
{

    public int ID = -1;


    public override void OnMove(InputValue value)
    {
        Vector2 moveValue = value.Get<Vector2>();

        horizontalInput = moveValue.x;
        verticalInput = moveValue.y;
    }

    public override void OnShoot(InputValue value)
    {
        shootInput = value.Get<float>() > 0.5;
    }
}
