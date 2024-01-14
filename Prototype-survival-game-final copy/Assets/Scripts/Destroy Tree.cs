using UnityEngine;

//gives log in inspectort, adds item to inventory 3 times 
//and displays a message then destroys the tree object
public class TreeChop : MonoBehaviour
{
    public GameObject logPrefab;  

    public void ChopDown()
    {

        if (Inventory.Instance != null)
        {
            for (int i = 0; i < 3; i++)
            {
                Inventory.Instance.AddItem("Logs");
            }

            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.ShowInventoryUpdate("+3 Logs added to inventory", 2.0f);
            }
        }

        Destroy(gameObject);
    }
}
