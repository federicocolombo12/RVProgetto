using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfermieraScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> characters; // Lista dei personaggi in fila
    private Animator animator;
    [SerializeField] private InfermieraSound infermieraSound; // Aggiungi il riferimento allo script audio separato

    void Start()
    {
        animator = GetComponent<Animator>();
        characters = QueueManager.instance.characters;
    }

    public void TriggerNurseAnimation()
    {
        // Attiva l'animazione dell'infermiera
        animator.SetTrigger("NurseReact");
    }

    public void TriggerNurseTalk(GameObject interactor)
    {
        // Attiva l'animazione dell'infermiera e riproduce il suono
        StartCoroutine(TriggerNurseTalkCoroutine(interactor));
    }

    private IEnumerator TriggerNurseTalkCoroutine(GameObject interactor)
    {
        // Attiva l'animazione dell'infermiera
        animator.SetTrigger("NurseTalk");

        // Riproduce il suono dell'infermiera che parla solo se non è già in riproduzione
        if (infermieraSound != null && !infermieraSound.IsPlaying())
        {
            infermieraSound.PlayNurseTalkSound();  // Riproduce il suono
        }

        yield return new WaitForSeconds(2f);  // Attendi che l'animazione finisca

        animator.SetTrigger("NurseIdle");
        PlayerLock playerLock = interactor.GetComponent<PlayerLock>();
        playerLock.reachedPoint = false;
    }
}
