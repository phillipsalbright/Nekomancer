using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Collections;

public class PlayerZoomMovement : PlayerMovement
{
    bool isBoosting;
    private Coroutine speedCharge;

    [Header("States")]
    [SerializeField] private bool chargingZoomie;
    [SerializeField] private bool activeZoomie;

    [Header("Settings")]
    [SerializeField] private float chargeTime = 1.5f;

    private void FixedUpdate()
    {
        float movementSpeed = 1f; // Temp;
        float speed = movementSpeed * (isBoosting ? 2 : 1);
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
    }

    //All Inputs
    #region Input

    void OnSpeedBoost()
    {
        //  && movementInput.x != 0 Check if movement direction is not 0
        if (!chargingZoomie && !activeZoomie)
        {
            ChargeZoomie(true);
        }
    }

    #endregion
}
