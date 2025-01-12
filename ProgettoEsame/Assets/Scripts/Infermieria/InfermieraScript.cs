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
    public void TriggerNurseTalk()
    {
        // Attiva l'animazione dell'infermiera
        animator.SetTrigger("NurseTalk");
    }
}
