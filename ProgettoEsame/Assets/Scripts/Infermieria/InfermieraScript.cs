using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfermieraScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> characters; // Lista dei personaggi in fila
    private Animator animator;

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
        // Attiva l'animazione dell'infermiera
        StartCoroutine(TriggerNurseTalkCoroutine(interactor));
        
    }

    private IEnumerator TriggerNurseTalkCoroutine(GameObject interactor)
    {
        // Attiva l'animazione dell'infermiera
        animator.SetTrigger("NurseTalk");
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("NurseIdle");
        PlayerLock playerLock = interactor.GetComponent<PlayerLock>();
        playerLock.reachedPoint = false;
    }
}
