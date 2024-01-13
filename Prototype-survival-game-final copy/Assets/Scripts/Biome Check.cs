using UnityEngine;
using TMPro;

//assigns GUI, controller position, and ref lighting manager to later edit lighting based off area
public class BiomeOneCheck : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public Transform playerTransform;
    public LightingManager lightingManager; 

    //All my biome areas to use later calculate (made public so i can debug later)
    public float biomeXThreshold = 350f;
    public float darkZoneXMin = 352.7f;
    public float darkZoneXMax = 420.4f;
    public float darkZoneZMin = 50.4f;
    public float darkZoneZMax = 117.8f;
    public float snowXMin = 352.0f;
    public float snowXMax = 848f;     
    public float snowZMin = -527f;    
    public float snowZMax = -30.66f;  
    public float mudXMin = 424.3f;
    public float mudXMax = 671f;
    public float mudZMin = -28f;
    public float mudZMax = 222f;

    //given each biome a number value to manage later
    public enum BiomeType
    {
        None = 0,
        Forest = 1,
        DarkZone = 4,
        Snow = 2,
        Mud = 3
    }

    //ill access the current biome , then check player transform and change lighting based off this
    public BiomeType CurrentBiome { get; private set; } 

    private void Start()
    {
        SetupMessageText();
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            return;
        }

        CurrentBiome = CheckBiome();
        if (lightingManager != null)
        {
            lightingManager.UpdateLighting(CurrentBiome);
        }
    }
    //based of player position ill return to the UI a message displaying biome, if certain chamrs have effect then this will be known too 
    private BiomeType CheckBiome()
    {
        float playerXPosition = playerTransform.position.x;
        float playerZPosition = playerTransform.position.z;

        if (playerXPosition < biomeXThreshold)
        {
            DisplayMessage("You are in the Forest", Color.green);
            return BiomeType.Forest;
        }
        else if (playerXPosition >= darkZoneXMin && playerXPosition <= darkZoneXMax &&
             playerZPosition >= darkZoneZMin && playerZPosition <= darkZoneZMax)
        {
            if (Inventory.Instance.HasItem("Air Charm"))
            {
                DisplayMessage("You are in the Dark Zone, your air charm is protecting you from poison effects", Color.magenta);
            }
            else
            {
                DisplayMessage("You are in the Dark Zone, the air is poisonous!", Color.magenta);
            }
            return BiomeType.DarkZone;
        }
        else if (playerXPosition >= snowXMin && playerXPosition <= snowXMax &&
                 playerZPosition >= snowZMin && playerZPosition <= snowZMax)
        {
            DisplayMessage("You are too cold, losing health !", Color.blue);
            return BiomeType.Snow;
        }
        else if (playerXPosition >= mudXMin && playerXPosition <= mudXMax &&
                 playerZPosition >= mudZMin && playerZPosition <= mudZMax)
        {
            DisplayMessage("You are in the Desert biome", new Color(0.6f, 0.4f, 0.2f, 1f)); // Custom brown color
            return BiomeType.Mud;
        }

        DisplayMessage("You are in the spawn room", Color.red);
        return BiomeType.None;
    }

    //I have set up the colour of each biome message here rather in the inspector cause i was having issues that way.
    private void DisplayMessage(string message, Color color)
    {
        messageText.text = message;
        messageText.color = color;
    }

    private void SetupMessageText()
    {
        messageText.fontSize = 24;
        messageText.alignment = TextAlignmentOptions.Center;
        messageText.enableWordWrapping = true;

        RectTransform rt = messageText.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = new Vector2(0, 150);
    }
}
