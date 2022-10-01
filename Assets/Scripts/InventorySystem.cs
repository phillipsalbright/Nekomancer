using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    Image[] itemImages = new Image[3];

    string ingredients = "";
    int counter = 0;

    private void Start()
    {
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

    void AddIngredient(Ingredient ingredient)
    {
        itemImages[counter].sprite = ingredient.GetSprite();
        itemImages[counter].gameObject.SetActive(true);
        ingredients += ingredient.GetIngredientName();
        counter++;
        if (counter == 3)
        {
            MakePotion();
        }
    }

    void MakePotion()
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            itemImages[i].gameObject.SetActive(false);
        }
    }
}
