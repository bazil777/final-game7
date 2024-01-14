using UnityEngine;
using System.Collections;

public class BearBehavior : MonoBehaviour
{
    public Transform player; // Assign your player's transform in the inspector
    public PlayerHealth playerHealth; // Reference to the PlayerHealth component of the player
     public GameObject meatPrefab;
    public float detectionRange = 10f;
    public float moveSpeed = 5f;
    public float attackRange = 2f;
    public float loseInterestRange = 15f; // When the player is this far, the bear stops chasing
    public int attackDamage = 5; // Damage per attack
    public float attackCooldown = 2f; // Cooldown time for attacks in seconds
    


    private Animator animator;
    private bool isChasing = false;
    private float lastAttackTime = 0f; // Track the last time the bear attacked

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(BearRoutine());
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (player == null || playerHealth == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Player within detection range
        if (distanceToPlayer < detectionRange)
        {
            isChasing = true;
            MoveTowardsPlayer();
        }
        // Player out of lose interest range
        else if (distanceToPlayer > loseInterestRange)
        {
            isChasing = false;
            ResetMovementAnimations();
        }

        // Attack if close enough and cooldown is over
        if (isChasing && distanceToPlayer < attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            int attackNumber = Random.Range(1, 9); // Select a random attack
            animator.SetTrigger($"Attack{attackNumber}");
            DealDamageToPlayer();
            lastAttackTime = Time.time; // Update the last attack time
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime, Space.World);
        animator.SetBool("Run Forward", true);
    }

    private void ResetMovementAnimations()
    {
        animator.SetBool("Run Forward", false);
        animator.SetBool("WalkForward", false);
        animator.SetBool("Strafe Left", false);
        animator.SetBool("Strafe Right", false);
        // Reset other movement animations if necessary
    }

    IEnumerator BearRoutine()
    {
        while (true)
        {
            if (!isChasing)
            {
                PerformRandomIdleAction();
                // Increase wait time to 10-15 seconds for longer idle actions
                yield return new WaitForSeconds(Random.Range(10f, 15f));
            }
            else
            {
                yield return new WaitForSeconds(3f);
            }
        }
    }

    private void PerformRandomIdleAction()
    {
        float action = Random.Range(0f, 1f);

        if (action < 0.5f)
        {
            animator.SetBool("Sit", true);
            animator.SetBool("Sleep", false);
        }
        else
        {
            animator.SetBool("Sleep", true);
            animator.SetBool("Sit", false);
        }
    }

    private void DealDamageToPlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
        public void HandleDeath()
    {
        if (meatPrefab != null)
        {
            Instantiate(meatPrefab, transform.position, Quaternion.identity); // Instantiate meat at bear's position
        }
        Destroy(gameObject); // Destroy the bear
    }
}
