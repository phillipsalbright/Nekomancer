using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;

public class RollyCat : PlayerMovement
{
    [Header("Rolly Cat")]
    public float rotateAmt = 2f;
    public float gravityStrength = 10f;

    private Vector3 gravityDirection = Vector3.down;

    public void OnEnable()
    {
        base.OnEnable();
        playerRB.useGravity = false;
    }

    public void OnDisable()
    {
        playerRB.useGravity = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        gravityDirection = -collision.impulse.normalized;
    }

    public void OnCollisionExit(Collision collision)
    {
        gravityDirection = Vector3.down;
    }

    public override void OnMove(InputAction.CallbackContext ctx)
    {
        base.OnMove(ctx);
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        playerRB.AddForce(gravityDirection * gravityStrength);
        Vector3 normalOfPlane = (gravityDirection * -1).normalized;
        Vector3 forwardMovement = Vector3.ProjectOnPlane(transform.position - cam.position, normalOfPlane).normalized;
        Vector3 sideMovement = Vector3.Cross(forwardMovement, normalOfPlane).normalized;

        movementDirection = new Vector3(movementInput.x, 0, movementInput.y);

        if (movementDirection.magnitude > .05)
        {
            if (anim)
            {
                anim.SetBool("Walking", true);
            }

            /** this little block is purely cosmetic, just a bunch of bullshit to mess with if you want. Might not even be what we want for rolling cat since its just supposed to roll */
            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;



            playerRB.AddForce(forwardMovement*speed * movementDirection.z, ForceMode.Acceleration);
            playerRB.AddForce(sideMovement * speed * -movementDirection.x, ForceMode.Acceleration);
            if (movementDirection.x != 0)
            {
                FindObjectOfType<CinemachineFreeLook>().m_RecenterToTargetHeading.m_enabled = true;
            }
            else
            {
                FindObjectOfType<CinemachineFreeLook>().m_RecenterToTargetHeading.m_enabled = false;
            }

        }
        else
        {
            FindObjectOfType<CinemachineFreeLook>().m_RecenterToTargetHeading.m_enabled = false;
            if (anim)
            {
                anim.SetBool("Walking", false);
            }
        }

        if (Physics.Raycast(this.transform.position, gravityDirection.normalized, 1.1f) && jumpPressed)
        {
            //jumpPressed = false;
            playerRB.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }

        //Vector3 movementDirection = new Vector3(movementInput.x, 0, movementInput.y);
        //if (movementDirection.magnitude > .05)
        //{
        //    visuals.transform.Rotate(transform.right, rotateAmt);
        //}
    }
}
