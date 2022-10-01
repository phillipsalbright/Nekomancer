using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField]
    List<Ingredient> ingredients = new List<Ingredient>();

    [SerializeField]
    CatState.States abilityState;

    public string GetStringRep()
    {
        ingredients.Sort();
        return string.Join(",", ingredients);
    }

    public CatState.States GetState()
    {
        return abilityState;
    }
}
