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
    void Start()
    {

        /*
        string query = "SELECT * FROM potion_types";

        IDbCommand cmd = dbConnection.CreateCommand(); 
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        while (dataReader.Read())
        {
            string potion_type = dataReader.GetString(1);
            Debug.Log(potion_type);
        }

        */
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

            string ingredientName = dataReader.GetString(1);
            float cost = dataReader.GetFloat(2);
            string description = dataReader.GetString(4);
            Ingredient ing = new Ingredient(ingredientName, description,cost, "Ingredient.png" );
           
          ingredients.Add(ing);
        }

        return ingredients;

    }

    public List<Recipe> GetRecipes()
    {
        string query = "SELECT * FROM potions";
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        List<Recipe> recipes = new List<Recipe>();

        while (dataReader.Read())
        {
            int id = dataReader.GetInt32(0);
            string RecipeName = dataReader.GetString(1);
            float cost = dataReader.GetFloat(2);
            string description = dataReader.GetString(4);
            
            
            int typeID = dataReader.GetInt32(5);

            Recipe rec = new Recipe(RecipeName, description, cost, "Potion.png", typeID, id);

            recipes.Add(rec);
        }
        
       
        


        return recipes;

    }

    public List<Ingredient> GetRecipeIngredients(Recipe recipe)
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
                Debug.Log(dataReader.GetValue(i));
            }
            Debug.Log("Cambio");

            //ingredients.Add(ing);
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
