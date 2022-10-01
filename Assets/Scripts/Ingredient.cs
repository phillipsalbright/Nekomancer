using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Ingredient : MonoBehaviour
{

    [SerializeField]
    protected Sprite ingSprite;
    [SerializeField]
    protected string IngredientName;

    protected bool ingredientPickup = false;
    protected InventorySystem invSystem;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ingredientPickup = true;
            invSystem = other.gameObject.GetComponent<InventorySystem>();
        }
    }

    public virtual void Pickup(CallbackContext ctx)
    {
        if (ingredientPickup)
        {
            invSystem.AddIngredient(this);
            Destroy(this);
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
