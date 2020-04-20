using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour 
{
    
    public Event[] gameEvents;
    public Event[] bonusEvents;
    public float timeForNextEvent = 5f;
    public int nextRound = 6;
    public float percentageForBonus = 50f;

    PlayerHealth playerHealth;

    void Start () 
	{
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        StartCoroutine(GameLoopCoroutine());
	}
		
	IEnumerator GameLoopCoroutine()
    {
        int eventsCounter = 0;

        while(playerHealth.currentHealth > 0)
        {
            eventsCounter++;

            yield return new WaitForSeconds(timeForNextEvent);

            bool condition = Random.Range(0, 100) < percentageForBonus;
            Event selectedEvent = RandomEventSelection(condition ? bonusEvents : gameEvents);

            if (selectedEvent != null && selectedEvent.EventObject != null)
            {
                // Spawn random event GO
                Instantiate(selectedEvent.EventObject, Vector3.zero, Quaternion.identity);
            }

            if (eventsCounter % nextRound == 0)
            {
                percentageForBonus -= 5;
                percentageForBonus = Mathf.Max(percentageForBonus, 0);
            }
        }
	}

    Event RandomEventSelection(Event[] events)
    {
        int total = 0;
        foreach (Event e in events)
        {
            total += e.SpawnPercentage;
        }

        int rand = Random.Range(0, total);
        int downLimit = 0;
        Event selectedEvent = new Event();

        foreach (Event e in events)
        {
            if (rand >= downLimit && rand < downLimit + e.SpawnPercentage)
            {
                selectedEvent = e;
                break;
            }

            downLimit += e.SpawnPercentage;
        }

        return selectedEvent;
    }
}