using UnityEngine;


//NOTE : i found an easier way to do this and this script is only used on tree objects 
//set defualt colour towhite , then update object material if objct has color property
[RequireComponent(typeof(Renderer))] 
public class ColorAdder : MonoBehaviour
{
    public Color color = Color.white; 

    // I calll the mothod straight away
    void Start()
    {
        ApplyColor();
    }

    // This method updates the color of the object's material
    public void UpdateColor(Color newColor)
    {
        Renderer renderer = GetComponent<Renderer>();
        Material materialInstance = new Material(renderer.sharedMaterial);
        renderer.material = materialInstance;

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
    [ContextMenu("Apply Color")]
    private void ApplyColorContextMenu()
    {
        ApplyColor();
    }
}
