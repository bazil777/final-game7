using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public int shieldAmount = 100; // Initial shield value
    public Material brokenShieldMaterial; // Shield material change
    public int maxShield = 100; // The maximum shield value

    private Renderer shieldRenderer;
    private Material originalShieldMaterial;

    private void Start(){
        shieldRenderer = GetComponent<Renderer>(); // Attach this script to your shield GameObject with a Renderer component
        originalShieldMaterial = shieldRenderer.material;
    }

    public void TakeShieldDamage(int damageAmount){
        shieldAmount -= damageAmount;
        if (shieldAmount <= 0){
            shieldAmount = 0; // Similar logic to health - make sure doesnt fall below 0
            OnShieldsBroken();
        }
        UpdateShieldMaterial();
    }

    public void RechargeShields(int rechargeAmount){
        shieldAmount += rechargeAmount;
        if (shieldAmount > maxShield){
            shieldAmount = maxShield; // similar logic to health - make sure doesnt exceed 100 (maxShield material)
        }
        UpdateShieldMaterial(); //else call this method
    }

    // When no shield then change colour. Not Neccassary for my game yet. I might leave empty for now
    private void OnShieldsBroken(){
        shieldRenderer.material = brokenShieldMaterial;
    }

    // updates shield to original colour
    private void UpdateShieldMaterial(){
        shieldRenderer.material = originalShieldMaterial;
    }

    // I created a boolean to turn shields on or off
    public void ToggleShield(bool isActive){
        gameObject.SetActive(isActive);
    }

    // Method to increase shields however I have not implemented this in the code yet
    public void IncreaseShields(int increaseAmount){
        RechargeShields(increaseAmount);
    }
}
