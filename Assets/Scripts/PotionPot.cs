using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PotionPot : MonoBehaviour
{
    bool ingredientPickup = false;
    InventorySystem invSystem;

    // Start is called before the first frame update
    void Start()
    {
        //set up controller interact
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ingredientPickup = true;
            invSystem = other.gameObject.GetComponent<InventorySystem>();
        }
    }

    void Craft(CallbackContext ctx)
    {
        if (ingredientPickup)
        {
            invSystem.MakePotion();
        }
    }
}
