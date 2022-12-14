using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField]
    protected List<Ingredient> ingredients = new List<Ingredient>();

    [SerializeField]
    protected CatState.States abilityState;

    protected AudioSource audioManager;

    [SerializeField]
    protected AudioClip activeClip;

    private void Start()
    {
        audioManager = GetComponent<AudioSource>();
    }

    public string GetStringRep()
    {
        List<string> ingredientNames = new List<string>();
        foreach (Ingredient ingredient in ingredients)
        {
            ingredientNames.Add(ingredient.GetIngredientName());
        }
        ingredientNames.Sort();
        return string.Join(",", ingredientNames);
    }

    public CatState.States GetState()
    {
        return abilityState;
    }
}
