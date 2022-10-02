using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Lock : MonoBehaviour
{
    bool canAttempt;
    RollyCat rollyCat;

    private void Unlock()
    {
        // ADD OTHER UNLOCK FUNCTIONALITY HERE
        gameObject.SetActive(false);
        rollyCat.RemoveItem();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            canAttempt = true;
            rollyCat = other.gameObject.GetComponentInParent<RollyCat>();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            canAttempt = false;
        }
    }

    public virtual void UnlockAttempt(CallbackContext ctx)
    {
        if (canAttempt && ctx.performed)
        {
            if (rollyCat.collectedObject)
            {
                Unlock();
            }
        }
    }
}
