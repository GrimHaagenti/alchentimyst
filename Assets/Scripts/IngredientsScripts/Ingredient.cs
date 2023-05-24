using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient
{
    public int id;
    string name;
    string description;
    float cost;
    string img_name;
    public Sprite icon;


    public Ingredient(int id, string name, string description, float cost, string img_name)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.cost = cost;
        this.img_name = img_name;
    }

    public string Name { get { return name; } }
    public string Description { get { return description; } }
    public float Cost { get { return cost; } }
    public string ImageName { get { return img_name; } }

}
