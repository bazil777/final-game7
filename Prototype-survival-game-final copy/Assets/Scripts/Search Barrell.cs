using UnityEngine;
using TMPro; // Make sure this is included to use TextMeshPro elements

public class BarrelSearch : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionText; // Now a serialized private field

    public GameObject crystalPrefab; // Reference to the crystal prefab.
    public float interactionRange = 3.0f; // Range within which player can interact with the barrel.
    public Transform playerTransform; // Reference to the player's transform.

    // Static variable to keep track of the active barrel
    private static BarrelSearch activeBarrel; 

    private void Awake()
    {
        // Ensure interactionText is hidden at start
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Early exit if interactionText is not assigned
        if (interactionText == null)
        {
            Debug.LogError("Interaction text not assigned.");
            return;
        }

        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= interactionRange)
        {
            if (activeBarrel != null && activeBarrel != this)
            {
                // There's already an active barrel, so don't show this one's text
                return;
            }

            // Set this barrel as the active one
            activeBarrel = this;
            interactionText.gameObject.SetActive(true);
            interactionText.text = "Press E to search";

            if (Input.GetKeyDown(KeyCode.E))
            {
                SearchBarrel();
            }
        }
        else if (activeBarrel == this)
        {
            // If player is out of range, deactivate text and clear active barrel
            interactionText.gameObject.SetActive(false);
            activeBarrel = null;
        }
    }

    private void SearchBarrel()
    {
        Debug.Log("Barrel searched!");

        // Instantiate crystal if assigned
        if (crystalPrefab != null)
        {
            Instantiate(crystalPrefab, transform.position, Quaternion.identity);
        }

        // Disable this barrel
        gameObject.SetActive(false);

        // If this was the active barrel, hide the text and clear the active barrel
        if (activeBarrel == this)
        {
            interactionText.gameObject.SetActive(false);
            activeBarrel = null;
        }
    }
}
