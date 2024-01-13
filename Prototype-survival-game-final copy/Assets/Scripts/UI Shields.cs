using UnityEngine;
using TMPro;

public class ShieldUI : MonoBehaviour{
    public ShieldController shieldController;
    private TextMeshProUGUI shieldText;

    [Header("Text Settings")]
    public Color activeColor = Color.blue; // active shields are blue
    public Color inactiveColor = Color.grey; // inactive shield will be grey

    private void Start(){
        //debug (I dont need anymore but might be needed going forward)
        // if (shieldController == null)
        // {
        //     Debug.LogError("ShieldController script reference not set in ShieldUI.");
        // }

        shieldText = GetComponentInChildren<TextMeshProUGUI>();

        if (shieldText == null){
            shieldText = gameObject.AddComponent<TextMeshProUGUI>();
            shieldText.font = Resources.Load<TMP_FontAsset>("YourCustomFontName");
            shieldText.fontSize = 24;
        }
    }

    private void Update() {
        //if player has shields value display and get the value to display
        if (shieldController != null && shieldText != null){
            shieldText.text = "Shields: " + shieldController.shieldAmount.ToString();

            // if no shield
            if (shieldController.shieldAmount == 0){
                shieldText.color = inactiveColor; // Changes the colour to inactive shields
            }
            else{
                shieldText.color = activeColor; // Change the font colior to active shield
            }
        }
    }
}
