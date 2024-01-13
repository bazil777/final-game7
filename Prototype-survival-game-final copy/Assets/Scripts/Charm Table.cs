using UnityEngine;
using System.Collections;

public class CharmTable : MonoBehaviour
{
    public float interactionRange = 4.0f;
    public Transform playerTransform;
    public GameObject airCharmPrefab;
    public GameObject logs;
    public GameObject crystal;
    public GameObject racket;

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance <= interactionRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed and player is within range.");
            StartCoroutine(CraftAirCharm());
        }
    }

    private IEnumerator CraftAirCharm()
    {
        if (Inventory.Instance == null)
        {
            Debug.LogError("Inventory instance is not found.");
            yield break;
        }

        if (Inventory.Instance.HasItem("Logs") && Inventory.Instance.HasItem("Crystal") && Inventory.Instance.HasItem("Racket"))
        {
            Debug.Log("Logs, Crystal, and Racket found in inventory. Starting the charm crafting process.");

            Inventory.Instance.RemoveItem("Logs");
            Inventory.Instance.RemoveItem("Crystal");
            Inventory.Instance.RemoveItem("Racket");

            Vector3 craftPosition = playerTransform.position + playerTransform.forward * 1.5f + Vector3.up * 1.0f;

            // Activate and position logs, crystal, and racket
            logs.SetActive(true);
            crystal.SetActive(true);
            racket.SetActive(true);
            logs.transform.position = craftPosition;
            crystal.transform.position = craftPosition + Vector3.up * 0.3f;
            racket.transform.position = craftPosition + Vector3.up * 0.6f;

            // Start spin animation
            Debug.Log("Starting spin animation for charm crafting.");
            float spinTime = 5.0f;
            float timer = 0;
            while (timer <= spinTime)
            {
                logs.transform.Rotate(Vector3.up, 180 * Time.deltaTime);
                crystal.transform.Rotate(Vector3.up, 180 * Time.deltaTime);
                racket.transform.Rotate(Vector3.up, -180 * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }

            // Hide logs, crystal, and racket, spawn air charm
            Debug.Log("Hiding crafting items and spawning air charm.");
            logs.SetActive(false);
            crystal.SetActive(false);
            racket.SetActive(false);
            Instantiate(airCharmPrefab, craftPosition, Quaternion.identity);

            Debug.Log("Air charm crafting completed.");
        }
        else
        {
            Debug.Log("Required items for crafting not found in inventory.");
        }
    }
}
