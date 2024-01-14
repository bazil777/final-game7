using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//reference red overay, sets pulse to one and count of one, then checks player position
//if player position within the range will later activate the effect
public class EffectDarkZone : MonoBehaviour
{
    public Image darkZoneOverlay; 
    public float pulseDuration = 1f; 
    public int pulseCount = 1; 

    private Color originalColor;
    private Transform playerTransform; 
    private bool isInDarkZone = false;
//biome threshholds i created
    public float darkZoneXMin = 353.2f;
    public float darkZoneXMax = 420.4f;
    public float darkZoneZMin = 50.4f;
    public float darkZoneZMax = 117.8f;
//make layer transparent at start and check player position 
    void Start()
    {
        originalColor = darkZoneOverlay.color;
        darkZoneOverlay.gameObject.SetActive(false);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
//if player is in the darkzone then ill apply the effect however if they have a air charm 
//in the inventory start another effecg
    void Update()
        {
            if (playerTransform.position.x >= darkZoneXMin && playerTransform.position.x <= darkZoneXMax &&
                playerTransform.position.z >= darkZoneZMin && playerTransform.position.z <= darkZoneZMax)
            {
                if (!isInDarkZone && !Inventory.Instance.HasItem("Air Charm")) // I Check if player has the Air Charm
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
//while here continously run , wait between pulses
    private IEnumerator DarkZoneEffect()
    {
        while (isInDarkZone) 
        {
            darkZoneOverlay.gameObject.SetActive(true);

            for (int i = 0; i < pulseCount; i++)
            {
                yield return StartCoroutine(FadeOverlay(0, 0.5f, pulseDuration / 2));
                yield return StartCoroutine(FadeOverlay(0.5f, 0, pulseDuration / 2));
            }

            yield return new WaitForSeconds(pulseDuration); 
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
