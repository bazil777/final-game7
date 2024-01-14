using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//assign overlay , how long it will last and how many times
//check player reference and then set the thresholds
public class HealingScreenEffect : MonoBehaviour
{
    public Image healingOverlay; 
    public float pulseDuration = 1f; 
    public int pulseCount = 1; 

    private Color originalColor;

    void Start()
    {
        originalColor = healingOverlay.color;

        healingOverlay.gameObject.SetActive(false);
    }
//debugging
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
//if successful then start green pulse effect and increment health by 10
    private IEnumerator HealEffect(PlayerHealth playerHealth)
    {
        Debug.Log("HealingScreenEffect: HealEffect coroutine started.");
        healingOverlay.gameObject.SetActive(true);

        for (int i = 0; i < pulseCount; i++)
        {
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
