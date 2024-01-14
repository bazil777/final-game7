using UnityEngine;
using System.Collections;

public class PlayerEquipment : MonoBehaviour
{
    public Transform handTransform; // Assign the hand bone or GameObject in the Inspector
    public GameObject hatchetPrefab; // Assign the hatchet prefab in the Inspector
    public GameObject blockToDisappear; // Assign this in the Inspector

    private GameObject currentHatchet; // To keep track of the instantiated hatchet

    void Update()
    {
        // Update the equipment based on the inventory
        EquipHatchet();

        // Check for swinging the hatchet
        if (Input.GetKeyDown(KeyCode.R) && currentHatchet != null)
        {
            StartCoroutine(SwingHatchet());
        }
    }

    private void EquipHatchet()
    {
        if (Inventory.Instance.HasItem("Hatchet"))
        {
            if (currentHatchet == null)
            {
                Debug.Log("Instantiating hatchet in hand.");
                currentHatchet = Instantiate(hatchetPrefab, handTransform);
                currentHatchet.transform.localPosition = Vector3.zero; // Adjust as needed
                currentHatchet.transform.localRotation = Quaternion.identity; // Adjust as needed
                currentHatchet.transform.localScale = new Vector3(10, 10, 10); // Further increase the scale

                // Make the block disappear
                if (blockToDisappear != null)
                {
                    blockToDisappear.SetActive(false);
                }
            }
        }
        else
        {
            if (currentHatchet != null)
            {
                Debug.Log("Removing hatchet from hand.");
                Destroy(currentHatchet);

                // Make the block reappear
                if (blockToDisappear != null)
                {
                    blockToDisappear.SetActive(true);
                }
            }
        }
    }

    private IEnumerator SwingHatchet()
    {
        // Swing forward and down, then back up
        float swingTime = 1.0f; // Time for the full swing
        float downAngle = 45f; // Angle to swing the hatchet down
        float upAngle = 45f; // Angle to swing the hatchet back up

        // Swing Down (Forward)
        float timer = 0;
        while (timer <= swingTime / 2)
        {
            // Rotate forward and a bit downward
            float step = (downAngle * (Time.deltaTime / (swingTime / 2)));
            currentHatchet.transform.RotateAround(handTransform.position, handTransform.forward, step);
            timer += Time.deltaTime;
            yield return null;
        }

        // Swing Up (Back to Original Position)
        timer = 0;
        while (timer <= swingTime / 2)
        {
            // Rotate back to the original position
            float step = (upAngle * (Time.deltaTime / (swingTime / 2)));
            currentHatchet.transform.RotateAround(handTransform.position, handTransform.forward, -step);
            timer += Time.deltaTime;
            yield return null;
        }

        // Reset the hatchet's rotation to its initial state
        currentHatchet.transform.localRotation = Quaternion.identity;

        // Wait for the swing to complete
        yield return new WaitForSeconds(swingTime);

        // Check for trees in range after swinging
        Collider[] hitColliders = Physics.OverlapSphere(handTransform.position, 4f); // 3f is the range
        bool treeInRange = false;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Tree")) // Make sure trees have the tag "Tree"
            {
                float distanceToTree = Vector3.Distance(handTransform.position, hitCollider.transform.position);
                Debug.Log("Tree in range. Distance to tree: " + distanceToTree);
                TreeChop tree = hitCollider.GetComponent<TreeChop>();
                if (tree != null)
                {
                    tree.ChopDown();
                }
                treeInRange = true;
                break; // Assuming you only want to chop one tree per swing
            }
        }
        

        if (!treeInRange)
        {
            Debug.Log("No tree in range.");
        }
    }
}
