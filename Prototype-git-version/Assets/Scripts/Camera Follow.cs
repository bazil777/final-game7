using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2.0f, 5f); // camera angle 

    void LateUpdate() // ensures the camera follows after the capsule's rotation.
    {
        if (target != null)
        {
            // psotion of the camera
            Vector3 desiredPosition = target.position - target.forward * offset.z + target.up * offset.y;

            // Interpolate the current position with the desired position for smooth movement.
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);

            // Make the camera look at the target's position.
            transform.LookAt(target);
        }
    }
}

