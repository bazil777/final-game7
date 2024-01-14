using UnityEngine;
using TMPro;

//will assign this to an game object that manages activating and closing texts
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public TextMeshProUGUI inventoryUpdateText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (inventoryUpdateText != null)
        {
            inventoryUpdateText.gameObject.SetActive(false);
        }
    }

    public void ShowInventoryUpdate(string message, float duration)
    {
        inventoryUpdateText.text = message;
        inventoryUpdateText.gameObject.SetActive(true);
        Invoke(nameof(HideInventoryUpdateMessage), duration);
    }

    private void HideInventoryUpdateMessage()
    {
        inventoryUpdateText.gameObject.SetActive(false);
    }
}
