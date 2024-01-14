using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//assign overlay , how long it will last and how many times
public class EffectSnow : MonoBehaviour
{
    public Image snowOverlay; 
    public float pulseDuration = 1f; 
    public int pulseCount = 1; 
//check player reference and then set the thresholds
    private Color originalColor;
    private Transform playerTransform; 
    private bool isInSnowArea = false;

    public float snowXMin = 352.0f;
    public float snowXMax = 848f;
    public float snowZMin = -527f;
    public float snowZMax = -30.66f;

    void Start()
    {
        // Save the original color of the overlay
        originalColor = snowOverlay.color;

        // make it so its invisible to player
        snowOverlay.gameObject.SetActive(false);

        // Find controller coordinates
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
// Check if the player is within the snow area, if so start the effect using coroutine to make it last for longer
//if player not in snow area then set the overlay to false
    void Update()
    {
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
//runs this while in snow area, waits for a few moments between pulses
    private IEnumerator SnowEffect()
    {
        while (isInSnowArea) 
        {
            snowOverlay.gameObject.SetActive(true);

            for (int i = 0; i < pulseCount; i++)
            {
                yield return StartCoroutine(FadeSnowOverlay(0, 0.5f, pulseDuration / 2));
                yield return StartCoroutine(FadeSnowOverlay(0.5f, 0, pulseDuration / 2));
            }

            yield return new WaitForSeconds(pulseDuration); 
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
