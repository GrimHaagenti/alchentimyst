using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class IngredientButton : MonoBehaviour
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

    public delegate void OnIngredientButtonDown();
    public static event OnIngredientButtonDown pointerDown;


    
    public void initButton(Ingredient ingredient)
    {

        ingredient_name = ingredient.Name;
        ingredientCost = ingredient.Cost;
       
        Texture2D tex = Resources.Load<Texture2D>("images/" + ingredient_img);
        ingredientSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 1);
        setButton();
    }
    void setButton()
    {
        
        Transform[] gameObjects = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform go in gameObjects)
        {
            if (go.name == "IngredientName")
            {
                ingredientNameContainer = go.GetComponent<TextMeshProUGUI>();
            }
            if (go.name == "Cost")
            {
                ingredientCostContainer = go.GetComponent<TextMeshProUGUI>();
            }
            if (go.name == "IngredientIcon")
            {
                ingredientIcon = go.GetComponent<Image>();

            }

        }

        ingredientIcon.sprite = ingredientSprite;
        ingredientNameContainer.text = ingredient_name;
        ingredientCostContainer.text = costString + ingredientCost.ToString();
    }
    
    

    private void Awake()
    {
       ingredientButton = GetComponent<Button>();
       
       
    }

    private void Start()
    {
        

    }
}
