using UnityEngine;
using TMPro;
using System.Collections.Generic;

//reference inventory, sets max items and inventory size, then checks dictionary to fill slots and description
public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    private const int INVENTORY_SIZE = 20;
    private const int MAX_STACK_SIZE = 15;

    private Dictionary<string, int> itemCounts = new Dictionary<string, int>();
    public TextMeshProUGUI[] slotTexts;
    public TextMeshProUGUI slotDescriptionText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeInventorySlots();
    }
//sets empty inventory slots to []
    private void InitializeInventorySlots()
    {
        for (int i = 0; i < INVENTORY_SIZE; i++)
        {
            slotTexts[i].text = "[]";
        }
    }
//if less than max then add item , or if no item at all then add the item

    public void AddItem(string itemName)
    {
        if (itemCounts.ContainsKey(itemName) && itemCounts[itemName] < MAX_STACK_SIZE)
        {
            itemCounts[itemName]++;
        }
        else if (!itemCounts.ContainsKey(itemName) && itemCounts.Count < INVENTORY_SIZE)
        {
            itemCounts.Add(itemName, 1);
        }
        else
        {
            Debug.Log("Inventory is full or item stack is at maximum");
            return;
        }

        UpdateSlotTexts();
    }
//updates all slots from [], check if has item with same key and also added a remove item method to 
//to remove items from inventoy
    private void UpdateSlotTexts()
    {
        int index = 0;
        foreach (var item in itemCounts)
        {
            // Check if the item name is longer than 3 characters 
            string itemName = item.Key.Length > 3 ? item.Key.Substring(0, 3) : item.Key;

            // Use Rich Text to format the item name and count with different styles
            slotTexts[index].text = "<size=75%><color=#ADD8E6><b>" + itemName + "</b></color> " + // I made it light blue 
                                    "<size=50%><color=yellow>x" + item.Value + "</color></size>"; // Made it yellow and smaller
            index++;
        }

        // Set empty inventory slots to []
        for (int i = index; i < INVENTORY_SIZE; i++)
        {
            slotTexts[i].text = "<color=#ADD8E6>" + "[ ]";
        }
    }


    public bool HasItem(string itemName)
    {
        return itemCounts.ContainsKey(itemName);
    }

    public void RemoveItem(string itemName)
    {
        if (itemCounts.ContainsKey(itemName))
        {
            itemCounts[itemName]--;
            if (itemCounts[itemName] <= 0)
            {
                itemCounts.Remove(itemName);
            }

            UpdateSlotTexts();
        }
    }
//get the item name via the indexand key
    public string GetItemName(int index)
    {
        if (index >= 0 && index < INVENTORY_SIZE)
        {
            int counter = 0;
            foreach (var item in itemCounts)
            {
                if (counter == index)
                {
                    return item.Key;
                }
                counter++;
            }
        }
        return string.Empty;
    }

    // New method to get the count of an item
    public int GetItemCount(string itemName)
    {
        return itemCounts.TryGetValue(itemName, out int count) ? count : 0;
    }
}
