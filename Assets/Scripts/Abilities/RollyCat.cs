using UnityEngine.InputSystem;
using UnityEngine;

public class RollyCat : PlayerMovement
{
    public float rotateAmt = 2f;

    public override void OnMove(InputAction.CallbackContext ctx)
    {
        base.OnMove(ctx);
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Vector3 movementDirection = new Vector3(movementInput.x, 0, movementInput.y);

        if (movementDirection.magnitude > .05)
        {
            visuals.transform.Rotate(transform.right, rotateAmt);
        }
    }
}
