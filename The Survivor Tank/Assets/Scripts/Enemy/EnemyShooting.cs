using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float timeBetweenShootings = 2f;     // The time in seconds between each attack.
    public int damagePerShot = 10;               // The amount of health taken away per attack.
    public float distanceToAttack = 15f;
    public GameObject bullet;
    public float BulletForce = 20f;
    public Transform bulletSpawnPoint;

    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    EnemyMovement enemyMovement;                // Reference to this enemy's movement
    float timer;                                // Timer for counting up to the next attack.

    // Start is called before the first frame update
    void Start()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenShootings && enemyMovement.isPlayerInRange && enemyHealth.currentHealth > 0)
        {
            // ... attack.
            Attack();
        }
    }

    void Attack()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if (playerHealth.currentHealth > 0)
        {
            GameObject bulletClone = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
            bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward * BulletForce, ForceMode.Impulse);
        }
    }
}
