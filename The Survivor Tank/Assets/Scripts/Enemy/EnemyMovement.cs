using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public bool isPlayerInRange;    // Whether player is within the range and can be attacked.
    public float speed = 3.5f;
    public float rotateSpeed = 30f; // Angular speed in degrees per sec.

    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    EnemyShooting enemyShooting;    // Reference to this enemy's shooting.
    NavMeshAgent nav;               // Reference to the nav mesh agent.

    void Start()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyShooting = GetComponent<EnemyShooting>();
        nav = GetComponent<NavMeshAgent>();
        nav.stoppingDistance = enemyShooting.distanceToAttack;
        nav.speed = speed;
    }

    void Update()
    {
        // If the enemy and the player have health left...
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if (Vector3.Distance(transform.position, player.position) < nav.stoppingDistance)
            {
                Vector3 direction = player.position - transform.position;
                direction.y = 0; // Ignore Y
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // The step size is equal to speed times frame time.
                var step = rotateSpeed * Time.deltaTime;

                // Rotate our transform a step closer to the target's
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);

                //transform.LookAt(player);
                isPlayerInRange = true;
            }
            else
            {
                // ... set the destination of the nav mesh agent to the player.
                nav.SetDestination(player.position);
                isPlayerInRange = false;
            }
        }
        // Otherwise...
        else
        {
            // ... disable the nav mesh agent.
            nav.enabled = false;
        }
    }
}
