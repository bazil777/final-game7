using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    public TextMeshProUGUI pickupText; // Reference to the UI text that will display the message.
    public float pickupRange = 4.0f; // The range within which the player can pick up the item.
    public Transform playerTransform; // Reference to the player's transform.
    public string itemName; // The name of the item to be picked up.

    private void Start()
    {
        pickupText.gameObject.SetActive(false);
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= pickupRange)
        {
            // Check if the item is a hatchet and if the player already has one
            bool alreadyHasHatchet = itemName.Equals("Hatchet") && Inventory.Instance.HasItem("Hatchet");
            if (!alreadyHasHatchet)
            {
                // Show the pickup text for other items or if the player doesn't have a hatchet
                pickupText.gameObject.SetActive(true);
                pickupText.text = "Press P to pick up " + itemName;

                if (Input.GetKeyDown(KeyCode.P))
                {
                    PickupItem();
                }
            }
            else
            {
                // Don't show the pickup text if the player already has a hatchet
                pickupText.gameObject.SetActive(false);
            }
        }
        else
        {
            pickupText.gameObject.SetActive(false);
        }
    }

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
