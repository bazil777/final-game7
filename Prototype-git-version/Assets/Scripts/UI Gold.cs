using UnityEngine;
using TMPro;

//This script will handle the UI for Gold/money
public class UIGold : MonoBehaviour
{
    public PlayerGold playerGold;
    private TextMeshProUGUI goldText;

    private void Start(){
        goldText = GetComponentInChildren<TextMeshProUGUI>();

        //just in case theres no set text then just display it in whtie and size 24
        if (goldText == null){
            goldText = gameObject.AddComponent<TextMeshProUGUI>();
            goldText.fontSize = 24;
            goldText.color = Color.white;
        }
    }
    //if theres some gold value (even 0) then display "gold : *use player gold to get his godl to display here"
    private void Update(){
        if (playerGold != null && goldText != null){
            goldText.text = "Gold: " + playerGold.currentGold.ToString();
        }
    }
}
