using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EffectDarkZone : MonoBehaviour
{
    public Image darkZoneOverlay; // Assign your orange overlay UI Image in the inspector
    public float pulseDuration = 1f; // Duration of one pulse
    public int pulseCount = 5; // Number of pulses

    private Color originalColor;
    private Transform playerTransform; // Reference to the player's transform
    private bool isInDarkZone = false;

    public float darkZoneXMin = 353.2f;
    public float darkZoneXMax = 420.4f;
    public float darkZoneZMin = 50.4f;
    public float darkZoneZMax = 117.8f;

    void Start()
    {
        // Save the original color of the overlay
        originalColor = darkZoneOverlay.color;

        // Make the overlay fully transparent at start
        darkZoneOverlay.gameObject.SetActive(false);

        // Find the player in the scene
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
        {
            // Check if the player is within the dark zone area
            if (playerTransform.position.x >= darkZoneXMin && playerTransform.position.x <= darkZoneXMax &&
                playerTransform.position.z >= darkZoneZMin && playerTransform.position.z <= darkZoneZMax)
            {
                if (!isInDarkZone && !Inventory.Instance.HasItem("Air Charm")) // Check if player has the Air Charm
                {
                    isInDarkZone = true;
                    StartCoroutine(DarkZoneEffect());
                }
            }
            else if (isInDarkZone)
            {
                StopCoroutine(DarkZoneEffect());
                darkZoneOverlay.gameObject.SetActive(false);
                isInDarkZone = false;
            }
        }

    private IEnumerator DarkZoneEffect()
    {
        while (isInDarkZone) // Continuously run while in dark zone area
        {
            darkZoneOverlay.gameObject.SetActive(true);

            for (int i = 0; i < pulseCount; i++)
            {
                yield return StartCoroutine(FadeOverlay(0, 0.5f, pulseDuration / 2));
                yield return StartCoroutine(FadeOverlay(0.5f, 0, pulseDuration / 2));
            }

            yield return new WaitForSeconds(pulseDuration); // Wait for a bit before the next pulse starts
        }

        darkZoneOverlay.gameObject.SetActive(false);
    }

    private IEnumerator FadeOverlay(float startAlpha, float endAlpha, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            darkZoneOverlay.color = Color.Lerp(
                new Color(originalColor.r, originalColor.g, originalColor.b, startAlpha),
                new Color(originalColor.r, originalColor.g, originalColor.b, endAlpha),
                time / duration
            );
            time += Time.deltaTime;
            yield return null;
        }
    }
}
