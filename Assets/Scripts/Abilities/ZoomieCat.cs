using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Collections;

public class ZoomieCat : PlayerMovement
{
    private Coroutine speedCharge;
    [SerializeField] GameObject[] boots;
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] protected float newSpeed = 100f;

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
        base.FixedUpdate();
        if (activeZoomie)
        {
            playerRB.AddForce(moveDir * (newSpeed * speedMultiplier), ForceMode.Acceleration);

        }
        if (playerRB.velocity.magnitude <= .05)
        {
            SpeedBoost(false);
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
            StopCoroutine(speedCharge);
        }

        
    }

    IEnumerator ChangeCoroutine()
    {
        yield return new WaitForSeconds(chargeTime);
        SpeedBoost(true);
    }

    public void SpeedBoost(bool state)
    {
        chargingZoomie = false;
        activeZoomie = state;
        if (state)
        {
            //Debug.Log("Zoomie");
        }
        else
        {

            //Debug.Log("No Zoomie");
        }
       
        foreach (var boot in boots)
        {
            boot.SetActive(state);
        }
    }

    //All Inputs
    #region Input

    public void OnZoomie(InputAction.CallbackContext ctx)
    {
        //  && movementInput.x != 0 Check if movement direction is not 0
        //  !chargingZoomie && !activeZoomie &&  && movementDirection.magnitude > .05
        if (ctx.action.triggered)
        {
            ChargeZoomie(true);
        }
        else
        {
            if (activeZoomie)
            {
                SpeedBoost(false);
            }
            
        }
        
        
    }

    #endregion
}
