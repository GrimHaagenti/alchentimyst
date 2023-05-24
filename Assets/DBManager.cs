using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

public class DBManager : MonoBehaviour
{
   

    public static DBManager _DB_MANAGER;

    IDbConnection dbConnection;

    private void OpenDatabase()
    {
        string dbUri = "URI=file:alchentimyst.sqlite";
        dbConnection = new SqliteConnection(dbUri); 
        dbConnection.Open();
    }
    private void Awake()
    {
        if (_DB_MANAGER != null && _DB_MANAGER != this)
        {
            Destroy(_DB_MANAGER);
        }
        else
        {
            _DB_MANAGER = this;
        }

        OpenDatabase();

    }
    public Order GetLastOrder(User user)
    {

        string query = "SELECT * FROM orders WHERE id_user=" + user.userID +
            " ORDER BY id_order DESC LIMIT 1;";
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;
        IDataReader dataReader = cmd.ExecuteReader();

        Order lastOrder = new Order(); 
        

        while (dataReader.Read())
        {
            lastOrder.OrderId = dataReader.GetInt32(0);
            lastOrder.orderClient = GameManager._GAME_MANAGER.clients[dataReader.GetInt32(3)-1];


        }

        query = "SELECT id_potion FROM orders_potions WHERE id_order=" + lastOrder.OrderId + ";";
        cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;
        dataReader = cmd.ExecuteReader();
        lastOrder.potionsInOrder = new List<Potion>();
        while (dataReader.Read())
        {
            lastOrder.potionsInOrder.Add(GameManager._GAME_MANAGER.recipes[dataReader.GetInt32(0)-1]);
        }


        return lastOrder;


    }
    public List<Client> GetClients()
    {
        string query = "SELECT * FROM clients";
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        List<Client> clients = new List<Client>();

        while (dataReader.Read())
        {
            int clientId = dataReader.GetInt32(0);
            string clientName = dataReader.GetString(1);
            string clientSpritePath = dataReader.GetString(2);
            Texture2D tex = Resources.Load<Texture2D>("clients/" + clientSpritePath);
            Sprite ClientSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 1);

            Client user = new Client(clientId, clientName, ClientSprite);
            clients.Add(user);
        }

