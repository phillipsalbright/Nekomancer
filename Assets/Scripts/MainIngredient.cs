using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class MainIngredient : Ingredient
{
    public override void Pickup(CallbackContext ctx)
    {
        if (ingredientPickup && ctx.performed)
        {
            invSystem.AddIngredient(this);
            Destroy(gameObject);
        }
    }
}
