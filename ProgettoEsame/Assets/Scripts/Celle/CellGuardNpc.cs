using System.Collections;
using UnityEngine;

public class CellGuardNpc : MonoBehaviour
{
    public float idleTime = 3f; // Tempo in secondi prima che inizi a camminare
    public float walkDuration = 5f; // Durata del camminare in secondi
    public float turnDuration = 1.5f; // Durata della rotazione a sinistra
    public float turnSpeed = 90f; // Velocità di rotazione in gradi al secondo

    private Animator animator;
    private bool isTurning = false;

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
        // Stato iniziale: Idle
        yield return new WaitForSeconds(idleTime);

        // Passa allo stato di camminata
        animator.SetBool("IsWalking", true);
        yield return new WaitForSeconds(walkDuration);

        // Passa allo stato di idle e inizia a girare
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsTurningLeft", true);
        isTurning = true;

        // Effettua la rotazione a sinistra
        float elapsedTime = 0f;
        while (elapsedTime < turnDuration)
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ferma la rotazione
        animator.SetBool("IsTurningLeft", false);
        isTurning = false;

        // Torna allo stato di idle
        yield return null;
    }

    void Update()
    {
        // Puoi aggiungere eventuali aggiornamenti se necessari
    }
}
