using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Ingredient> ingredients { get; private set; }
    public List<Recipe> recipes{ get; private set; }


static public GameManager instance;

  
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else { instance = this; }
    }
    private void Start()
    {
        ingredients = DBManager._DB_MANAGER.GetIngredients();
        recipes = DBManager._DB_MANAGER.GetRecipes();
        DBManager._DB_MANAGER.GetRecipeIngredients(recipes[1]);
    }
    
}
