using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{

    public int userID;
    public string UserName;
    public string Date;
    public int Level;
    public int Money;

    public User(int id, string name, string date, int level )
    {
        userID = id;
        UserName = name;
        Date = date;
        Level = level;
    }
}
