using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class DragAndDropHandler : MonoBehaviour, IPointerDownHandler,  IDragHandler, IEndDragHandler
{
    public GameObject draggedObject;
    public GameObject draggedObjectChild;

    public delegate void ClickOnIngredient(PointerEventData eventData);
    public event ClickOnIngredient OnIngredientClick;

    public delegate void MoveIngredient(PointerEventData eventData);
    public event MoveIngredient OnIngredientMove;
    
    public delegate void DropIngredient(PointerEventData eventData);
    public event DropIngredient OnIngredientDrop;

    private void Update()
    {
    }
    private void Awake()
    {
        draggedObject  = GameObject.Find("DragParent");

    }
    public void SetDraggedObject(GameObject drgObj)
    {
        draggedObjectChild = drgObj; 
        drgObj.transform.parent = draggedObject.transform;
        draggedObjectChild.transform.localPosition = Vector3.zero; 

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("aAA");
        if (eventData.eligibleForClick)
        {
            OnIngredientClick(eventData);
            draggedObject.transform.position = eventData.pointerCurrentRaycast.screenPosition;

        }
    }



    public void OnDrag(PointerEventData eventData)
    {
        draggedObject.transform.position = eventData.pointerCurrentRaycast.screenPosition; 
    }

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(draggedObjectChild); 
    }




    //public void SetDraggedItem(GameObject draggedObj)
    //{
    //    draggedObject = draggedObj;
    //}
    //public void OnDrag(PointerEventData eventData)
    //{
    //    draggedObject.transform.position = eventData.position;

    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{

    //    mouseDownEvent = new MouseDownEvent();
    //    draggedObject.transform.position = eventData.position;
    //    Debug.Log(mouseDownEvent.target);
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    throw new System.NotImplementedException();
    //}


}
