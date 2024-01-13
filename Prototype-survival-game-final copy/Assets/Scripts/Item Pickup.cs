using UnityEngine;
using TMPro;

// Reference to the UI text that will display the message.
// The range within which the player can pick up the item.
//get player pos
//then find name
public class ItemPickup : MonoBehaviour
{
    public TextMeshProUGUI pickupText; 
    public float pickupRange = 4.0f; 
    public Transform playerTransform; 
    public string itemName; 

    private void Start()
    {
        pickupText.gameObject.SetActive(false);
    }
//if special case like hatchet then dont allow pickup if already owned
//otherwise i will display press P to pick up and if p is pressed call pick up item method
    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= pickupRange)
        {
            // Check if hatchet and if the player has
            bool alreadyHasHatchet = itemName.Equals("Hatchet") && Inventory.Instance.HasItem("Hatchet");
            if (!alreadyHasHatchet)
            {
                // Show the pickup textif the player doesn't have a hatchet
                pickupText.gameObject.SetActive(true);
                pickupText.text = "Press P to pick up " + itemName;

                if (Input.GetKeyDown(KeyCode.P))
                {
                    PickupItem();
                }
            }
            else
            {
                
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
            }
            else
            {
                Debug.LogError("Item not found in database: " + itemName);
            }

            pickupText.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
