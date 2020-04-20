using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shield : MonoBehaviour
{
    public float timeToShield = 10f;
    public float timeToNextDest = 3.5f;
    NavMeshAgent nav;
    Transform spawnPoints;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        spawnPoints = GameObject.Find("ShieldSpawnPoints").transform;

        StartCoroutine(MoveToNextDest());
    }

    IEnumerator MoveToNextDest()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.childCount);
        nav.SetDestination(spawnPoints.GetChild(spawnPointIndex).position);

        yield return new WaitForSeconds(timeToNextDest);

        StartCoroutine(MoveToNextDest());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

            // If the player has health to lose...
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.AddShield(timeToShield);
            }

            Destroy(gameObject);
        }
    }
}