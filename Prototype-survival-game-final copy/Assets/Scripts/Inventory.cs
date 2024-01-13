using UnityEngine;
using TMPro;
using System.Collections.Generic;

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

    private void InitializeInventorySlots()
    {
        for (int i = 0; i < INVENTORY_SIZE; i++)
        {
            slotTexts[i].text = "[]";
        }
    }

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

    private void UpdateSlotTexts()
    {
        int index = 0;
        foreach (var item in itemCounts)
        {
            slotTexts[index].text = item.Key;
            index++;
        }

        for (int i = index; i < INVENTORY_SIZE; i++)
        {
            slotTexts[i].text = "[]";
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
