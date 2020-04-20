using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;               // The speed that the player will move at.
    public float rotateSpeed = 180f;        // The speed that the player will rotate at.

    private float invertedControlFactor = 1;

    void Update()
    {
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime * invertedControlFactor);
        transform.position += transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime * invertedControlFactor;
    }

    public void InvertControls(float time)
    {
        StartCoroutine(InvertedControlsCourtine(time));
    }

    public IEnumerator InvertedControlsCourtine(float time)
    {
        invertedControlFactor = -1f;

        yield return new WaitForSeconds(time);

        invertedControlFactor = 1f;
    }

    public void ReduceSpeed(float time)
    {
        StartCoroutine(ReduceSpeedCourtine(time));
    }

    public IEnumerator ReduceSpeedCourtine(float time)
    {
        speed /= 3f;
        rotateSpeed /= 3f;

        yield return new WaitForSeconds(time);

        speed *= 3f;
        rotateSpeed *= 3f;
    }
}
