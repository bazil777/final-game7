using UnityEngine;

public class TreeChop : MonoBehaviour
{
    public GameObject logPrefab;  // Assign the log prefab in the Inspector

    public void ChopDown()
    {
        // Update inventory automatically
        if (Inventory.Instance != null)
        {
            for (int i = 0; i < 3; i++)
            {
                Inventory.Instance.AddItem("Logs");
            }

            // Show inventory update message
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.ShowInventoryUpdate("+3 Logs added to inventory", 2.0f);
            }
        }

        // Destroy the tree
        Destroy(gameObject);
    }
}
