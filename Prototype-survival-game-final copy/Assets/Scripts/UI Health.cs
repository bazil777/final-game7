using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth; //refplayer script
    private TextMeshProUGUI healthText; 

    private void Start()
    {
        //debug (not needed anymore)
        // if (playerHealth == null)
        // {
        //     Debug.LogError("PlayerHealth script reference not set in HealthUI.");
        // }

        // Try to find a TextMeshProUGUI component on the current GameObject
        healthText = GetComponentInChildren<TextMeshProUGUI>();

        // If a TextMeshProUGUI component is not found
        if (healthText == null){
            healthText = gameObject.AddComponent<TextMeshProUGUI>();
            healthText.font = Resources.Load<TMP_FontAsset>("MyCustomFont"); // Load custom font (not made in prototype yet)
            healthText.fontSize = 24; 
            healthText.color = Color.white; //make font white
        }
    }

    //if playr has a health value then display as "Health: x"
    private void Update(){
        if (playerHealth != null && healthText != null){
            healthText.text = "Health: " + playerHealth.health.ToString();
        }
    }
}


