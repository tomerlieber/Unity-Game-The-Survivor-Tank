using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;              // The damage inflicted by each bullet.
    public float timeBetweenBullets = 1f;       // The time between each shot.
    public GameObject bullet;                   // Reference to the bullet.
    public float bulletForce = 10f;             // The starting force that it's given to the bullet.

    AudioSource shotAudio;                      // Reference to the audio source.
    Transform bulletSpawnPoint;                 // Reference to the bullet spawn point.
    float timer;                                // A timer to determine when to fire.

    void Start()
    {
        bulletSpawnPoint = GameObject.Find("PlayerBulletSpawnPoint").transform;
        shotAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the Space button is being press and it's time to shoot...
        if (Input.GetKey(KeyCode.Space) && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Reset the timer.
        timer = 0f;

        // Play the shoot bullet audioclip.
        shotAudio.Play();

        GameObject bulletClone = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
        bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce, ForceMode.Impulse);
    }

    public void GetStrongerWeapon(float time)
    {
        StartCoroutine(GetStrongerWeaponCourtine(time));
    }

    IEnumerator GetStrongerWeaponCourtine(float time)
    {
        damagePerShot += 20;
        timeBetweenBullets /= 4f;

        yield return new WaitForSeconds(time);

        damagePerShot -= 20;
        timeBetweenBullets *= 4f;
    }
}