using UnityEngine;
using System.Collections;

public class CraftingTable : MonoBehaviour
{
    public float interactionRange = 6.0f;
    public Transform playerTransform;
    public GameObject hatchetPrefab;
    public GameObject fork;
    public GameObject stone;

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance <= interactionRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed and player is within range.");
            StartCoroutine(CraftHatchet());
        }
    }

    private IEnumerator CraftHatchet()
    {
        if (Inventory.Instance == null)
        {
            Debug.LogError("Inventory instance is not found.");
            yield break;
        }

        if (Inventory.Instance.HasItem("Fork") && Inventory.Instance.HasItem("Stone"))
        {
            Debug.Log("Fork and Stone found in inventory. Starting the crafting process.");

            Inventory.Instance.RemoveItem("Fork");
            Inventory.Instance.RemoveItem("Stone");

            // Adjusted position to be higher and closer to the player
            Vector3 craftPosition = playerTransform.position + playerTransform.forward * 1.5f + Vector3.up * 1.0f;

            // Activate and position fork and stone
            fork.SetActive(true);
            stone.SetActive(true);
            fork.transform.position = craftPosition;
            stone.transform.position = craftPosition + Vector3.up * 0.3f;

            // Start spin animation
            Debug.Log("Starting spin animation.");
            float spinTime = 5.0f; // Increased spin time for visibility
            float timer = 0;
            while (timer <= spinTime)
            {
                fork.transform.Rotate(Vector3.up, 180 * Time.deltaTime);
                stone.transform.Rotate(Vector3.up, -180 * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }

            // Hide fork and stone, spawn hatchet
            Debug.Log("Hiding fork and stone, and spawning hatchet.");
            fork.SetActive(false);
            stone.SetActive(false);
            Instantiate(hatchetPrefab, craftPosition, Quaternion.identity);

            Debug.Log("Hatchet crafting completed.");
        }
        else
        {
            Debug.Log("Required items for crafting not found in inventory.");
        }
    }
}
