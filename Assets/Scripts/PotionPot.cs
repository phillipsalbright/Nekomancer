using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

public class PotionPot : MonoBehaviour
{
    public delegate void PotUsed();
    public event PotUsed PotUsedEvent;

    bool potionCraft = false;
    InventorySystem invSystem;

    private HintBillboard hint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            potionCraft = true;
            invSystem = other.gameObject.GetComponentInParent<InventorySystem>();

            hint.gameObject.SetActive(true);
            hint.SetPosition(transform.position);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            potionCraft = false;

            hint.gameObject.SetActive(false);
        }
    }

    public void Craft(CallbackContext ctx)
    {
        if (potionCraft && ctx.performed)
        {
            invSystem.MakePotion();
            hint.gameObject.SetActive(false);

            PotUsedEvent.Invoke();
        }
    }

    private void Start()
    {
        if (hint == null)
        {
            hint = FindObjectOfType<HintBillboard>();
        }
    }
}
