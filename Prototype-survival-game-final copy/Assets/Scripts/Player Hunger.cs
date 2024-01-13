using UnityEngine;
using UnityEngine.UI; // For UI Text
using TMPro; // Uncomment this if using TextMeshPro

public class PlayerHunger : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public TextMeshProUGUI hungerText; // Use this if you're using TextMeshPro

    public int maxHunger = 100;
    public float hungerDecreaseInterval = 5f;
    public int hungerDamage = 1;
    public int criticalHungerLevel = 0;

    private int currentHunger;
    private float hungerTimer = 0f;

    void Start()
    {
        currentHunger = maxHunger;
        UpdateHungerDisplay();
    }

    void Update()
    {
        hungerTimer += Time.deltaTime;

        if (hungerTimer >= hungerDecreaseInterval)
        {
            DecreaseHunger(1);
            hungerTimer = 0f;

            if (currentHunger < criticalHungerLevel)
            {
                playerHealth.TakeDamage(hungerDamage);
            }
        }
    }

    public void DecreaseHunger(int amount)
    {
        currentHunger -= amount;
        currentHunger = Mathf.Max(currentHunger, 0);
        UpdateHungerDisplay();
    }

    public void IncreaseHunger(int amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Min(currentHunger, maxHunger);
        UpdateHungerDisplay();
    }

    private void UpdateHungerDisplay()
    {
        if (hungerText != null)
        {
            hungerText.text = "Hunger: " + currentHunger;
        }
    }
}
