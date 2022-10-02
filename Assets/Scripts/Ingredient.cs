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

    public PotionPot potionPot;

    private HintBillboard hint;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ingredientPickup = true;
            invSystem = other.GetComponentInParent<InventorySystem>();

            hint.gameObject.SetActive(true);
            hint.SetPosition(transform.position);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ingredientPickup = false;

            hint.gameObject.SetActive(false);
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
            gameObject.SetActive(false);
            ingredientPickup = false;
            hint.gameObject.SetActive(false);
        }
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
    }

    public Sprite GetSprite()
    {
        return ingSprite;
    }

    public string GetIngredientName()
    {
        return IngredientName;
    }

    private void OnPotUsed()
    {
        Respawn();
    }

    private void Start()
    {
        if (potionPot == null)
        {
            potionPot = FindObjectOfType<PotionPot>();
        }

        if (hint == null)
        {
            hint = FindObjectOfType<HintBillboard>();
        }

        potionPot.PotUsedEvent += OnPotUsed;
    }
}
