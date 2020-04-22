using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;       // Reference to the player's health.

    Animator anim;                          // Reference to the animator component.

    void Awake()
    {
        // Set up the reference.
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
		StartCoroutine(CheckGameOver());
    }

    IEnumerator CheckGameOver()
	{
        // While the player has health...
        while (playerHealth.currentHealth > 0)
		{
			yield return new WaitForSeconds(0.3f);
		}

        // Tell the animator the game is over...
        anim.SetTrigger("GameOver");

		yield return new WaitForSeconds(3f);

		// Reload the level that is currently loaded.
		SceneManager.LoadScene(0);
	}
}
