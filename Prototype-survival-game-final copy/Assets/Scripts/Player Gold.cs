using UnityEngine;
using System.Collections;

public class PlayerGold : MonoBehaviour
{
    public int currentGold = 50; // Initial gold value
    public int goldPerInterval = 20; // Amount of gold to add per interval
    public float interval = 10; // Time interval between adding gold

    private void Start()
    {
        // Start the coroutine to add gold periodically
        StartCoroutine(AddGoldPeriodically());
    }

    // Coroutine to add gold periodically 
    private IEnumerator AddGoldPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            AddGold(goldPerInterval);
        }
    }

    // Method to add gold
    public void AddGold(int goldAmount)
    {
        currentGold += goldAmount;
        if (currentGold < 0)
        {
            currentGold = 0; // Ensure gold doesn't go negative
        }
    }

    // Method to deduct gold
    public void DeductGold(int goldAmount)
    {
        currentGold -= goldAmount;
        if (currentGold < 0)
        {
            currentGold = 0; // Ensure gold doesn't go negative
        }
    }
}
