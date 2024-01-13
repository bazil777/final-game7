using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }

    public List<Item> items = new List<Item>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeItems();
        }
        else
        {
            Destroy(gameObject);
        }
    }
//I have added items here
    private void InitializeItems()
    {
        items.Add(new Item("Sword", "A sharp blade"));
        items.Add(new Item("Shield", "Protects you from attacks"));
        items.Add(new Item("Medkit", "Used to heal player's health"));
        items.Add(new Item("Bottle", "This slurp juice gives you a speed boost... for a time"));
        items.Add(new Item("Fork", "Take this & stone to the crafting table"));
        items.Add(new Item("Stone", "Take this & the fork to the crafting table"));
        items.Add(new Item("Hatchet", "Use this to knock down trees to get wood !"));
        items.Add(new Item("Logs", "Use this to craft a better weopon!"));
        items.Add(new Item("Battery", "Plug this into a computer to solve the mini puzzle!"));
        items.Add(new Item("PacCake", "A piece is missing but find a food station to eat and recover some health!"));
        items.Add(new Item("Water", "Find a food station to drink and recover some health!"));
        items.Add(new Item("Sandwich", "Find a food station to eat this sandwich and recover some health!"));
        items.Add(new Item("Racket", "interesting ! Combine this with a crystal to create charm!"));
        items.Add(new Item("Crystal", "interesting ! Combine this with a Racket to create charm!"));
        items.Add(new Item("Air Charm", "This mystic charm will keep the air clean, you'll tak no damage from harmful air poisons"));
    }

    public string GetItemDescription(string itemName)
    {
        Item foundItem = items.Find(item => item.itemName == itemName);
        return foundItem != null ? foundItem.itemDescription : "Item not found";
    }
}
