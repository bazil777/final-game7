using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public Light mainLight; // Assign the main directional light in the inspector

    public void UpdateLighting(BiomeOneCheck.BiomeType biome)
    {
        switch (biome)
        {
            case BiomeOneCheck.BiomeType.Forest:
                SetForestLighting();
                break;
            case BiomeOneCheck.BiomeType.Snow:
                SetSnowLighting();
                break;
            case BiomeOneCheck.BiomeType.Mud:
                SetMudLighting();
                break;
            case BiomeOneCheck.BiomeType.DarkZone:
                SetDarkZoneLighting();
                break;
            default:
                SetDefaultLighting();
                break;
        }
    }

    private void SetForestLighting()
    {
        mainLight.intensity = 1.0f;
        mainLight.color = Color.white;
        RenderSettings.ambientLight = new Color(0.8f, 0.8f, 0.8f);
        // Set the skybox or other environmental settings as needed
    }

    private void SetSnowLighting()
    {
        mainLight.intensity = 0.8f;
        mainLight.color = new Color(0.9f, 0.9f, 1.0f); // Slightly blue tint
        RenderSettings.ambientLight = new Color(0.6f, 0.7f, 0.8f);
        // Set the skybox or other environmental settings as needed
    }

    private void SetMudLighting()
    {
        mainLight.intensity = 0.7f;
        mainLight.color = new Color(0.8f, 0.7f, 0.5f); // Warm, earthy tone
        RenderSettings.ambientLight = new Color(0.7f, 0.65f, 0.6f);
        // Set the skybox or other environmental settings as needed
    }

    private void SetDarkZoneLighting()
    {
        mainLight.intensity = 0.25f;
        mainLight.color = Color.gray; // Dim and eerie
        RenderSettings.ambientLight = new Color(0.5f, 0.5f, 0.5f);
        // Set the skybox or other environmental settings as needed
    }

    private void SetDefaultLighting()
    {
        mainLight.intensity = 1.0f;
        mainLight.color = Color.white;
        RenderSettings.ambientLight = Color.white;
        // Reset to default skybox or other settings
    }
}
