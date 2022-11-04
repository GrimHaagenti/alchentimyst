using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

public class DBManager : MonoBehaviour
{

    IDbConnection dbConnection;

    private void OpenDatabase()
    {
        string dbUri = "URI=file:alchentimyst.sqlite";
        dbConnection = new SqliteConnection(dbUri); 
        dbConnection.Open();


    }
    void Start()
    {
        OpenDatabase();

        string query = "SELECT * FROM potion_types";

        IDbCommand cmd = dbConnection.CreateCommand(); 
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        while (dataReader.Read())
        {
            string potion_type = dataReader.GetString(1);
            Debug.Log(potion_type);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
