using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayLengthInSeconds = 360; // Length of a day in real-time seconds
    private float rotationSpeed;

    // Define what rotation angle is considered morning
    public float morningAngle = 0; // Assuming 0 is the rotation at morning

    private void Start()
    {
        rotationSpeed = 360 / dayLengthInSeconds; // Calculate rotation speed
    }

    private void Update()
    {
        // Rotate the light every frame
        transform.RotateAround(Vector3.zero, Vector3.right, rotationSpeed * Time.deltaTime);
        transform.LookAt(Vector3.zero); // Ensure the light points to the center
    }

    public void SkipToMorning()
    {
        // Calculate the angle to rotate from the current position to the morning position
        float currentAngle = transform.eulerAngles.x;
        float angleToRotate;

        // If the current angle is less than 180, it means we are in the first half of the day (morning to afternoon)
        if (currentAngle < 180)
        {
            // If we are already past morning, we need to complete the day and start a new one
            if (currentAngle > morningAngle)
            {
                angleToRotate = (360 - currentAngle) + morningAngle;
            }
            else // If we are before morning, we just rotate to the morning angle
            {
                angleToRotate = morningAngle - currentAngle;
            }
        }
        else // If the current angle is more than 180, we are in the second half of the day (afternoon to night)
        {
            angleToRotate = (360 - currentAngle) + morningAngle;
        }

        // Rotate the light to morning
        transform.RotateAround(Vector3.zero, Vector3.right, angleToRotate);
    }
}