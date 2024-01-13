using UnityEngine;

public class InfoPopUp : MonoBehaviour
{
    public GameObject infoPanel; // Assign your info panel in the inspector
    public Transform playerTransform; // Assign your player's transform in the inspector
    public float activationDistance = 3f; // Distance within which the player can activate the panel

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        // Debug.Log("Distance to player: " + distance); // Log the distance to the player

        if (distance <= activationDistance)
        {
            // Debug.Log("Player is within activation distance."); // Log when player is within range

            if (Input.GetKeyDown(KeyCode.T)) // Use 'T' key instead of 'E'
            {
                Debug.Log("T key pressed."); // Log when T key is pressed
                infoPanel.SetActive(!infoPanel.activeSelf);

                if (infoPanel.activeSelf)
                {
                    Debug.Log("Info panel activated."); // Log when the panel is activated
                }
                else
                {
                    Debug.Log("Info panel deactivated."); // Log when the panel is deactivated
                }
            }
        }
        else if (infoPanel.activeSelf)
        {
            infoPanel.SetActive(false);
            Debug.Log("Player moved out of range, info panel deactivated."); // Log when the player moves out of range and the panel is deactivated
        }
    }
}
