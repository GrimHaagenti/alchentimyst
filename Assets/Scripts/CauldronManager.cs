using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronManager : MonoBehaviour
{
    List<Ingredient> IngredientsInCauldron = new List<Ingredient>();

   public bool AddIngredientToCauldron(Ingredient ingredientToAdd)
    {
        IngredientsInCauldron.Add(ingredientToAdd);
        return CheckIfPotion();
    }

    private bool CheckIfPotion()
    {

    }

}
