using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfermieraScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> characters; // Lista dei personaggi in fila
    private Animator animator;
    [SerializeField] private InfermieraSound infermieraSound; // Aggiungi il riferimento allo script audio separato
    [SerializeField] private int interactionCount = 0; // Contatore delle interazioni
    private bool isTalking = false; // Variabile di stato per tracciare se l'infermiera sta parlando
    [SerializeField] private AttivaPorta attivaPorta; // Riferimento allo script AttivaPorta
    void Start()
    {
        animator = GetComponent<Animator>();
        characters = QueueManager.instance.characters;
        infermieraSound = GetComponent<InfermieraSound>();
    }

    public void TriggerNurseAnimation()
    {
        // Attiva l'animazione dell'infermiera
        animator.SetTrigger("NurseReact");
    }

    public void TriggerNurseTalk(GameObject interactor)
    {
        if (!isTalking)
        {
            // Attiva l'animazione dell'infermiera e riproduce il suono
            StartCoroutine(TriggerNurseTalkCoroutine(interactor));
        }
    }

    private IEnumerator TriggerNurseTalkCoroutine(GameObject interactor)
    {
        isTalking = true; // Imposta lo stato a "parlando"
        animator.SetTrigger("NurseTalk");

        // Riproduce il suono dell'infermiera che parla solo se non è già in riproduzione
        if (infermieraSound != null && !infermieraSound.IsPlaying())
        {
            infermieraSound.PlayNurseTalkSound();  // Riproduce il suono
        }

        if (interactionCount == 0)
        {
            // Prima interazione: permetti di esplorare la stanza
            Debug.Log("Puoi esplorare la stanza.");
        }
        else if (interactionCount == 1)
        {
            // Seconda interazione: permetti di far cadere l'oggetto sul tavolo e triggerare il corridoio
            Debug.Log("Puoi posare l'oggetto sul tavolo.");
            
            if (attivaPorta != null)
            {
                attivaPorta.enabled = true;
            }
        }

        interactionCount++;

        yield return new WaitForSeconds(5f);  // Attendi che l'animazione finisca

        animator.SetTrigger("NurseIdle");
        PlayerLock playerLock = interactor.GetComponent<PlayerLock>();
        playerLock.reachedPoint = false;
        isTalking = false; // Reimposta lo stato a "non parlando"
    }
}
