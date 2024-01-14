using UnityEngine;
using TMPro; 

public class BarrelSearch : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionText; 

    public GameObject crystalPrefab; // Reference to the crystal prefab to drop on search
    public float interactionRange = 3.0f; // ive set a good rane so player can interact with barrel
    public Transform playerTransform; // check the player position

    private static BarrelSearch activeBarrel; 

    private void Awake()
    {
        // hide text at start
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // debuugging
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
// Instantiate item if assigned once broken
    private void SearchBarrel()
    {
        Debug.Log("Barrel searched!");

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
