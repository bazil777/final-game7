using UnityEngine;

[RequireComponent(typeof(Renderer))] // This ensures there's a Renderer component on the GameObject.
public class ColorAdder : MonoBehaviour
{
    public Color color = Color.white; // Default color is set to white

    // Start is called before the first frame update
    void Start()
    {
        ApplyColor();
    }

    // This method updates the color of the object's material
    public void UpdateColor(Color newColor)
    {
        Renderer renderer = GetComponent<Renderer>();

        // Create an instance of the material for this object
        Material materialInstance = new Material(renderer.sharedMaterial);
        renderer.material = materialInstance;

        // Check if material has a color property named "_Color"
        if (materialInstance.HasProperty("_Color"))
        {
            materialInstance.color = newColor;
        }
        else
        {
            Debug.LogError("Material does not have a _Color property.");
        }
    }

    // This method applies the selected color to the material
    public void ApplyColor()
    {
        UpdateColor(color);
    }

    // This context menu item allows you to apply the color in the editor
    [ContextMenu("Apply Color")]
    private void ApplyColorContextMenu()
    {
        ApplyColor();
    }
}
