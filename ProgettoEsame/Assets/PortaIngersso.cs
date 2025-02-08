using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaIngersso : MonoBehaviour
{
    public float rotationSpeed = 1f; // Velocità di rotazione
    private bool isRotating = false;
    private Quaternion initialRotation;
    private Quaternion openRotation;

    void Start()
    {
        initialRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 91, transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Rimuovi il controllo della rotazione manuale
    }

    public void OpenDoor()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateDoor(openRotation));
        }
    }

    public void CloseDoor()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateDoor(initialRotation));
        }
    }

    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        float elapsedTime = 0f;
        float duration = 1f / rotationSpeed; // Durata della rotazione in base alla velocità

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;
    }
}
