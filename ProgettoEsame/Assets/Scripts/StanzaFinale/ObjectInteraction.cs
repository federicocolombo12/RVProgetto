using System.Collections;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public Animator animator; // Assegna l'Animator dell'oggetto animato
    public bool hasActivated = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !hasActivated)
        {
            StartCoroutine(TriggerDisapprova());
        }
        else if (Input.GetKeyDown(KeyCode.E) && hasActivated)
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
}
