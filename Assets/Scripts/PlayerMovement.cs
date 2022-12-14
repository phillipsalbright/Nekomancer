using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : Ability
{
    protected Vector2 movementInput = new Vector2(0, 0);
    protected Vector3 movementDirection;
    protected Vector2 mouseInput = new Vector2(0, 0);
    protected bool jumpPressed = false;
    [SerializeField] protected Rigidbody playerRB;
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float jumpForce = 10f;
    [SerializeField] protected float turnSmoothTime = .1f;
    [SerializeField] protected float dragVal = 2;
    [SerializeField] protected Vector3 moveDir;
    protected float turnSmoothVelocity;
    [SerializeField] protected Transform cam;
    [SerializeField] protected GameObject visuals;
    [SerializeField] protected Animator anim;

    public virtual void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>().normalized;
    }

    protected virtual void OnEnable()
    {
        playerRB.drag = dragVal;
        visuals.SetActive(true);
    }

    protected virtual void OnDisable()
    {
        visuals.SetActive(false);
    }

    public virtual void OnMouse(InputAction.CallbackContext ctx)
    {
        mouseInput = ctx.ReadValue<Vector2>().normalized;
    }

    public virtual void OnJump(InputAction.CallbackContext ctx)
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
        movementDirection = new Vector3(movementInput.x, 0, movementInput.y);

        if (movementDirection.magnitude > .05)
        {
            if (anim)
            {
                anim.SetBool("Walking", true);
            }
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
            if (anim)
            {
                anim.SetBool("Walking", false);
            }
        }
        
        if (Physics.Raycast(this.transform.position, new Vector3(0, -1f, 0), 1.1f) && jumpPressed) {
            //jumpPressed = false;
            playerRB.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }
}
