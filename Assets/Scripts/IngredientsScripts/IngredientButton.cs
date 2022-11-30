using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class IngredientButton : MonoBehaviour
{
    
    Image ingredientIcon;
    TextMeshProUGUI ingredientNameContainer;
    TextMeshProUGUI ingredientCostContainer;
    [SerializeField] Button ingredientButton;
    [SerializeField] GameObject ingredientPreviewPref;
    GameObject previewParent;

    Ingredient Ingredient;

    //ACCESSORS
    [SerializeField] public Image Icon;
    [SerializeField] public TextMeshProUGUI Name;
    [SerializeField] public TextMeshProUGUI Cost;





    public void initButton(Ingredient ingredient)
    {

        Ingredient = ingredient;
       
        setButton();
    }
    void setButton()
    {
        Icon.sprite = Ingredient.icon;
        Name.text = Ingredient.Name;
        Cost.text = Ingredient.Cost.ToString();
    }
    
    private void Start()
    {

    }
    public void OnMouseMove(PointerEventData eventData)
    {

    }

    public void OnMouseDrop(PointerEventData eventData)
    {
        
    }

    public void OnMouseClick(PointerEventData eventData)
    {

        Debug.Log("AAAAAAAA");
        GameObject ingredientPreview = Instantiate(ingredientPreviewPref, eventData.pointerCurrentRaycast.screenPosition, Quaternion.identity);
        
        ingredientPreview.GetComponent<Image>().sprite = ingredientSprite;
        _Drag_Handler.SetDraggedObject(ingredientPreview);
        
;    }
}
