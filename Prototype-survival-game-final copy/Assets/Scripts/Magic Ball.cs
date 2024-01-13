using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public float healingRadius = 3f; // Radius player is affected
    public int healingAmount = 5; // Amount of health to restore on contact x2
    public int goldDeductionAmount = 10; // Amount of gold to deduct on contact x2
    public float healingInterval = 1f; // Time interval between healing 

    private float nextHealTime;

    // Reference to the HealingScreenEffect script
    public HealingScreenEffect healingScreenEffect;

    private void Start()
    {
        nextHealTime = Time.time + healingInterval;
    }

    private void Update()
    {
        if (Time.time >= nextHealTime)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, healingRadius);

            foreach (Collider collider in colliders)
            {
                PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                PlayerGold playerGold = collider.GetComponent<PlayerGold>();

                if (playerHealth != null && playerHealth.health < playerHealth.maxHealth)
                {
                    // Heal the player and start the pulsing effect
                    playerHealth.IncrementHealth(healingAmount); // Use IncrementHealth for pulsing

                    if (healingScreenEffect != null)
                    {
                        healingScreenEffect.StartHealingEffect(playerHealth);
                    }
                    else
                    {
                        Debug.LogWarning("HealingScreenEffect is not assigned in the inspector.");
                    }

                    if (playerGold != null)
                    {
                        playerGold.DeductGold(goldDeductionAmount);
                    }
                }
            }

            nextHealTime = Time.time + healingInterval;
        }
    }
}
