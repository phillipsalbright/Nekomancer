using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Ingredient : MonoBehaviour
{

    [SerializeField]
    private Sprite ingSprite;
    [SerializeField]
    private string IngredientName;

    bool ingredientPickup = false;
    InventorySystem invSystem;

    private void Start()
    {
        //controls interact Pickup
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ingredientPickup = true;
            invSystem = other.gameObject.GetComponent<InventorySystem>();
        }
    }

    void Pickup(CallbackContext ctx)
    {
        if (ingredientPickup)
        {
            invSystem.AddIngredient(this);
        }
    }

    public Sprite GetSprite()
    {
        return ingSprite;
    }

    public string GetIngredientName()
    {
        return IngredientName;
    }
}
