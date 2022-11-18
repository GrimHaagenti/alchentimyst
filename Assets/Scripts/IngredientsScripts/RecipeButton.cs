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
    [SerializeField] GameObject ingredientPreviewPref;
    GameObject previewParent;






    public void initButton(Recipe recipe)
    {

        ingredient_name = recipe.Name;
        ingredientCost = recipe.Cost;
       
        Texture2D tex = Resources.Load<Texture2D>("images/" + ingredient_img);
        ingredientSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 1);
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
        setButton();
    }
    void setButton()
    {
        
        Transform[] gameObjects = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform go in gameObjects)
        {
            if (go.name == "RecipeName")
            {
                ingredientNameContainer = go.GetComponent<TextMeshProUGUI>();
            }
            if (go.name == "Cost")
            {
                ingredientCostContainer = go.GetComponent<TextMeshProUGUI>();
            }
            if (go.name == "RecipeIcon")
            {
                ingredientIcon = go.GetComponent<Image>();

            }

        }
        ingredientIcon.sprite = ingredientSprite;
        ingredientNameContainer.text = ingredient_name;
        ingredientCostContainer.text = costString + ingredientCost.ToString();


    }
    
    

   
}
