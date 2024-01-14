using UnityEngine;

public class BiomeDamage : MonoBehaviour
{
    //refenceing the gameobjects so can be edited and debugged in inspector
    public PlayerHealth playerHealth; 
    public BiomeOneCheck biomeChecker; 
    public float damageInterval = 1f; 
    private float damageTimer = 0f;

    //new update method that checks if a player is in snow or sarkzone biome and if they dont  have charm apply damage while there
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
