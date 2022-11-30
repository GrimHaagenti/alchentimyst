using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public List<Ingredient> ingredients;
    [SerializeField] public List<Recipe> recipes;

    [SerializeField]public UIManager _UI_MANAGER;


    static public GameManager _GAME_MANAGER;

  
    private void Awake()
    {
        if (_GAME_MANAGER != null && _GAME_MANAGER != this)
        {
            Destroy(_GAME_MANAGER);
        }
        else { _GAME_MANAGER = this; }

    }
    private void Start()
    {
        ingredients = DBManager._DB_MANAGER.GetIngredients();
        recipes = DBManager._DB_MANAGER.GetRecipes();

        _UI_MANAGER.InitUI();

    }
    
}
