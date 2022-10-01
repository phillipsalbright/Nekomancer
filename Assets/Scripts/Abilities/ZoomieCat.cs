using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Collections;

public class ZoomieCat : PlayerMovement
{
    private Coroutine speedCharge;
    [SerializeField] private float speedMultiplier = 2f;

    [Header("States")]
    [SerializeField] private bool chargingZoomie;
    [SerializeField] private bool activeZoomie;

    [Header("Settings")]
    [SerializeField] private float chargeTime = 1.5f;

    protected override void FixedUpdate()
    {
        // If charging, LERP the cat speed to max
        // If activeZoomie, multiply the cat speed

        //float speed = movementSpeed * (activeZoomie ? 2 : 1);

        if (activeZoomie)
        {
            playerRB.AddForce(moveDir * speed * speedMultiplier, ForceMode.Acceleration);

        }
        if (movementDirection.magnitude <= .05)
        {
            ChargeZoomie(false);
        }
    }

    void ChargeZoomie(bool state)
    {
        chargingZoomie = state;

        if (state)
        {
            speedCharge = StartCoroutine(ChangeCoroutine());
        }
        else
        {
            if (speedCharge != null)
            {
                StopCoroutine(speedCharge);
            }
            
        }

        IEnumerator ChangeCoroutine()
        {
            yield return new WaitForSeconds(chargeTime);
            SpeedBoost(true);
        }
    }

    public void SpeedBoost(bool state)
    {
        chargingZoomie = false;
        activeZoomie = state;
        Debug.Log("Zoomie");
    }

    //All Inputs
    #region Input

    public void OnZoomie(InputAction.CallbackContext ctx)
    {
        //  && movementInput.x != 0 Check if movement direction is not 0
        //  
        if (!chargingZoomie && !activeZoomie && ctx.action.triggered && movementDirection.magnitude > .05)
        {
            ChargeZoomie(true);
        }
        
    }

    #endregion
}
