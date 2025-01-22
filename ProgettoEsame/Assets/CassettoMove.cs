using System.Collections;
using UnityEngine;

public class CassettoMove : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isOpen = false; // Stato del cassetto
    [SerializeField] private bool isMoving = false; // Controlla se il cassetto è in movimento

    public float moveDistance = 0.5f; // Distanza che il cassetto deve muoversi
    public float moveSpeed = 2f; // Velocità del movimento

    public void Interact(GameObject interactor)
    {
        if (!isMoving)
        {
            if (!isOpen)
            {
                Debug.Log("Interazione con il cassetto");
                StartCoroutine(Open());
            }
            else
            {
                StartCoroutine(Close());
            }
        }
    }

    public void StopInteract(GameObject interactor)
    {
        // Questo metodo può essere usato per gestire eventuali interazioni persistenti
        Debug.Log("Interazione terminata.");
    }

    private IEnumerator Open()
    {
        Debug.Log("Cassetto aperto");
        isMoving = true;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, 0, -moveDistance);

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition; // Per precisione
        isOpen = true;
        isMoving = false;
    }

    private IEnumerator Close()
    {
        Debug.Log("Cassetto chiuso");
        isMoving = true;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, 0, moveDistance);

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition; // Per precisione
        isOpen = false;
        isMoving = false;
    }
}
