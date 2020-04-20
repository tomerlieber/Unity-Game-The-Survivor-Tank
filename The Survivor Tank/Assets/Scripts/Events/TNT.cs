using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    public int damageOfExplosion = 50;
    public float timeOfReductionSpeed = 5f;

    // public array of Texture2D called Textures
    public Texture2D[] Textures;

    // public prefab Explosion
    public GameObject ExplosionParticle;

    public float minWaitTime = 5f;
    public float maxWaitTime = 10f;

    // A variable of type Muffin
    GameObject player;

    // A variable of type bool called isPlayerInsideExplosionArea, with a default value equal to false
    bool isPlayerInsideExplosionArea;

    void Start()
    {
        player = GameObject.Find("Player");
        
        // call the coroutine Explosion
        StartCoroutine(Explosion());
    }

    // Coroutine Explosion
    IEnumerator Explosion()
    {
        // randomly wait.
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);

        // Each 1 seconds, set the "TNT_3", then "TNT_2" then "TNT_1" textures to the cube. Check the shader to know which property to modify.
        for (int i = 0; i < Textures.Length; i++)
        {
            yield return new WaitForSeconds(1f);
            GetComponent<MeshRenderer>().material.SetTexture("_MainTexture", Textures[i]);
        }
        // Spawn 5 times the Explosion prefab around the cube position each 0.1 second.
        // Use a random radius of 3 => Random.insideUnitSphere
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            Destroy(Instantiate(ExplosionParticle, transform.position + (Random.insideUnitSphere + Vector3.up) * 4, Quaternion.identity), 2f);
        }

        Destroy(gameObject);

        // If the muffin is inside the trigger, call the public method Death of the Muffin class.
        if (isPlayerInsideExplosionArea)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            // If the EnemyHealth component exist...
            if (playerHealth != null)
            {
                // ... the enemy should take damage.
                playerHealth.TakeDamage(damageOfExplosion);
                player.GetComponent<PlayerMovement>().ReduceSpeed(timeOfReductionSpeed);
            }
        }
    }

    // Set the correct value for the variable isMuffinInsideExplosionArea
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerInsideExplosionArea = true;
        }
    }

    // Set the correct value for the variable isMuffinInsideExplosionArea
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerInsideExplosionArea = false;
        }
    }
}