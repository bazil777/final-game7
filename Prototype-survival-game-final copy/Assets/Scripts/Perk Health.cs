using UnityEngine;

public class PerkHealthMachine : MonoBehaviour
{
    public float interactionRange = 4.0f; // Interaction range with the health machine
    public Transform playerTransform; // Reference to the player's transform

    private void Update()
    {
        // Check the distance between the player and this health machine
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        // Check if the player is within range and has pressed the interaction key
        if (distance <= interactionRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed and player is within range.");
            UseHealthMachine();
        }
    }

    private void UseHealthMachine()
    {
        // Check if the Inventory instance is available
        if (Inventory.Instance == null)
        {
            Debug.LogError("Inventory instance is not found.");
            return;
        }

        // Assuming "Medkit" is the string identifier for the item
        string medkitItemName = "Medkit";

        // Check if the player has a medkit in their inventory
        if (Inventory.Instance.HasItem(medkitItemName))
        {
            Debug.Log("Medkit found in inventory. Attempting to heal.");

            PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.StartHealingProcess();
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on the player.");
            }

            // Remove the medkit from the inventory
            Inventory.Instance.RemoveItem(medkitItemName);
            Debug.Log("Medkit removed from inventory.");
        }
        else
        {
            Debug.Log("You need a medkit to use this Perk Health Machine!");
        }
    }
}
