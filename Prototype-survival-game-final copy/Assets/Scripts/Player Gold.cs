using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    public int currentGold = 50; // Initial gold value

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
