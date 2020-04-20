using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int life = 10;               // The amount of health is given to the player.
    public float rotateSpeed = 50f;     // The speed that the heart will rotate at.

    private void Start()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
    }

    private void Update()
    {
        // Spin the object around the world origin at 20 degrees/second.
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

            // If the player has health to lose...
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.GetLife(life);
            }

            Destroy(gameObject);
        }
    }
}
