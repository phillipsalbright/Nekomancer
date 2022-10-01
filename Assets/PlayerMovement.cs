using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput = new Vector2(0, 0);
    private Vector2 mouseInput = new Vector2(0, 0);
    private bool jumpPressed = false;
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float turnSmoothTime = .1f;
    float turnSmoothVelocity;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform camVals;
    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>().normalized;
    }

    public void OnMouse(InputAction.CallbackContext ctx)
    {
        mouseInput = ctx.ReadValue<Vector2>().normalized;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.action.triggered)
        {
            jumpPressed = true;
        }
        else
        {
            jumpPressed = false;
        }
    }

    private void Update()
    {
        if (movementInput.magnitude == 0 || mouseInput.magnitude != 0)
        {
            camVals.position = cam.position;
            camVals.rotation = cam.rotation;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movementDirection = new Vector3(movementInput.x, 0, movementInput.y);

        if (movementDirection.magnitude > .05)
        {

            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + camVals.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerRB.AddForce(moveDir * speed, ForceMode.Acceleration);

        }
        
        if (Physics.Raycast(this.transform.position, new Vector3(0, -1f, 0), 1.1f) && jumpPressed) {
            //jumpPressed = false;
            playerRB.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }
}
