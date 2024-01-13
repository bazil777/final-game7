using UnityEngine;
using TMPro;

//reference UI, interaction range , player position and the daynight script
public class BedSleep : MonoBehaviour
{
    public TextMeshProUGUI sleepText; 
    public float sleepRange = 2.0f; 
    public Transform playerTransform; 
    public DayNightCycle dayNightCycle; 

    //set UI sleep text to false so doesn't appear at start of game, then checks distance to bed, if in range and player 
    //presses E then call sleep till morning method to skip night
    private void Start()
    {
        sleepText.gameObject.SetActive(false);
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
       

        if (distance <= sleepRange)
        {
            if (IsNightTime())
            {
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
                sleepText.gameObject.SetActive(true);
                sleepText.text = "It is not yet night";
            }
        }
        else
        {
            sleepText.gameObject.SetActive(false);
        }
    }
    ///check if night, call skip to morning from daynightcycle
    private bool IsNightTime()
    {
        float xRotation = dayNightCycle.transform.eulerAngles.x;
        bool isNight = xRotation > 180 && xRotation < 360;
        return isNight;
    }

    private void SleepTillMorning()
    {
        Debug.Log("Skipping to morning");
        dayNightCycle.SkipToMorning();
    }
}
