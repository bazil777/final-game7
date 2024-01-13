using UnityEngine;

public class FoodStation : MonoBehaviour
{
    public float interactionRange = 4.0f; // Interaction range with the food station
    public Transform playerTransform; // Reference to the player's transform
    public int healthRecoveryAmount = 5; // Amount of health to recover per interaction

    private void Update()
    {
        // Check the distance between the player and this food station
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        // Check if the player is within range and has pressed the interaction key
        if (distance <= interactionRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed and player is within range.");
            UseFoodStation();
        }
    }

    private void UseFoodStation()
    {
        if (Inventory.Instance == null)
        {
            Debug.LogError("Inventory instance is not found.");
            return;
        }

        // List of food items that can be consumed for health recovery
        string[] foodItems = { "PacCake", "Water", "Sandwich" };

        foreach (string foodItem in foodItems)
        {
            if (Inventory.Instance.HasItem(foodItem))
            {
                Debug.Log(foodItem + " found in inventory. Attempting to heal.");

                PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.Heal(healthRecoveryAmount); // Heal the player by a fixed amount
                }
                else
                {
                    Debug.LogError("PlayerHealth component not found on the player.");
                }

                // Remove the food item from the inventory
                Inventory.Instance.RemoveItem(foodItem);
                Debug.Log(foodItem + " removed from inventory.");
                return; // Exit the loop after consuming one item
            }
        }

        Debug.Log("You need a food item to use this Food Station!");
    }
}
