using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIManager
{
    [Header("Main Menu")]
    [SerializeField] GameObject MainMenuTab;
    [SerializeField] Button NewGameButton;
    [SerializeField] Button LoadGameButton;
    [SerializeField] Button ExitGameButton;
    
    [Header("User Menu")]
    [SerializeField] GameObject LoadGameTab;
    [SerializeField] RectTransform UserListParent;
    [SerializeField] GameObject UserPrefab;
    [SerializeField] GameObject NoUserText;
    [SerializeField] Button GoBackLoadMenuButton;

    [Header("New Game Menu")]
    [SerializeField] GameObject NewGameTab;
    [SerializeField] GameObject ConfirmationPanel;
    [SerializeField] GameObject AlreadyExistsNameObj;
    [SerializeField] Button ConfirmationButton;
    [SerializeField] Button GoBackNewGameButton;
    [SerializeField] Button SendNameButton;
    [SerializeField] Button NegateNewUserNameButton;

    [Header("Pause Menu")]
    [SerializeField] GameObject PauseTab;
    [SerializeField] Button PauseContinueButton;
    [SerializeField] Button ExitPauseButton;


    [Header("Game")]
    [SerializeField] GameObject GameTab;
    [SerializeField] Button PauseGameButton;
    [SerializeField] TextMeshProUGUI CurrentMoneyText;


    [Header("Game Recipes")]
    /// Recipes ///
    [SerializeField] Button ShowRecipesButton;
    [SerializeField] Button HideRecipesButton;

    [SerializeField] GameObject recipesTab;
    [SerializeField] RectTransform recipesListParent;
    //Prefabs
    [SerializeField] GameObject recipeButtonTemplate;
    [SerializeField] GameObject recipeIngredientPref;
    ///

    [Header("Game Ingredients")]
    /// Ingredients ///
    [SerializeField] RectTransform ingredientsListParent;
    // Prefabs
    [SerializeField] GameObject ingredientButtonPrefab;
    ///

    [Header("Game Order")]
    /// Order ///
    [SerializeField] GameObject orderTab;
    [SerializeField] Button showOrderButton;
    [SerializeField] Button closeOrderButton;
    /// 

    public void DestroyUsersList()
    {
        foreach(UserButtonAccess user in UserListParent.GetComponentsInChildren< UserButtonAccess>())
        {
            GameObject.Destroy(user.gameObject);
        }
    }
    public void Init()
    {
        ShowRecipesButton.onClick.AddListener(() => ShowRecipes());
        HideRecipesButton.onClick.AddListener(() => HideRecipes());

        showOrderButton.onClick.AddListener(() => {
            GameManager._GAME_MANAGER.orderFactory.LoadOrder(GameManager._GAME_MANAGER.currentOrder);
            orderTab.SetActive(true);
            recipesTab.SetActive(false);
        });
        closeOrderButton.onClick.AddListener(() => { orderTab.SetActive(false); 
            recipesTab.SetActive(false);
        });

        PauseGameButton.onClick.AddListener(() => { PauseTab.SetActive(true);  });
        PauseContinueButton.onClick.AddListener(() => { PauseTab.SetActive(false); });

        ExitPauseButton.onClick.AddListener(() => { GameManager._GAME_MANAGER.ExitGame(); PauseTab.SetActive(false); GameTab.SetActive(false); MainMenuTab.SetActive(true); });

        NewGameButton.onClick.AddListener(() =>
        {
            MainMenuTab.SetActive(false);
            NewGameTab.SetActive(true);

        });
        LoadGameButton.onClick.AddListener(() =>
        {
            GameManager._GAME_MANAGER.LoadUser();
            CreateUserList();
            if (UserListParent.transform.childCount == 0)
            {
                NoUserText.SetActive(true);
            }
            MainMenuTab.SetActive(false);
            LoadGameTab.SetActive(true);

        });

        GoBackNewGameButton.onClick.AddListener(() =>
        {
            MainMenuTab.SetActive(true);
            NewGameTab.SetActive(false);

        });

            GoBackLoadMenuButton.onClick.AddListener(() =>
        {
            MainMenuTab.SetActive(true);
            LoadGameTab.SetActive(false);
            NoUserText.SetActive(false);

        });


        ConfirmationButton.onClick.AddListener(()=>
        {
            GameManager._GAME_MANAGER.NewGameStart(GameManager._GAME_MANAGER.newUserName);
            ConfirmationPanel.SetActive(false);
        });
        SendNameButton.onClick.AddListener(() =>
        {
            if (DBManager._DB_MANAGER.CheckIfNameAlreadyExists(GameManager._GAME_MANAGER.newUserName))
            {
                AlreadyExistsNameObj.SetActive(true);
            }
            else
            {
                AlreadyExistsNameObj.SetActive(false);
                ConfirmationPanel.SetActive(true);
            }
        });
        NegateNewUserNameButton.onClick.AddListener(() =>
        {
            ConfirmationPanel.SetActive(false);

        });

        ExitGameButton.onClick.AddListener(() => Application.Quit());


        CreateRecipes();
        CreateIngredientsList();
    }

    public void BeginGame()
    {

        NewGameTab.SetActive(false);
        LoadGameTab.SetActive(false);
        GameTab.SetActive(true);
        GameManager._GAME_MANAGER.orderFactory.LoadOrder(GameManager._GAME_MANAGER.currentOrder);

    }

    public void UpdateCurrentMoney(int money)
    {
        CurrentMoneyText.text = "Money: " + money;
    }

    void CreateUserList()
    {
        foreach(User user in GameManager._GAME_MANAGER.Users)
        {
            UserButtonAccess userButton = GameObject.Instantiate(UserPrefab, UserListParent).GetComponent<UserButtonAccess>();
            userButton.Init(user);
            userButton.GetComponent<Button>().onClick.AddListener(() => { GameManager._GAME_MANAGER.UserLoadGame(user);
            GameManager._GAME_MANAGER.orderFactory.LoadOrder(GameManager._GAME_MANAGER.currentOrder);
            });
        }

    }
    void CreateIngredientsList()
    {
        foreach (Ingredient ingredient in GameManager._GAME_MANAGER.ingredients)
        {
            IngredientButton ingBut = GameObject.Instantiate(ingredientButtonPrefab, ingredientsListParent.transform).GetComponent<IngredientButton>();
            ingBut.initButton(ingredient);
            ingBut.ingredientButton.onClick.AddListener(() => GameManager._GAME_MANAGER.AddIngredientToCauldron(ingredient));
        }
    }

    void CreateRecipes()
    {
        foreach (Potion recipe in GameManager._GAME_MANAGER.recipes)
        {
            RecipeButton recBut = GameObject.Instantiate(recipeButtonTemplate, recipesListParent.transform).GetComponent<RecipeButton>();
            recBut.initButton(recipe);

            recBut.recipeName.text = recipe.Name;

            recBut.Icon.sprite = recipe.icon;
            recBut.Description.text = recipe.Description;
            recBut.cost.text = "Cost: " +recipe.Cost;
            recipe.recipe.ForEach((it) =>
            {
                GameObject.Instantiate(recipeIngredientPref, recBut.IngredientParent.transform).GetComponent<Image>().sprite = it.icon;
            });
        }
    }
    void ShowRecipes()
    {
        recipesTab.SetActive(true);
        orderTab.SetActive(false);
    }
    void HideRecipes()
    {
        recipesTab.SetActive(false) ;
        orderTab.SetActive(false);
    }



}
