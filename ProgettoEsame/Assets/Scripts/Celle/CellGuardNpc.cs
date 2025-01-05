using System.Collections;
using UnityEngine;

public class CellGuardNpc : MonoBehaviour
{
    public float idleTime = 2f; // Tempo in secondi prima che inizi a camminare
    public float firstWalkDuration = 1.5f; // Durata del primo camminare in secondi
    public float secondWalkDuration = 2f; // Durata del secondo camminare in secondi
    public float secondIdleTime = 10f; // Tempo in secondi per il secondo idle
    public float rightTurnDuration = 1f; // Durata della rotazione a destra in secondi
    public float leftTurnWaitTime = 0.5f; // Tempo di attesa dopo la rotazione a sinistra

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
            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(idleTime);

            // Passa allo stato di camminata
            Debug.Log("Inizio Camminata");
            animator.SetBool("SetIdle", false);
            animator.SetBool("IsWalking", true);
            yield return new WaitForSeconds(firstWalkDuration);

            // Passa allo stato di idle e inizia a girare a sinistra
            Debug.Log("Inizio Rotazione a Sinistra");
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsTurningLeft", true);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            // Ferma la rotazione a sinistra e aspetta
            Debug.Log("Fine Rotazione a Sinistra");
            animator.SetBool("IsTurningLeft", false);
            yield return new WaitForSeconds(leftTurnWaitTime);

            // Stato di idle
            Debug.Log("Inizio Idle");
            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(secondIdleTime);

            // Inizia a girare a destra
            Debug.Log("Inizio Rotazione a Destra");
            animator.SetBool("SetIdle", false);
            animator.SetBool("IsTurningRight", true);
            yield return new WaitForSeconds(rightTurnDuration);

            // Ferma la rotazione a destra
            Debug.Log("Fine Rotazione a Destra");
            animator.SetBool("IsTurningRight", false);

            // Passa allo stato di camminata
            Debug.Log("Inizio Seconda Camminata");
            animator.SetBool("IsWalking", true);
            yield return new WaitForSeconds(secondWalkDuration);

            // Inizia a girare a sinistra
            Debug.Log("Inizio Seconda Rotazione a Sinistra");
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsTurningLeft", true);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            // Ferma la rotazione a sinistra
            Debug.Log("Fine Seconda Rotazione a Sinistra");
            animator.SetBool("IsTurningLeft", false);

            // Torna allo stato di idle
            Debug.Log("Torna a Idle");
            animator.SetBool("SetIdle", true);
            yield return new WaitForSeconds(idleTime);
        }
    }

    void Update()
    {
        // Puoi aggiungere eventuali aggiornamenti se necessari
    }
}

