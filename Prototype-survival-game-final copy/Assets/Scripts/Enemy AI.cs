using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour{
    public GameObject target; // Change to GameObject

    public float chaseRange = 15f; //will chase player when in range
    public float moveSpeed = 3.0f; // movement speed of enemy
    public float contactRange = 1.0f; // attack range
    public float attackCooldown = 2.0f; // attack cooldown

    public Material chasingMaterial; // Assign chasing material in the Inspector. Optional just a visual que for my prototype

    public int damageAmount = 5; // Each enemy cone will deal this damage per hit
    public int maxHealth = 50; // NPC enemy will have this health

    private Material originalMaterial;
    private Renderer coneRenderer;
    private bool canAttack = true; 
    private int currentHealth; // Current health of the enemy

    public PlayerGold playerGold; // Reference to the PlayerGold script, I might introduce an attack that steals gold and deals damage

    void Start() {
        coneRenderer = GetComponent<Renderer>();
        originalMaterial = coneRenderer.material;
        currentHealth = maxHealth; // health of enemy set
    }

    void Update(){
        if (target == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToPlayer <= chaseRange){
            coneRenderer.material = chasingMaterial;
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // if in contact and in range then target player health or shields if the player still has
            if (distanceToPlayer < contactRange && canAttack){
                PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
                ShieldController shieldController = target.GetComponent<ShieldController>();

                //check if player has shields before attacking health
                if (shieldController != null && shieldController.shieldAmount > 0){
                    // Deal damage to the player's shields 
                    shieldController.TakeShieldDamage(damageAmount);
                }
                //otherwise they must not have nany shield so attack health
                else{
                    // Deal damage to the player's health
                    playerHealth.TakeDamage(damageAmount);
                }

                // call cooldown method
                StartCoroutine(AttackCooldown());
            }
        }
        else
        {
            coneRenderer.material = originalMaterial;
        }
    }

    //cooldown method
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    // if current health reaches less than or equal to zero  (from player attcaks) then call die function on enemy
    public void TakeDamage(int damage){
        currentHealth -= damage;

        if (currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        // Destroy the enemy cone object
        Destroy(gameObject);
    }
}
