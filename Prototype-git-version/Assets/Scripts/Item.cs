[System.Serializable]
//set up for inventory
public class Item
{
    public string itemName;
    public string itemDescription;

    // Constructor
    public Item(string name, string description)
    {
        itemName = name;
        itemDescription = description;
    }
}
