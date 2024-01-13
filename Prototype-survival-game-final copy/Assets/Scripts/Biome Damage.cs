using UnityEngine;

public class BiomeDamage : MonoBehaviour
{
    public PlayerHealth playerHealth; // Reference to the PlayerHealth script
    public BiomeOneCheck biomeChecker; // Reference to the BiomeOneCheck script
    public float damageInterval = 1f; // Time in seconds between each damage tick
    private float damageTimer = 0f;

    private void Update()
    {
        if (biomeChecker == null) return;

        damageTimer += Time.deltaTime;

        if (damageTimer >= damageInterval)
        {
            if (biomeChecker.CurrentBiome == BiomeOneCheck.BiomeType.Snow)
            {
                playerHealth.TakeDamage(1); // Apply 1 damage in Snow biome
            }
            else if (biomeChecker.CurrentBiome == BiomeOneCheck.BiomeType.DarkZone)
            {
                // Check if the player has the Air Charm to prevent damage
                if (!Inventory.Instance.HasItem("Air Charm"))
                {
                    playerHealth.TakeDamage(2); // Apply 2 damage in Dark Zone biome
                }
            }
            
            damageTimer = 0f;
        }
    }
}
