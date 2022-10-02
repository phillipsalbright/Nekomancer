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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            potionCraft = true;
            invSystem = other.gameObject.GetComponentInParent<InventorySystem>();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            potionCraft = false;
        }
    }

    public void Craft(CallbackContext ctx)
    {
        if (potionCraft && ctx.performed)
        {
            invSystem.MakePotion();

            PotUsedEvent.Invoke();
        }
    }
}
