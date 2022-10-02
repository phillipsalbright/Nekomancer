using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;

public class RollyCat : PlayerMovement
{
    [Header("Rolly Cat")]
    public float rotateAmt = 2f;
    public float gravityStrength = 10f;
    public GameObject key;

    private const int CLINGABLE_LAYER = 7;

    private Vector3 gravityDirection = Vector3.down;
    [HideInInspector]
    public bool collectedObject;

    protected override void OnEnable()
    {
        base.OnEnable();
        audioManager = GetComponent<AudioSource>();
        audioManager.clip = activeClip;
        audioManager.loop = true;
        audioManager.Play();
        playerRB.useGravity = false;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        audioManager.Stop();
        playerRB.useGravity = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!this.enabled)
        {
            return;
        }

        if (other.CompareTag("Collectible"))
        {
            CollectItem();
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Chain"))
        {
            other.gameObject.GetComponent<Chain>().PullChain();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!this.enabled)
        {
            return;
        }

        if (collision.collider.gameObject.layer == CLINGABLE_LAYER)
        {
            gravityDirection = -collision.impulse.normalized;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (!this.enabled)
        {
            return;
        }

        if (collision.collider.gameObject.layer == CLINGABLE_LAYER)
        {
            gravityDirection = Vector3.down;
        }
    }

    public override void OnMove(InputAction.CallbackContext ctx)
    {
        base.OnMove(ctx);
    }

    public override void OnJump(InputAction.CallbackContext ctx)
    {
        base.OnJump(ctx);
    }

    public override void OnMouse(InputAction.CallbackContext ctx)
    {
        base.OnMouse(ctx);
    }

    private void CollectItem()
    {
        collectedObject = true;
        key.SetActive(true);
    }

    public void RemoveItem()
    {
        collectedObject = false;
        key.SetActive(false);
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
            playerRB.AddForce(normalOfPlane * jumpForce, ForceMode.Impulse);
        }



        //Vector3 movementDirection = new Vector3(movementInput.x, 0, movementInput.y);
        //if (movementDirection.magnitude > .05)
        //{
        //    visuals.transform.Rotate(transform.right, rotateAmt);
        //}

        float r = 0.5f;
        float rollSpeed = 3;

        Vector3 moveDelta = new Vector3(movementDirection.x * rollSpeed * Time.deltaTime, movementDirection.z * rollSpeed * Time.deltaTime, 0);
        Vector3 rotationAxis = Vector3.Cross(moveDelta.normalized, transform.forward);
        visuals.transform.RotateAround(visuals.transform.position, rotationAxis, Mathf.Sin(moveDelta.magnitude * r * 2 * Mathf.PI)  *Mathf.Rad2Deg);
    }
}
