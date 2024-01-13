[System.Serializable]
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
