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
    [SerializeField] private AttivaPorta attivaPorta;
    [SerializeField] private GameObject childUi;  // Riferimento allo script AttivaPorta

    void Start()
    {
        animator = GetComponent<Animator>();
        characters = QueueManager.instance.characters;
        infermieraSound = GetComponent<InfermieraSound>();
        childUi = transform.GetChild(2).gameObject;
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
        if (interactionCount == 0)
        {
            // Prima interazione: permetti di esplorare la stanza
            Debug.Log("Puoi esplorare la stanza.");
            isTalking = true; // Imposta lo stato a "parlando"
            animator.SetTrigger("NurseTalk");
            childUi.tag = "Untagged"; // Rimuovi il tag "Interactable" dall'infermiera
            if (!infermieraSound.IsPlaying())
            {
                infermieraSound.PlayNurseTalkSound(1);  // Riproduce il primo suono
            }
        }
        else
        {
            // Interazioni successive: riproduce sempre il secondo dialogo
            childUi.tag = "Untagged";
            Debug.Log("Puoi posare l'oggetto sul tavolo.");
            isTalking = true; // Imposta lo stato a "parlando"
            animator.SetTrigger("NurseTalk");
            if (infermieraSound != null && !infermieraSound.IsPlaying())
            {
                infermieraSound.PlayNurseTalkSound(2);  // Riproduce il secondo suono
            }

            if (attivaPorta != null)
            {
                attivaPorta.enabled = true;
            }
        }

        interactionCount++;

        yield return new WaitForSeconds(10f);  // Attendi che l'animazione finisca

        animator.SetTrigger("NurseIdle");
        childUi.tag = "OggettoInteragibile1";
        PlayerLock playerLock = interactor.GetComponent<PlayerLock>();
        playerLock.reachedPoint = false;
        isTalking = false; // Reimposta lo stato a "non parlando"
    }
}