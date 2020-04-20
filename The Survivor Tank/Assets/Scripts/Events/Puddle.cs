using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    public float timeToInvertControls = 8f;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        transform.position = FindTheClosestSpawnPoint();
    }

    Vector3 FindTheClosestSpawnPoint()
    {
        Transform spawnPoints = GameObject.Find("PuddleSpawnPoints").transform;

        Vector3 closestSpawnPoint = Vector3.positiveInfinity;
        float minDistance = float.PositiveInfinity;

        foreach (Transform spawnPoint in spawnPoints)
        {
            float newDistance = Vector3.Distance(spawnPoint.position, player.transform.position);
            if (newDistance < minDistance || closestSpawnPoint == Vector3.positiveInfinity)
            {
                minDistance = newDistance;
                closestSpawnPoint = spawnPoint.position;
            }
        }

        return closestSpawnPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            // If the player has health to lose...
            if (playerHealth.currentHealth > 0)
            {
                other.GetComponent<PlayerMovement>().InvertControls(timeToInvertControls);
            }

            Destroy(gameObject, 1.5f);
        }
    }
}