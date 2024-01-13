using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EffectSnow : MonoBehaviour
{
    public Image snowOverlay; // Assign your blue overlay UI Image in the inspector
    public float pulseDuration = 1f; // Duration of one pulse
    public int pulseCount = 1; // Number of pulses

    private Color originalColor;
    private Transform playerTransform; // Reference to the player's transform
    private bool isInSnowArea = false;

    public float snowXMin = 352.0f;
    public float snowXMax = 848f;
    public float snowZMin = -527f;
    public float snowZMax = -30.66f;

    void Start()
    {
        // Save the original color of the overlay
        originalColor = snowOverlay.color;

        // Make the overlay fully transparent at start
        snowOverlay.gameObject.SetActive(false);

        // Find the player in the scene
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check if the player is within the snow area
        if (playerTransform.position.x >= snowXMin && playerTransform.position.x <= snowXMax &&
            playerTransform.position.z >= snowZMin && playerTransform.position.z <= snowZMax)
        {
            if (!isInSnowArea)
            {
                isInSnowArea = true;
                StartCoroutine(SnowEffect());
            }
        }
        else if (isInSnowArea)
        {
            StopCoroutine(SnowEffect());
            snowOverlay.gameObject.SetActive(false);
            isInSnowArea = false;
        }
    }

    private IEnumerator SnowEffect()
    {
        while (isInSnowArea) // Continuously run while in snow area
        {
            snowOverlay.gameObject.SetActive(true);

            for (int i = 0; i < pulseCount; i++)
            {
                yield return StartCoroutine(FadeSnowOverlay(0, 0.5f, pulseDuration / 2));
                yield return StartCoroutine(FadeSnowOverlay(0.5f, 0, pulseDuration / 2));
            }

            yield return new WaitForSeconds(pulseDuration); // Wait for a bit before the next pulse starts
        }

        snowOverlay.gameObject.SetActive(false);
    }

    private IEnumerator FadeSnowOverlay(float startAlpha, float endAlpha, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            snowOverlay.color = Color.Lerp(
                new Color(originalColor.r, originalColor.g, originalColor.b, startAlpha),
                new Color(originalColor.r, originalColor.g, originalColor.b, endAlpha),
                time / duration
            );
            time += Time.deltaTime;
            yield return null;
        }
    }
}
