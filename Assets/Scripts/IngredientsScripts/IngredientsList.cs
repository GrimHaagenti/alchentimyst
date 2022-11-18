using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IngredientsList : MonoBehaviour
{
    List<Ingredient> ingredients;

    [SerializeField] GameObject ingredientButtonTemplate;
    
    [SerializeField] Sprite sprite;

    List<GameObject> ingredientsList = new List<GameObject>();

    GameObject ingredientPreview;
    
    void Start()
    {
        ingredients = GameManager.instance.ingredients;
        foreach(Ingredient ingredient in ingredients)
        {
            
            IngredientButton ingBut = Instantiate(ingredientButtonTemplate,transform).GetComponent<IngredientButton>();
            ingBut.initButton(ingredient);

            ingredientsList.Add(ingBut.gameObject);
        }

    }

    public void onClickEvent()
    {


        
        //ingredientPreview.AddComponent<DragAndDropHandler>();

       
    }

    public void OnMouseDown()
    {

        onClickEvent();

    }

    //private void OnMouseDrag()
    //{
    //    DragUpdatedEvent dragUpdatedEvent = new DragUpdatedEvent();
    //    ingredientPreview.transform.position = dragUpdatedEvent.mousePosition;
    //}
    //private void OnMouseExit()
    //{
    //    Destroy(ingredientPreview);
    //}
    // Update is called once per frame
    void Update()
    {







    }
}
