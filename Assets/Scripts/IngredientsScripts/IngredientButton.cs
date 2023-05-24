using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class IngredientButton : MonoBehaviour
{
    
    Ingredient Ingredient;


    //ACCESSORS
    [SerializeField] public Image Icon;
    [SerializeField] public TextMeshProUGUI Name;
    [SerializeField] public TextMeshProUGUI Cost;
    [SerializeField] public TextMeshProUGUI Description;
    [SerializeField] public Button ingredientButton;





    public void initButton(Ingredient ingredient)
    {

        Ingredient = ingredient;
       
        setButton();
    }
    void setButton()
    {
        Icon.sprite = Ingredient.icon;
        Name.text = Ingredient.Name;
        Cost.text = "Cost: " +Ingredient.Cost;
        Description.text = Ingredient.Description;
    }
    
    private void Start()
    {

    }

}
