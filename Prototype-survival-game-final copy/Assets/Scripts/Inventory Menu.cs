using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public GameObject inventoryMenu;
    public int maxItems = 20;
    private bool menuActivated = false;
    private int selectedItemIndex = 0;

    void Start()
    {
        inventoryMenu.SetActive(false);
        selectedItemIndex = 0; // Initialize selected item index only once
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            menuActivated = !menuActivated;
            inventoryMenu.SetActive(menuActivated);
            Time.timeScale = menuActivated ? 0 : 1;

            if (menuActivated)
            {
                UpdateSelectedItem(); // Update selected item when inventory is opened
            }
        }

        if (menuActivated)
        {
            bool selectionChanged = false;

            if (Input.GetKeyDown(KeyCode.D))
            {
                selectedItemIndex = (selectedItemIndex + 1) % maxItems;
                selectionChanged = true;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                selectedItemIndex = (selectedItemIndex - 1 + maxItems) % maxItems;
                selectionChanged = true;
            }

            if (selectionChanged)
            {
                UpdateSelectedItem();
            }
        }
    }

    private void UpdateSelectedItem()
    {
        string itemName = Inventory.Instance.GetItemName(selectedItemIndex);
        string itemDescription = string.IsNullOrEmpty(itemName) ? "No item selected" : ItemDatabase.Instance.GetItemDescription(itemName);

        // Add item count to the description if more than one
        int count = Inventory.Instance.GetItemCount(itemName);
        if (count > 1)
        {
            itemDescription += $" (x{count})";
        }

        if (Inventory.Instance.slotDescriptionText != null)
        {
            Inventory.Instance.slotDescriptionText.text = itemDescription;
        }
    }
}
