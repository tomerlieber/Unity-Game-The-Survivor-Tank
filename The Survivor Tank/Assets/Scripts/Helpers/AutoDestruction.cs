using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruction : MonoBehaviour
{
    public float timeToDie = 15f; //How many seconds this object will survive

    void Start()
    {
        Destroy(gameObject, timeToDie);
    }
}
