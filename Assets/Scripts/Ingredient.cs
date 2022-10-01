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

    public GameObject relObj;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ingredientPickup = true;
            invSystem = other.GetComponentInParent<InventorySystem>();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ingredientPickup = false;
        }
    }

    public virtual void Pickup(CallbackContext ctx)
    {
        if (ingredientPickup && ctx.performed)
        {
            invSystem.AddIngredient(this);
            if (relObj != null)
            {
                relObj.SetActive(true);
            }
            Destroy(gameObject);
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
