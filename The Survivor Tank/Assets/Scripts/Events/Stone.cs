using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public int damage = 20;             // The amount of health taken away when hit.
    public int height = 6;              // The height factor of the stone above the player.
    public float timeToFollow = 1.5f;   // The time that the stone follow the player.

    GameObject player;
    float timer;

    void Start()
    {
        player = GameObject.Find("Player");

        StartCoroutine(FollowPlayerCourtine());
    }

    IEnumerator FollowPlayerCourtine()
    {
        while (timer < timeToFollow)
        {
            transform.position = player.transform.position + height * Vector3.up;
            yield return null;
        }

        gameObject.AddComponent<Rigidbody>();
    }

    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            // If the player has health to lose...
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        Destroy(gameObject, 0.5f);
    }
}
