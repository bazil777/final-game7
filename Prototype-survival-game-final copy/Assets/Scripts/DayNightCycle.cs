using UnityEngine;

//defining day, and morning angle, to differentiate when mornin and night
public class DayNightCycle : MonoBehaviour
{
    public float dayLengthInSeconds = 360; 
    private float rotationSpeed;

    
    public float morningAngle = 0; 
    // lcalculate rotation speed ie length of a daynight
    private void Start()
    {
        rotationSpeed = 360 / dayLengthInSeconds; // Calculate rotation speed
    }

    //rotates the light depending on the angle day and double check light points at centre
    private void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, rotationSpeed * Time.deltaTime);
        transform.LookAt(Vector3.zero); 
    }

    //finds angle needed for morning, if angle exceeds 180 its night so we need to rotate day
    public void SkipToMorning()
    {
        float currentAngle = transform.eulerAngles.x;
        float angleToRotate;
        if (currentAngle < 180)
        {
            if (currentAngle > morningAngle)
            {
                angleToRotate = (360 - currentAngle) + morningAngle;
            }
            else // If we are before morning, we just rotate to the morning angle
            {
                angleToRotate = morningAngle - currentAngle;
            }
        }
        else 
        {
            angleToRotate = (360 - currentAngle) + morningAngle;
        }

        // Rotates the light to the morning angle
        transform.RotateAround(Vector3.zero, Vector3.right, angleToRotate);
    }
}