using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    public TextMeshProUGUI pickupText;
    public float pickupRange = 4.0f;
    public Transform playerTransform;
    public string itemName;
    private PlayerGold playerGold;

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

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= pickupRange)
        {
            // Check if the item is already in the inventory and is a non-stackable item
            bool alreadyHasItem = (itemName.Equals("Hatchet") || itemName.Equals("Torch")) && Inventory.Instance.HasItem(itemName);
            if (!alreadyHasItem)
            {
                // Show the pickup text if the player doesn't have the item or it's stackable
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
