using System.Collections;
using UnityEngine;

public class CellGuardNpc : MonoBehaviour
{
    public float idleTime = 2f; // Tempo in secondi prima che inizi a camminare
    public float walkDuration = 10f; // Durata del camminare in secondi

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator non trovato sul personaggio!");
            return;
        }

        // Inizia la routine di comportamento
        StartCoroutine(GuardRoutine());
    }

    private IEnumerator GuardRoutine()
    {
        while (true)
        {
            // Stato iniziale: Idle
            Debug.Log("Inizio Idle");
            animator.SetBool("IsWalking", false);
            yield return new WaitForSeconds(idleTime);

            // Passa allo stato di camminata
            Debug.Log("Inizio Camminata");
            animator.SetBool("IsWalking", true);
            yield return new WaitForSeconds(walkDuration);

            // Passa allo stato di idle e inizia a girare
            Debug.Log("Inizio Rotazione");
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsTurningLeft", true);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            // Ferma la rotazione
            Debug.Log("Fine Rotazione");
            animator.SetBool("IsTurningLeft", false);

            // Torna allo stato di idle
            Debug.Log("Torna a Idle");
            yield return new WaitForSeconds(idleTime);
        }
    }

    void Update()
    {
        // Puoi aggiungere eventuali aggiornamenti se necessari
    }
}
