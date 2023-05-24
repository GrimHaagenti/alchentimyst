using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion
{
    public int id;
    string name;
    string description;
    float cost;
    string img_name;
    public int typeID;
    public string type;
    public Sprite icon;
    public List <Ingredient> recipe;
    public Potion(string name, string description, float cost, string img_name,  int typeID, int id)
    {
        this.name = name;
        this.description = description;
        this.cost = cost;
        this.img_name = img_name;
        this.typeID = typeID;
        this.id = id;
    }

    public string Name { get { return name; } }
    public string Description { get { return description; } }
    public float Cost { get { return cost; } }
    public string ImageName { get { return img_name; } }

    
}
