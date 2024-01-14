using UnityEngine;

// finds Interaction range with the health machine
// finds the player position
// Check the distance between the player and the machine
// Check if the player is within range and has pressed the interaction key
public class PerkHealthMachine : MonoBehaviour
{
    public float interactionRange = 4.0f; 
    public Transform playerTransform; 

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= interactionRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed and player is within range.");
            UseHealthMachine();
        }
    }
// Check if the Inventory item is appropriate
//manually added medkit
// Check if the player has a medkit in their inventory, if so start heal otherwise do nothing
// Remoif successful then remove the medkit from the inventory
    private void UseHealthMachine()
    {
        if (Inventory.Instance == null)
        {
            Debug.LogError("Inventory instance is not found.");
            return;
        }

        string medkitItemName = "Medkit";

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

            Inventory.Instance.RemoveItem(medkitItemName);
            Debug.Log("Medkit removed from inventory.");
        }
        else
        {
            Debug.Log("You need a medkit to use this Perk Health Machine!");
        }
    }
}
