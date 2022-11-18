using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button ShowRecipesButton;

    //public DragAndDropHandler _Drag_Handler { get; private set; }

    static public UIManager instance;

    List<Recipe> recipes;

    [SerializeField] GameObject recipeButtonTemplate;
    [SerializeField] GameObject recipesParent;


    private void Awake()
    {
        if(instance != null &&instance != this)
        {
            Destroy(instance);
        }
        else { instance = this; }

        
        ShowRecipesButton.onClick.AddListener(() => ShowRecipes());
        recipesParent.GetComponent<Button>().onClick.AddListener(() => HideRecipes());
        recipes = GameManager.instance.recipes;
    }

    void ShowRecipes()
    {
        if (recipes.Count < 1)
        {
            recipes = DBManager._DB_MANAGER.GetRecipes();
            GameObject parent = recipesParent.GetComponentsInChildren<RectTransform>()[1].gameObject;
            foreach (Recipe recipe in recipes)
            {

                RecipeButton recBut = Instantiate(recipeButtonTemplate, parent.transform).GetComponent<RecipeButton>();
                recBut.initButton(recipe);

                //ingredientsList.Add(recBut.gameObject);
            }
        }
        recipesParent.SetActive(true);

    }
    void HideRecipes()
    {
        recipesParent.SetActive(false) ;

    }



}
