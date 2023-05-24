using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RecipeButton : MonoBehaviour
{
    string ingredient_name;
    string ingredient_img = "Ingredient";
    string costString = "Cost: ";
    float ingredientCost = 0;
    Sprite ingredientSprite;
    Image ingredientIcon;
    TextMeshProUGUI ingredientNameContainer;
    TextMeshProUGUI ingredientCostContainer;
    Button ingredientButton;
    GameObject previewParent;


    //ACCESSORS
    [SerializeField] public Image Icon;
    [SerializeField] public TextMeshProUGUI recipeName;
    [SerializeField] public TextMeshProUGUI cost;
    [SerializeField] public TextMeshProUGUI Description;
    [SerializeField] public GameObject IngredientParent;




    public void initButton(Potion recipe)
    {

        ingredient_name = recipe.Name;
        ingredientCost = recipe.Cost;


        ingredientSprite = recipe.icon;
        Image buttonBG = GetComponent<Image>();
        switch (recipe.typeID)
        {
            default:
            case 1:
                buttonBG.color = Color.white;
                break;
            case 2:
                buttonBG.color = Color.red;

                break;
            case 3:
                buttonBG.color = Color.blue;

                break;
            case 4:
                buttonBG.color = Color.green;

                break;
            case 5:
                buttonBG.color = Color.cyan;

                break;
            case 6:
                buttonBG.color = Color.magenta;

                break;

            case 7:
                buttonBG.color = Color.gray;

                break;
        }
    }
    
    

   
}