        return clients;
    }

   
    public void InsertOrder(Order order, User player)
    {
        string query = "INSERT INTO orders(number, date, id_client, id_user) " +
            "VALUES('" + player.Level + "', CURRENT_TIMESTAMP, " + order.orderClient.clientId+
            ", " +player.userID+");";
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;
        cmd.ExecuteReader();

        query = "SELECT id_order FROM orders WHERE id_order=LAST_INSERT_ROWID();";

        cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;
        IDataReader dataReader = cmd.ExecuteReader();
        int orderID = -1;


        while (dataReader.Read())
        {
            orderID = dataReader.GetInt32(0);
            order.OrderId = orderID;
        }

        foreach(Potion pot in order.potionsInOrder)
        {
            string potQuery = "INSERT INTO orders_potions(id_order, id_potion) " +
               "VALUES(" + orderID + ", " +  pot.id + ");";

            IDbCommand potCmd = dbConnection.CreateCommand();
            potCmd.CommandText = potQuery;
            potCmd.ExecuteReader();

        }


    }


    public User InsertNewUser(string Name)
    {
        string query = "INSERT INTO users(name, date, level) " +
            "VALUES('" + Name + "', CURRENT_TIMESTAMP, 1);";
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;
        cmd.ExecuteReader();


        query = "SELECT * FROM users WHERE id_user=LAST_INSERT_ROWID();";

        cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;
        IDataReader dataReader = cmd.ExecuteReader();
        User newUser = null;

        while (dataReader.Read())
        {
            int idUser = dataReader.GetInt32(0);
            string userName = dataReader.GetString(1);
            string userDate= dataReader.GetString(2);
            int userLevel = dataReader.GetInt32(3);

            newUser = new User(idUser, userName, userDate, userLevel);
            newUser.Money = 10000;

        }

        query = "INSERT INTO bank_accounts(balance, id_user) VALUES ("+ newUser.Money +", "+ newUser.userID + ");";

        cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;
        cmd.ExecuteReader();



        return newUser;



    }



    public bool CheckIfNameAlreadyExists(string name)
    {
        string query = "SELECT name FROM users";
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;
        IDataReader dataReader = cmd.ExecuteReader();

        while (dataReader.Read())
        {
            string userName = dataReader.GetString(0);
            if (userName == name) 
            {
                return true;
            }

        }
        return false;

    }

    public void UpdateUserLevel(User user)
    {
        string query = "UPDATE users Set level="+user.Level+" WHERE id_user ="+user.userID;
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;
        cmd.ExecuteReader();
        query = "UPDATE bank_accounts Set balance="+user.Money+" WHERE id_user ="+user.userID;
        cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;
        cmd.ExecuteReader();


    }

    public int CheckIfPotionCraftable(List<Ingredient> ingredients)
    {
        string query = "";
        for (int i = 0; i < ingredients.Count; i++)
        {
            query += "SELECT id_potion FROM potions_ingredients WHERE id_ingredient =" + ingredients[i].id;

            if (i != ingredients.Count - 1)
            {
                query += " INTERSECT ";
            }

        }
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        List<Potion> recipes = new List<Potion>();
        
        while (dataReader.Read())
        {
            recipes.Add(GameManager._GAME_MANAGER.recipes[dataReader.GetInt32(0) - 1]);
        }
        if(recipes.Count == 1)
        {
            return recipes[0].id;
        }

        return -1;

    }

    public List<User> GetUsers()
    {
        string query = "SELECT * FROM users";
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        List<User> users = new List<User>();

        while (dataReader.Read())
        {
            int userId = dataReader.GetInt32(0);
            string userName = dataReader.GetString(1);
            string userDate = dataReader.GetString(2);
            int userLevel = dataReader.GetInt32(3);

            User user = new User(userId, userName, userDate, userLevel);
            users.Add(user);
        }
        for (int i = 0; i < users.Count; i++)
        {
            string userQuery = "SELECT balance FROM bank_accounts WHERE id_user="+ users[i].userID+";";
            IDbCommand userCmd = dbConnection.CreateCommand();
            userCmd.CommandText = userQuery;
            IDataReader userDataReader = userCmd.ExecuteReader();
            while (userDataReader.Read())
            {
                users[i].Money = Mathf.CeilToInt(userDataReader.GetFloat(0));
            }
        }



        return users;


    }


    public List<Ingredient> GetIngredients()
    {
        string query = "SELECT * FROM ingredients";
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        List<Ingredient> ingredients = new List<Ingredient>();

        while (dataReader.Read())
        {
            int ingredientId = dataReader.GetInt32(0);
            string ingredientName = dataReader.GetString(1);
            float cost = dataReader.GetFloat(2);
            string description = dataReader.GetString(4);
            string icon_path = dataReader.GetString(3);
            Texture2D tex = Resources.Load<Texture2D>("ingredients/" + icon_path);
            Sprite ingredientSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 1);
           
            Ingredient ing = new Ingredient(ingredientId, ingredientName, description,cost, icon_path);

            ing.icon = ingredientSprite;
            
          ingredients.Add(ing);
        }

        return ingredients;

    }

    public List<Potion> GetRecipes()
    {
        string query = "SELECT * FROM potions";
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        List<Potion> recipes = new List<Potion>();

        while (dataReader.Read())
        {
            int id = dataReader.GetInt32(0);
            string RecipeName = dataReader.GetString(1);
            float cost = dataReader.GetFloat(2);
            string icon_path = dataReader.GetString(3);
            Texture2D tex = Resources.Load<Texture2D>("potions/" + icon_path);

            string description = dataReader.GetString(4);
            
            
            int typeID = dataReader.GetInt32(5);

            Sprite ingredientSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 1);

           
            Potion rec = new Potion(RecipeName, description, cost, icon_path, typeID, id);
            rec.icon = ingredientSprite;
            rec.recipe = GetRecipeIngredients(rec);
            recipes.Add(rec);
        }
        
       
        


        return recipes;

    }

    public List<Ingredient> GetRecipeIngredients(Potion recipe)
    {

        string query = "SELECT ingredients.id_ingredient FROM ingredients " +
        "LEFT JOIN potions_ingredients ON ingredients.id_ingredient = potions_ingredients.id_ingredient " +
        "LEFT JOIN potions ON potions.id_potion = potions_ingredients.id_potion " +
        "WHERE potions.id_potion=" + recipe.id+ ";";

        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        List<Ingredient> ingredients = new List<Ingredient>();

        while (dataReader.Read())
        {

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                int aa = dataReader.GetInt32(i);

                ingredients.Add(GameManager._GAME_MANAGER.ingredients[aa -1]);

            }
        }
        return ingredients;
    }

    

    string GetPotionType(int typeID)
    {
        string typeQuery = "SELECT * FROM potion_types WHERE id_potion_type=" + typeID;

        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = typeQuery;

        IDataReader dataReader = cmd.ExecuteReader();
        return dataReader.GetString(1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
