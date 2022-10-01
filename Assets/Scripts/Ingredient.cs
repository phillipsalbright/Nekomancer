using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField]
    private Sprite ingSprite;
    [SerializeField]
    private string IngredientName;

    public Sprite GetSprite()
    {
        return ingSprite;
    }

    public string GetIngredientName()
    {
        return IngredientName;
    }
}
