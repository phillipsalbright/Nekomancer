using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    Image[] itemImages = new Image[3];
    [SerializeField]
    Toggle ClawOfCat;
    [SerializeField]
    Toggle WhiskerOfCat;
    [SerializeField]
    Toggle FurOfCat;

    List<string> ingredients = new List<string>();
    int counter = 0;
    int mainCounter = 0;

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

    public void AddMainIngredient(MainIngredient ingredient)
    {
        switch (ingredient.GetIngredientName())
        {
            default:
            case "WhiskerOfCat":
                WhiskerOfCat.isOn = true;
                break;
            case "ClawOfCat":
                ClawOfCat.isOn = true;
                break;
            case "FurOfCat":
                FurOfCat.isOn = true;
                break;
        }
        mainCounter++;
        if (mainCounter == 3)
        {
            //Win game
        }
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
