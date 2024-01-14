using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject sword; // Assign your sword GameObject in the inspector
    public GameObject meatPrefab; // Assign your meat prefab GameObject in the inspector
    public float stabDistance = 0.5f; // Distance the sword moves forward for stab
    public float stabSpeed = 10f; // Speed of the stab motion
    private Vector3 swordOriginalPosition; // Original position of the sword

    void Start()
    {
        if (sword != null)
        {
            swordOriginalPosition = sword.transform.localPosition;
        }
    }

    void Update()
    {
        // Toggle sword on/off when 'P' key is pressed
        if (Input.GetKeyDown(KeyCode.Y))
        {
            sword.SetActive(!sword.activeSelf);
            if (sword.activeSelf)
            {
                sword.transform.localPosition = swordOriginalPosition; // Reset position when sword is activated
            }
        }

        // Perform a stabbing motion when 'U' key is pressed and the sword is active
        if (Input.GetKeyDown(KeyCode.U) && sword.activeSelf)
        {
            StartCoroutine(StabSword());
        }
    }

    IEnumerator StabSword()
    {
        // Move the sword forward
        float startTime = Time.time;
        while (Time.time < startTime + stabDistance / stabSpeed)
        {
            sword.transform.localPosition += sword.transform.forward * (stabSpeed * Time.deltaTime);
            yield return null;
        }

        // Check for bear in close proximity
        CheckForBear();

        // Return the sword to its original position
        sword.transform.localPosition = swordOriginalPosition;
    }

    void CheckForBear()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f); // 2f is the proximity range
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Bear")) // Ensure the bear has the tag "Bear"
            {
                HandleBearDeath(hitCollider.gameObject);
                break; // Affect only one bear per stab
            }
        }
    }

    void HandleBearDeath(GameObject bear)
    {
        if (meatPrefab != null)
        {
            Instantiate(meatPrefab, bear.transform.position, Quaternion.identity); // Instantiate meat at bear's position
        }
        Destroy(bear); // Destroy the bear
    }
}
