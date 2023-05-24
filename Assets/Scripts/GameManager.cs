using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public List<Ingredient> ingredients;
    [SerializeField] public List<Potion> recipes;
    [SerializeField] public List<User> Users;
    [SerializeField] public List<Client> clients;
    public User currentUser;

    [SerializeField] public UIManager _UI_MANAGER;
    [SerializeField] public CauldronManager CauldronManager;
    [SerializeField] public OrderFactory orderFactory;

    [SerializeField] public List<OrderButton> PotionOrders = new List<OrderButton>();
    [SerializeField] public List<Potion> PotionInOrder = new List<Potion>();
    [SerializeField] public GameObject PotionsOrderParent;

    static public GameManager _GAME_MANAGER;

    [SerializeField] public TextMeshProUGUI scoreText;
    int Score = 0;
    public Order currentOrder;

    public string newUserName;

    private void Awake()
    {
        if (_GAME_MANAGER != null && _GAME_MANAGER != this)
        {
            Destroy(_GAME_MANAGER);
        }
        else { _GAME_MANAGER = this; }

        if (_UI_MANAGER == null)
        {
            _UI_MANAGER = new UIManager();
        }
        if (orderFactory == null)
        {
            orderFactory = new OrderFactory();
        }

        OrderButton[] order = PotionsOrderParent.GetComponentsInChildren<OrderButton>();
        int indexer = 0;
        foreach (OrderButton ord in order)
        {
            ord.index = indexer;
            PotionOrders.Add(ord);
            indexer++;
        }


        PotionOrders.ForEach((it) =>
        {
            it.ResetButton();
            it.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (it.index < PotionInOrder.Count)
                {
                    PotionInOrder.RemoveAt(it.index);
                }
                ResetList();
                
            });

        });
    }
    

    void ResetList()
    {
        PotionOrders.ForEach((it) =>
        {
            it.ResetButton();
            if(it.index < PotionInOrder.Count){
                it.SetButton(PotionInOrder[it.index]); 
            }
        });


    }
    public void LoadUser()
    {
        _UI_MANAGER.DestroyUsersList();
        Users = DBManager._DB_MANAGER.GetUsers();

    }

    public void ExitGame()
    {
        currentOrder = null;
        currentUser = null;

    }
    public void NewGameStart(string userName)
    {
        User newUser = DBManager._DB_MANAGER.InsertNewUser(userName);
        if (newUser != null)
        {
            currentUser = newUser;
            Score = newUser.Level - 1;
            scoreText.text = "Score: " + Score;
            currentOrder = orderFactory.CreateOrder();
            _UI_MANAGER.UpdateCurrentMoney(newUser.Money);
            _UI_MANAGER.BeginGame();
        }
        else
        {
            Debug.LogError("SOMETHING WENT REALLY WRONG");
        }

    }


    public void UserLoadGame(User user)
    {
        currentUser = user;
        Score = user.Level - 1;
        scoreText.text = "Score: " + Score;
        currentOrder = DBManager._DB_MANAGER.GetLastOrder(currentUser);

        orderFactory.LoadOrder(currentOrder);
        _UI_MANAGER.UpdateCurrentMoney(user.Money);
        _UI_MANAGER.BeginGame();
        
    }
    private void Start()
    {
        ingredients = DBManager._DB_MANAGER.GetIngredients();
        recipes = DBManager._DB_MANAGER.GetRecipes();
        clients = DBManager._DB_MANAGER.GetClients();


        _UI_MANAGER.Init();
        CauldronManager.Init();
        orderFactory.Init();

    }

    public void OnNewUserInputChange(string newName)
    {
        newUserName = newName;
    }

  

    public void SendOrder()
    {

        if (CompareListWithOrder())
        {
            foreach (Potion pot in currentOrder.potionsInOrder) {
                currentUser.Money += Mathf.CeilToInt(pot.Cost);
            }
            _UI_MANAGER.UpdateCurrentMoney(currentUser.Money);
            Score++;
            currentUser.Level++;
            DBManager._DB_MANAGER.UpdateUserLevel(currentUser);

            scoreText.text = "Score: " + Score;

            PotionOrders.ForEach((it) =>
            {
                it.ResetButton();
                it.GetComponent<Button>().onClick.AddListener(() =>
                {
                    it.ResetButton();
                });
            });
            PotionInOrder.Clear();
            currentOrder = orderFactory.CreateOrder();
        }
    }

    private bool CompareListWithOrder()
    {
        foreach(Potion pot in currentOrder.potionsInOrder)
        {
            if (!PotionInOrder.Contains(pot)){
                return false;
            }
        }
        return true;
    }

    public void UseTrashCan()
    {
        CauldronManager.ResetCauldron();
    }
    public void AddIngredientToCauldron(Ingredient ing)
    {
        if (CauldronManager.AddIngredientToCauldron(ing))
        {
            if (PotionInOrder.Count < 8)
            {
                PotionInOrder.Add(CauldronManager.currentPotion);
                PotionOrders[PotionInOrder.Count - 1].SetButton(CauldronManager.currentPotion);
                CauldronManager.ResetCauldron();
            }
        }
    }

    
}
