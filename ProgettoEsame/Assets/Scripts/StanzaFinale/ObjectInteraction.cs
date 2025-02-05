using System.Collections;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public Animator animator; // Assegna l'Animator dell'oggetto animato
    public bool hasActivated = false;
    private bool isColliding = false; // Variabile per tenere traccia della collisione

    void Update()
    {
        if (isColliding && Input.GetKeyDown(KeyCode.E) && !hasActivated)
        {
            StartCoroutine(TriggerDisapprova());
        }
        else if (isColliding && Input.GetKeyDown(KeyCode.E) && hasActivated)
        {
            StartCoroutine(TriggerActivateAction());
        }
    }

    private IEnumerator TriggerDisapprova()
    {
        animator.SetTrigger("Disapprova");
        yield return new WaitForSeconds(1f); // Aspetta prima di tornare indietro
        animator.SetTrigger("TornaIndietro");
        animator.ResetTrigger("Disapprova");
        hasActivated = true;
    }

    private IEnumerator TriggerActivateAction()
    {
        animator.SetTrigger("ActivateAction");
        hasActivated = false;
        yield return null; // Puoi cambiare se serve una pausa
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assicurati che l'altro oggetto abbia il tag "Player"
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Assicurati che l'altro oggetto abbia il tag "Player"
        {
            isColliding = false;
        }
    }
}

