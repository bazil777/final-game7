using UnityEngine;

//points to info popup, checks player distance and defines range to use
public class InfoPopUp : MonoBehaviour
{
    public GameObject infoPanel; 
    public Transform playerTransform; 
    public float activationDistance = 3f; 
//distance to the player , if within range and presses T activate the info panel
//deactivate the info panel if t pressed again or if player moves out of range
    // Set the info panel to inactive when the game starts
    private void Start()
    {
        infoPanel.SetActive(false);
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= activationDistance)
        {

            if (Input.GetKeyDown(KeyCode.T)) 
            {
                Debug.Log("T key pressed."); 
                infoPanel.SetActive(!infoPanel.activeSelf);

                if (infoPanel.activeSelf)
                {
                    Debug.Log("Info panel activated."); 
                }
                else
                {
                    Debug.Log("Info panel deactivated."); 
                }
            }
        }
    }
}
