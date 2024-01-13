using UnityEngine;
using TMPro;

public class BedSleep : MonoBehaviour
{
    public TextMeshProUGUI sleepText; // Reference to the UI text that will display the message.
    public float sleepRange = 2.0f; // The range within which the player can sleep in the bed.
    public Transform playerTransform; // Reference to the player's transform.
    public DayNightCycle dayNightCycle; // Reference to the DayNightCycle script.

    private void Start()
    {

        // Initially hide the sleep text
        sleepText.gameObject.SetActive(false);
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        // Debug.Log("Distance to bed: " + distance);

        if (distance <= sleepRange)
        {
            // Debug.Log("Player is within sleep range.");

            if (IsNightTime())
            {
                // Debug.Log("It is night time. Displaying sleep prompt.");
                sleepText.gameObject.SetActive(true);
                sleepText.text = "Press E to sleep in bed";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Player pressed E to sleep");
                    SleepTillMorning();
                }
            }
            else
            {
                // Debug.Log("It is not night time. Displaying 'not yet night' message.");
                sleepText.gameObject.SetActive(true);
                sleepText.text = "It is not yet night";
            }
        }
        else
        {
            // Debug.Log("Player is out of sleep range. Hiding sleep text.");
            sleepText.gameObject.SetActive(false);
        }
    }

    private bool IsNightTime()
    {
        float xRotation = dayNightCycle.transform.eulerAngles.x;
        bool isNight = xRotation > 180 && xRotation < 360;
        // Debug.Log("Is it night time? " + isNight);
        return isNight;
    }

    private void SleepTillMorning()
    {
        Debug.Log("Skipping to morning");
        dayNightCycle.SkipToMorning();
    }
}
