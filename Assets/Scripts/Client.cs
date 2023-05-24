using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client
{
    public int clientId;
    public string clientName;
    public Sprite icon;

    public Client(int id, string name, Sprite sprite)
    {
        clientId = id;
        clientName = name;
        icon = sprite;
    }

}
