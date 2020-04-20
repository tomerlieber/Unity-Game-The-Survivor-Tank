using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongerWeapon : MonoBehaviour
{
    public float timeToStrongerWeapon = 8f;
    public float speed = 2.5f;
    public float height = 1f;

    // Update is called once per frame
    void Update()
    {
        // Constantly moves the GameObject up and down
        float newY = Mathf.Sin(Time.time * speed) * height + 1f;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerHealth>().currentHealth > 0)
            {
                other.gameObject.GetComponent<PlayerShooting>().GetStrongerWeapon(timeToStrongerWeapon);
            }

            Destroy(gameObject);
        }
    }
}
