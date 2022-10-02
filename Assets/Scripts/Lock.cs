using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Lock : MonoBehaviour
{
    [SerializeField]
    GameObject door;

    bool canAttempt;
    RollyCat rollyCat;

    private HintBillboard hint;

    private void Unlock()
    {
        // ADD OTHER UNLOCK FUNCTIONALITY HERE
        gameObject.SetActive(false);
        rollyCat.RemoveItem();
        if (door != null)
            door.SetActive(false);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            canAttempt = true;
            rollyCat = other.gameObject.GetComponentInParent<RollyCat>();

            if (rollyCat.collectedObject)
            {
                hint.SetText("to open lock");
                hint.gameObject.SetActive(true);
                hint.SetPosition(transform.position);
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            canAttempt = false;

            hint.gameObject.SetActive(false);
        }
    }

    public virtual void UnlockAttempt(CallbackContext ctx)
    {
        if (canAttempt && ctx.performed)
        {
            if (rollyCat.collectedObject)
            {
                Unlock();
                hint.gameObject.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        if (hint == null)
        {
            hint = FindObjectOfType<HintBillboard>();
        }
    }

}
