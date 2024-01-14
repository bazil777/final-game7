using UnityEngine;
using System.Collections;

//find player position, if within range checks E kep is pressed and will start animation to make item
public class CraftingTable : MonoBehaviour
{
    public float interactionRange = 6.0f;
    public Transform playerTransform;
    public GameObject hatchetPrefab; // Prefab for the hatchet
    public GameObject torchPrefab;    // Prefab for the torch
    public GameObject fork;           // GameObject for the visual representation of a fork
    public GameObject stone;          // GameObject for the visual representation of a stone

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance <= interactionRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed and player is within range.");
            StartCoroutine(CraftItem());
        }
    }

<<<<<<< HEAD
    private IEnumerator CraftItem()
=======
    //if the player tries to press E without needed items then don't craft, if they have it then craft and remove from their inventory
    private IEnumerator CraftHatchet()
>>>>>>> 030d312468b850f6a1e45789359b93d6160a8064
    {
        if (Inventory.Instance == null)
        {
            Debug.LogError("Inventory instance is not found.");
            yield break;
        }

        // Check for hatchet materials
        if (Inventory.Instance.HasItem("Fork") && Inventory.Instance.HasItem("Stone"))
        {
<<<<<<< HEAD
            Debug.Log("Crafting hatchet...");
            yield return CraftHatchet();
        }
        // Check for torch materials
        else if (Inventory.Instance.GetItemCount("Plastic Shard") >= 10 && Inventory.Instance.GetItemCount("Matches") >= 2)
        {
            Debug.Log("Crafting torch...");
            yield return CraftTorch();
=======
            Debug.Log("Fork and Stone found in inventory. Starting the crafting process.");

            Inventory.Instance.RemoveItem("Fork");
            Inventory.Instance.RemoveItem("Stone");

            // animation of crafting, spins for 5 seconds 
            Vector3 craftPosition = playerTransform.position + playerTransform.forward * 1.5f + Vector3.up * 1.0f;

            fork.SetActive(true);
            stone.SetActive(true);
            fork.transform.position = craftPosition;
            stone.transform.position = craftPosition + Vector3.up * 0.3f;
            Debug.Log("Starting spin animation.");
            float spinTime = 5.0f; 
            float timer = 0;
            while (timer <= spinTime)
            {
                fork.transform.Rotate(Vector3.up, 180 * Time.deltaTime);
                stone.transform.Rotate(Vector3.up, -180 * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }

            // gets rid of the items and instantiates the crafted weopon
            Debug.Log("Hiding fork and stone, and spawning hatchet.");
            fork.SetActive(false);
            stone.SetActive(false);
            Instantiate(hatchetPrefab, craftPosition, Quaternion.identity);

            Debug.Log("Hatchet crafting completed.");
>>>>>>> 030d312468b850f6a1e45789359b93d6160a8064
        }
        else
        {
            Debug.Log("Required items for crafting not found in inventory.");
        }
    }

    private IEnumerator CraftHatchet()
    {
        Inventory.Instance.RemoveItem("Fork");
        Inventory.Instance.RemoveItem("Stone");

        Vector3 craftPosition = playerTransform.position + playerTransform.forward * 1.5f + Vector3.up * 1.0f;
        fork.SetActive(true);
        stone.SetActive(true);
        fork.transform.position = craftPosition;
        stone.transform.position = craftPosition + Vector3.up * 0.3f;

        float spinTime = 5.0f;
        float timer = 0;
        while (timer <= spinTime)
        {
            fork.transform.Rotate(Vector3.up, 180 * Time.deltaTime);
            stone.transform.Rotate(Vector3.up, 180 * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        fork.SetActive(false);
        stone.SetActive(false);
        Instantiate(hatchetPrefab, craftPosition, Quaternion.identity);

        Debug.Log("Hatchet crafting completed.");
    }

    private IEnumerator CraftTorch()
    {
        // Remove items for torch crafting
        for (int i = 0; i < 10; i++)
        {
            Inventory.Instance.RemoveItem("Plastic Shard");
        }
        for (int i = 0; i < 2; i++)
        {
            Inventory.Instance.RemoveItem("Matches");
        }

        Vector3 craftPosition = playerTransform.position + playerTransform.forward * 1.5f + Vector3.up * 1.0f;
        GameObject temporaryTorch = Instantiate(torchPrefab, craftPosition, Quaternion.identity);
        temporaryTorch.SetActive(true);

        float spinTime = 5.0f;
        float timer = 0;
        while (timer <= spinTime)
        {
            temporaryTorch.transform.Rotate(Vector3.up, 180 * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(temporaryTorch);
        Instantiate(torchPrefab, craftPosition, Quaternion.identity);

        Debug.Log("Torch crafting completed.");
    }


}