using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeButtonAccess : MonoBehaviour
{
    [SerializeField] public Image Icon;
    [SerializeField] public TextMeshProUGUI recipeName;
    [SerializeField] public TextMeshProUGUI cost;
    [SerializeField] public GameObject IngredientParent;
}
