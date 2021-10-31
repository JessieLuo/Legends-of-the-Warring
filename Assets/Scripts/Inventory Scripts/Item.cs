using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// A ScriptableObject that represents any item in the game.
[CreateAssetMenu(menuName = ("Item"))]
public class Item : ScriptableObject
{
    // Config Data
    [SerializeField] string itemID = null;
    [SerializeField] string displayName = null;
    [SerializeField] [TextArea] string description = null;
    [SerializeField] Sprite icon = null;
    [SerializeField] int price;

    public int health;
    public int maxHealth;
    public float attackPower;
    public float attackRange;
    public float defense;
    public float movement;

    // Getters for config data
    public Sprite GetIcon()
    {
        return icon;
    }

    public string GetItemID()
    {
        return itemID;
    }

    public string GetDisplayName()
    {
        return displayName;
    }

    public string GetDescription()
    {
        return description;
    }

    public int GetPrice()
    {
        return price;
    }
}
