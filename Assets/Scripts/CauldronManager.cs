using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CauldronManager
{
    List<Ingredient> IngredientsInCauldron = new List<Ingredient>();
    [SerializeField] List<Image> IngredientsInCauldronImage;

    public Potion currentPotion;
    
   public void Init() {
        IngredientsInCauldronImage.ForEach((it) =>
        {
            it.gameObject.SetActive(false);

        });
    }

    public void ResetCauldron()
    {
        IngredientsInCauldron.Clear();
        IngredientsInCauldronImage.ForEach((it) =>
        {
            it.sprite = null;
            it.gameObject.SetActive(false);
        });
        currentPotion = null;
    }
   public bool AddIngredientToCauldron(Ingredient ingredientToAdd)
    {
        if (IngredientsInCauldron.Count < 3)
        {
            if (GameManager._GAME_MANAGER.currentUser.Money >= Mathf.FloorToInt(ingredientToAdd.Cost))
            {
                GameManager._GAME_MANAGER.currentUser.Money -= Mathf.FloorToInt(ingredientToAdd.Cost);
                GameManager._GAME_MANAGER._UI_MANAGER.UpdateCurrentMoney(GameManager._GAME_MANAGER.currentUser.Money);
                IngredientsInCauldron.Add(ingredientToAdd);
                IngredientsInCauldronImage[IngredientsInCauldron.Count - 1].sprite = IngredientsInCauldron[IngredientsInCauldron.Count - 1].icon;
                IngredientsInCauldronImage[IngredientsInCauldron.Count - 1].gameObject.SetActive(true);
            }
        }

        if (IngredientsInCauldron.Count >1)
        {
            return CheckIfPotion();
        }
        return false;
    }

    private bool CheckIfPotion()
    {
        int potionId = DBManager._DB_MANAGER.CheckIfPotionCraftable(IngredientsInCauldron);

        if (potionId != -1)
        {
            Potion potion = GameManager._GAME_MANAGER.recipes[potionId-1];
            if (potion.recipe.Count != IngredientsInCauldron.Count)
            {
                return false;
            }
            foreach(Ingredient ingredient in potion.recipe)
            {
                if (!IngredientsInCauldron.Contains(ingredient))
                {
                    Debug.Log("AAA");
                    return false;
                }
            }
            currentPotion = potion;
            return true;
        }
        return false;
    }

}
