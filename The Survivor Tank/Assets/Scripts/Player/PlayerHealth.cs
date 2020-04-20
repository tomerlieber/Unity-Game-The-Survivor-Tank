using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour                       
{
    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

    PlayerMovement playerMovement;                              // Reference to the player's movement.
    PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
    //AudioSource playerAudio;                                    // Reference to the AudioSource component.

    bool isDamaged;                                             // Indicate Wheather the player gets damaged.
    bool isDead;                                                // Indicate whether the player is dead.
    bool isInvulnerable;                                        // Indicates whether the player cannot be damaged.

    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponent<PlayerShooting>();
        //playerAudio = GetComponent<AudioSource>();

        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDamaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        isDamaged = false;
    }

    public void TakeDamage(int amount)
    {
        if (isInvulnerable)
        {
            return;
        }

        // Set the damaged flag so the screen will flash.
        isDamaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;

        // Play the hurt sound effect.
        //playerAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void AddShield(float time)
    {
        StartCoroutine(AddShieldCourtine(time));
    }

    public IEnumerator AddShieldCourtine(float time)
    {
        isInvulnerable = true;

        StartCoroutine(Blink(time));

        yield return new WaitForSeconds(time);

        isInvulnerable = false;
    }

    IEnumerator Blink(float waitTime)
    {
        float endTime = Time.time + waitTime;
        while (Time.time < endTime)
        {
            Renderer[] renders = gameObject.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renders.Length; i++)
            {
                renders[i].enabled = false;
            }

            yield return new WaitForSeconds(0.2f);

            for (int i = 0; i < renders.Length; i++)
            {
                renders[i].enabled = true;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void GetLife(int amount)
    {
		// Increase the current health by the life amount.
		currentHealth += amount;

        // Make sure the amount of health is not higher than a hundred
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;
    }

    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;


        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }
}