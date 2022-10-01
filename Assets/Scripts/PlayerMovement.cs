using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    protected Vector2 movementInput = new Vector2(0, 0);
    protected Vector3 movementDirection;
    private Vector2 mouseInput = new Vector2(0, 0);
    protected bool jumpPressed = false;
    [SerializeField] protected Rigidbody playerRB;
    [SerializeField] protected float speed = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float turnSmoothTime = .1f;
    [SerializeField] private float dragVal = 2;
    [SerializeField] protected Vector3 moveDir;
    float turnSmoothVelocity;
    [SerializeField] private Transform cam;
    [SerializeField] protected GameObject visuals;
    public virtual void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>().normalized;
    }

    private void OnEnable()
    {
        playerRB.drag = dragVal;
    }

    public virtual void OnMouse(InputAction.CallbackContext ctx)
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

    protected virtual void FixedUpdate()
    {
        Vector3 movementDirection = new Vector3(movementInput.x, 0, movementInput.y);

        if (movementDirection.magnitude > .05)
        {

            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerRB.AddForce(moveDir * speed, ForceMode.Acceleration);
            if (movementDirection.x != 0)
            {
               FindObjectOfType<CinemachineFreeLook>().m_RecenterToTargetHeading.m_enabled = true;
            } else
            {
                FindObjectOfType<CinemachineFreeLook>().m_RecenterToTargetHeading.m_enabled = false;
            }

        } else
        {
            FindObjectOfType<CinemachineFreeLook>().m_RecenterToTargetHeading.m_enabled = false;
        }
        
        if (Physics.Raycast(this.transform.position, new Vector3(0, -1f, 0), 1.1f) && jumpPressed) {
            //jumpPressed = false;
            playerRB.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }
}
