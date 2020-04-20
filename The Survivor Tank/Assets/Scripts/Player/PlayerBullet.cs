using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    PlayerShooting playerShooting;
    ParticleSystem hitParticles;         // Reference to the particles that will play on explosion.
    AudioSource hitAudio;                // Reference to the audio that will play on hit.

    void Start()
    {
        playerShooting = GameObject.Find("Player").GetComponent<PlayerShooting>();

        hitParticles = GetComponentInChildren<ParticleSystem>();
        hitAudio = GetComponentInChildren<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the entering collider is an Enemy...
        if (collision.collider.tag == "Enemy")
        {
            // Try and find an EnemyHealth script on the gameobject hit.
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            // If the EnemyHealth component exist...
            if (enemyHealth != null)
            {
                // ... the enemy should take damage.
                enemyHealth.TakeDamage(playerShooting.damagePerShot, collision.transform.position);
            }
        }

        // Unparent the particles from the shell.
        hitParticles.transform.parent = null;

        // Play the particle system.
        hitParticles.Play();

        // Play the explosion sound effect.
        hitAudio.Play();

        // Once the particles have finished, destroy the gameobject they are on.
        ParticleSystem.MainModule mainModule = hitParticles.main;
        Destroy(hitParticles.gameObject, mainModule.duration);

        // Destroy the shell.
        Destroy(gameObject);
    }
}
