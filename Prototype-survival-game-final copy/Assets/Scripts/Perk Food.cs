using UnityEngine;

// This class represents the functionality of a food station in the game.
public class FoodStation : MonoBehaviour
{
    public float interactionRange = 4.0f; // Range within which the player can interact with the food station
    public Transform playerTransform; 
    public int healthRecoveryAmount = 5; // Amount of health recovered when using the food station

    private void Update()
    {
        // Check the distance between the player and this food station
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        // Check if the player is within range and has pressed the interaction key (E)
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

                // Heal the player by a fixed amount using the PlayerHealth component
                PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.Heal(healthRecoveryAmount);
                }
                else
                {
                    Debug.LogError("PlayerHealth component not found on the player.");
                }

                // Increase the player's hunger level using the PlayerHunger component
                PlayerHunger playerHunger = playerTransform.GetComponent<PlayerHunger>();
                if (playerHunger != null)
                {
                    playerHunger.IncreaseHunger(20); // Increase hunger by 20
                }
                else
                {
                    Debug.LogError("PlayerHunger component not found on the player.");
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
