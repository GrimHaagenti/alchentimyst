using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button ShowRecipesButton;

    //public DragAndDropHandler _Drag_Handler { get; private set; }

    static public UIManager instance;


    [SerializeField] GameObject recipeButtonTemplate;
    [SerializeField] GameObject recipesParent;

    [SerializeField] GameObject recipeIngredientPref;

    private void Awake()
    {
        if(instance != null &&instance != this)
        {
            Destroy(instance);
        }
        else { instance = this; }

        
        ShowRecipesButton.onClick.AddListener(() => ShowRecipes());
        recipesParent.GetComponent<Button>().onClick.AddListener(() => HideRecipes());
    }
    public void InitUI()
    {

        GameObject parent = recipesParent.GetComponentsInChildren<RectTransform>()[1].gameObject;
        foreach (Recipe recipe in GameManager._GAME_MANAGER.recipes)
        {

            RecipeButton recBut = Instantiate(recipeButtonTemplate, parent.transform).GetComponent<RecipeButton>();
            recBut.initButton(recipe);

            RecipeButtonAccess acc = recBut.gameObject.GetComponent<RecipeButtonAccess>();
            
            acc.recipeName.text = recipe.Name;

            
            acc.Icon.sprite =recipe.icon;
            acc.cost.text = recipe.Cost.ToString();
            recipe.ingredients.ForEach((it) =>
            {
                Instantiate(recipeIngredientPref, acc.IngredientParent.transform).GetComponent<TextMeshProUGUI>().text = it.Name;

            });


            //ingredientsList.Add(recBut.gameObject);
        }
    }

    void ShowRecipes()
    {
       
           
            
        
        recipesParent.SetActive(true);

    }
    void HideRecipes()
    {
        recipesParent.SetActive(false) ;

    }



}
