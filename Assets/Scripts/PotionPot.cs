using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PotionPot : MonoBehaviour
{
    bool ingredientPickup = false;
    InventorySystem invSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ingredientPickup = true;
            invSystem = other.gameObject.GetComponent<InventorySystem>();
        }
    }

    public void Craft(CallbackContext ctx)
    {
        if (ingredientPickup)
        {
            invSystem.MakePotion();
        }
    }
}
