using UnityEngine;
using TMPro;

// Reference to the UI text that will display the message.
// The range within which the player can pick up the item.
//get player pos
//then find name
public class ItemPickup : MonoBehaviour
{
<<<<<<< HEAD
    public TextMeshProUGUI pickupText;
    public float pickupRange = 4.0f;
    public Transform playerTransform;
    public string itemName;
     private PlayerGold playerGold;
=======
    public TextMeshProUGUI pickupText; 
    public float pickupRange = 4.0f; 
    public Transform playerTransform; 
    public string itemName; 
>>>>>>> 030d312468b850f6a1e45789359b93d6160a8064

    private void Start()
    {
        pickupText.gameObject.SetActive(false);

        // Find the PlayerGold component on the player GameObject using the new method
        playerGold = Object.FindObjectOfType(typeof(PlayerGold)) as PlayerGold;
        if (playerGold == null)
        {
            Debug.LogError("PlayerGold script not found in the scene.");
        }
    }
//if special case like hatchet then dont allow pickup if already owned
//otherwise i will display press P to pick up and if p is pressed call pick up item method
    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= pickupRange)
        {
<<<<<<< HEAD
            // Check if the item is already in the inventory and is a non-stackable item
            bool alreadyHasItem = (itemName.Equals("Hatchet") || itemName.Equals("Torch")) && Inventory.Instance.HasItem(itemName);
            if (!alreadyHasItem)
            {
                // Show the pickup text if the player doesn't have the item or it's stackable
=======
            // Check if hatchet and if the player has
            bool alreadyHasHatchet = itemName.Equals("Hatchet") && Inventory.Instance.HasItem("Hatchet");
            if (!alreadyHasHatchet)
            {
                // Show the pickup textif the player doesn't have a hatchet
>>>>>>> 030d312468b850f6a1e45789359b93d6160a8064
                pickupText.gameObject.SetActive(true);
                pickupText.text = "Press P to pick up " + itemName;

                if (Input.GetKeyDown(KeyCode.P))
                {
                    PickupItem();
                }
            }
            else
            {
<<<<<<< HEAD
=======
                
>>>>>>> 030d312468b850f6a1e45789359b93d6160a8064
                pickupText.gameObject.SetActive(false);
            }
        }
        else
        {
            pickupText.gameObject.SetActive(false);
        }
    }
//if item not in database, inventory needs a way to deal with this so Ill just give it these values
    private void PickupItem()
    {
        if (Inventory.Instance != null && !string.IsNullOrEmpty(itemName))
        {
            string itemDescription = ItemDatabase.Instance.GetItemDescription(itemName);

            if (!string.IsNullOrEmpty(itemDescription))
            {
                Inventory.Instance.AddItem(itemName);
                Debug.Log("Picked up: " + itemName + " - " + itemDescription);
                playerGold.AddGold(50); // Add 50 gold
                
            }
            else
            {
                Debug.LogError("Item not found in database: " + itemName);
            }

            pickupText.gameObject.SetActive(false);
            gameObject.SetActive(false); // Optionally, disable the item GameObject
        }
    }
}
