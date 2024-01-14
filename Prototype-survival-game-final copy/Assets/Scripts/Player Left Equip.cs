using UnityEngine;

public class PlayerLeftEquipItem : MonoBehaviour
{
    public GameObject torchGameObject; // Assign the parent torch GameObject in the inspector
    public Transform leftHandTransform; // Assign the left hand (cube) transform in the inspector

    private Light torchLight; // Reference to the Light component on the torch's child
    private bool isTorchEquipped = false;

    void Start()
    {
        if (torchGameObject != null)
        {
            // Find the "Torch Light" child and get the Light component
            Transform torchLightTransform = torchGameObject.transform.Find("Torch Light");
            if (torchLightTransform != null)
            {
                torchLight = torchLightTransform.GetComponent<Light>();
            }

            // Initially deactivate the torch GameObject
            torchGameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Equip torch if the player has it and it's not already equipped
        if (Inventory.Instance.HasItem("Torch") && !isTorchEquipped)
        {
            EquipTorch();
        }

        // Toggle the torch light with 'T' key
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleTorchLight();
        }
    }

    private void EquipTorch()
    {
        // Set the torch at the hand's position and rotation
        torchGameObject.transform.position = leftHandTransform.position;
        torchGameObject.transform.rotation = leftHandTransform.rotation;

        // Parent the torch to the hand
        torchGameObject.transform.SetParent(leftHandTransform, worldPositionStays: false);

        // Adjust local position and rotation for correct placement in hand
        torchGameObject.transform.localPosition = new Vector3(0, 0, 0); // Adjust these values as needed
        torchGameObject.transform.localEulerAngles = new Vector3(0, 0, 0); // Adjust these values as needed

        torchGameObject.SetActive(true);
        isTorchEquipped = true;
    }

    private void ToggleTorchLight()
    {
        if (torchLight != null)
        {
            torchLight.enabled = !torchLight.enabled;
        }
    }
}
