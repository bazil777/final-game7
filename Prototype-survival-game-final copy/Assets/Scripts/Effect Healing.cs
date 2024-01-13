using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealingScreenEffect : MonoBehaviour
{
    public Image healingOverlay; // UI Image for healing effect
    public float pulseDuration = 1f; // Duration of one pulse
    public int pulseCount = 1; // Number of pulses

    private Color originalColor;

    void Start()
    {
        // Save the original color of the overlay
        originalColor = healingOverlay.color;

        // Make the overlay fully transparent at start
        healingOverlay.gameObject.SetActive(false);
    }

    public void StartHealingEffect(PlayerHealth playerHealth)
    {
        Debug.Log("HealingScreenEffect: StartHealingEffect() called.");

        // Check if player's health is less than maximum before starting effect
        if (playerHealth != null && playerHealth.health < playerHealth.maxHealth)
        {
            StartCoroutine(HealEffect(playerHealth));
        }
        else
        {
            Debug.Log("Player health is already at maximum, no healing effect needed.");
        }
    }

    private IEnumerator HealEffect(PlayerHealth playerHealth)
    {
        Debug.Log("HealingScreenEffect: HealEffect coroutine started.");
        healingOverlay.gameObject.SetActive(true);

        for (int i = 0; i < pulseCount; i++)
        {
            // Increment health by 10 each pulse
            if (playerHealth != null)
            {
                playerHealth.IncrementHealth(10);
            }

            yield return StartCoroutine(FadeHealingOverlay(0, 0.5f, pulseDuration / 2));
            yield return StartCoroutine(FadeHealingOverlay(0.5f, 0, pulseDuration / 2));
        }

        healingOverlay.gameObject.SetActive(false);
    }

    private IEnumerator FadeHealingOverlay(float startAlpha, float endAlpha, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            healingOverlay.color = Color.Lerp(
                new Color(originalColor.r, originalColor.g, originalColor.b, startAlpha),
                new Color(originalColor.r, originalColor.g, originalColor.b, endAlpha),
                time / duration
            );
            time += Time.deltaTime;
            yield return null;
        }
    }
}
