using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLibrary : MonoBehaviour
{
    [SerializeField] float rotateDuration = 5f; // Durata in secondi della rotazione
    [SerializeField] float rotationAngle = -45f; // Angolo di rotazione sull'asse X

    // Metodo per avviare la rotazione
    public void Rotate()
    {
        StartCoroutine(RotateCoroutine());
    }

    // Coroutine per gestire la rotazione temporizzata
    IEnumerator RotateCoroutine()
    {
        float elapsedTime = 0f;
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(rotationAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        while (elapsedTime < rotateDuration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotateDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assicurati che la rotazione finale sia esattamente quella desiderata
        transform.rotation = targetRotation;

        // Rotazione terminata
        Debug.Log("Rotazione terminata!");
    }
}
