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


    // Update is called once per frame
    void Update()
    {
        
    }
}
