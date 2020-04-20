using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStartingPoint : MonoBehaviour
{
    public string SpawnPointsName;

    Transform spawnPoints;

    void Start()
    {
        spawnPoints = GameObject.Find(SpawnPointsName).transform;
        int spawnPointIndex = Random.Range(0, spawnPoints.childCount);
        transform.position = spawnPoints.GetChild(spawnPointIndex).position;
    }
}
