using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public Animator animator; // Assegna l'Animator dell'oggetto animato
    public bool hasActivated = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !hasActivated) // Puoi cambiare la condizione di attivazione
        {
            animator.SetTrigger("ActivateAction");
            hasActivated = true; // Evita che venga attivato più volte
        }
    }
}
