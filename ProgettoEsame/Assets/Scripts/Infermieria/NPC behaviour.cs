using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CharacterBehavior : MonoBehaviour
{
    public Transform medicinePoint;
    public Transform exitPoint;
    public bool HasFinished { get; private set; } = false;

    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private InfermieraScript nurse;
    private NpcFootstepAudio footstepAudio; // Aggiunto per il suono dei passi
    private DialogueManager dialogueManager; // Riferimento al DialogueManager
    private bool hasStartedDialogue = false; // Per evitare di chiamare il dialogo più volte

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        footstepAudio = GetComponent<NpcFootstepAudio>(); // Trova il componente audio
        dialogueManager = FindObjectOfType<DialogueManager>(); // Trova il DialogueManager nella scena

        // Assicurati che il personaggio sia in idle inizialmente
        animator.SetBool("isWalking", false);
    }

    public void StartActions()
    {
        StartCoroutine(PerformActions());
    }

    private IEnumerator PerformActions()
    {
        // Cammina verso il punto della medicina
        StartWalking();
        agent.SetDestination(medicinePoint.position);

        // Avvia il dialogo se non è già iniziato
        if (!hasStartedDialogue && dialogueManager != null)
        {
            dialogueManager.StartDialogue("L'NPC sta andando a prendere la medicina.");
            hasStartedDialogue = true;
        }

        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.1f);

        // Prendi la medicina
        StopWalking();
        if (nurse != null)
        {
            Debug.Log("Notifying nurse");
            nurse.TriggerNurseAnimation();
        }
        else
        {
            Debug.LogWarning("Nurse reference is missing");
        }
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("takeMedicine");

        yield return new WaitForSeconds(4f);

        // Cammina verso il punto di uscita
        StartWalking();
        HasFinished = true;
        agent.SetDestination(exitPoint.position);

        // Aggiorna il dialogo mentre l'NPC si muove verso l'uscita
        if (dialogueManager != null)
        {
            dialogueManager.PlayMovementDialogue();
        }

        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.5f);

        // Termina il dialogo quando l'NPC arriva a destinazione
        if (dialogueManager != null)
        {
            dialogueManager.EndDialogue();
        }

        // Termina le azioni
        StopWalking();
        Destroy(gameObject);
    }

    private void StartWalking()
    {
        animator.SetBool("isWalking", true);
        footstepAudio?.StartFootsteps(); // Avvia i passi se il componente esiste
    }

    private void StopWalking()
    {
        animator.SetBool("isWalking", false);
        footstepAudio?.StopFootsteps(); // Ferma i passi
    }
}
