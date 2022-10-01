using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    Image[] itemImages = new Image[3];

    List<string> ingredients = new List<string>();
    int counter = 0;

    CatState catState;

    private void Start()
    {
        catState = GetComponent<CatState>();
        foreach (Image itemImage in itemImages)
        {
            itemImage.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Potion"))
        {
            AddIngredient(collision.gameObject.GetComponent<Ingredient>());
            Destroy(collision.gameObject);
        }
    }

    public void AddIngredient(Ingredient ingredient)
    {
        if (counter == 3)
            return;
        itemImages[counter].sprite = ingredient.GetSprite();
        itemImages[counter].gameObject.SetActive(true);
        ingredients.Add(ingredient.GetIngredientName());
        counter++;
    }

    public void MakePotion()
    {
        if (counter != 3)
        {
            return;
        }
        for (int i = 0; i < itemImages.Length; i++)
        {
            itemImages[i].gameObject.SetActive(false);
        }
        ingredients.Sort();
        catState.ApplyPotion(string.Join(",", ingredients));
    }
}
