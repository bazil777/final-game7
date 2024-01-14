using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 60; // Initial health value
    public Material deathMaterial; // Material used when health reaches 0
    public int maxHealth = 100; // Maximum health value
    public HealingScreenEffect healingScreenEffect; // Reference to the healing screen effect script

    private Renderer capsuleRenderer;
    private Material originalMaterial;

    private void Start()
    {
        capsuleRenderer = GetComponent<Renderer>();
        originalMaterial = capsuleRenderer.material;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            health = 0;
            OnPlayerDeath();
        }
        UpdateMaterial();
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        UpdateMaterial();
    }

    public void IncrementHealth(int increment)
    {
        health += increment;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        UpdateMaterial();
    }

    public void StartHealingProcess()
    {
        if (healingScreenEffect != null)
        {
            healingScreenEffect.StartHealingEffect(this);
        }
        else
        {
            Debug.LogWarning("HealingScreenEffect is not assigned in the inspector.");
        }
    }

    private void OnPlayerDeath()
    {
        capsuleRenderer.material = deathMaterial;
        // Additional death logic here
    }

    private void UpdateMaterial()
    {
        capsuleRenderer.material = originalMaterial;
    }
}

