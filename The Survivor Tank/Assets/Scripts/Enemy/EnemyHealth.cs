using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
    public AudioClip deathClip;                 // The sound to play when the enemy dies.

    AudioSource deathAudio;                     // Reference to the audio source.
    ParticleSystem deathParticles;              // Reference to the particle system that plays when the enemy is damaged.
    BoxCollider boxCollider;                    // Reference to the box collider.
    bool isDead;

    void Start()
    {
        // Setting up the references.
        deathAudio = GetComponentInChildren<AudioSource>();
        deathParticles = GetComponentInChildren<ParticleSystem>();
        boxCollider = GetComponent<BoxCollider>();

        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }

        void Death()
        {
            // The enemy is dead.
            isDead = true;

            // Turn the collider into a trigger so shots can pass through it.
            boxCollider.isTrigger = true;

            deathParticles.Play();

            // Play the tank explosion sound effect.
            deathAudio.Play();

            // Increase the score by the enemy's score value.
            ScoreManager.score += scoreValue;

            // After 2 seconds destory the enemy.
            Destroy(gameObject, 3f);
        }
    }
}