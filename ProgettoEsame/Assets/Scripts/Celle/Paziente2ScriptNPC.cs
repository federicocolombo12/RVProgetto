using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paziente2ScriptNPC : MonoBehaviour
{
    private Animator animator;
    private NpcHeadLookAtCelle npcHeadLookAtCelle;
    private Paziente2AudioManager audioManager; // Riferimento al gestore audio

    void Awake()
    {
        npcHeadLookAtCelle = GetComponent<NpcHeadLookAtCelle>();
        audioManager = GetComponent<Paziente2AudioManager>(); // Recupera il componente audio
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator non trovato sul Paziente 2!");
        }
        npcHeadLookAtCelle = GetComponent<NpcHeadLookAtCelle>();
    }

    void Update()
    {
        if (NpcCelleInteractionManager.instance.paziente2Interaction)
        {
            Interagisci();
            NpcCelleInteractionManager.instance.paziente2Interaction = false;
            CellaManager.instance.attivaPazienteRoutine = true;
        }
        else
        {
            PazienteLook();
        }
    }

    private void Interagisci()
    {
        if (animator != null)
        {
            StartCoroutine(InteragisciRoutine());
        }

        if (audioManager != null)
        {
            audioManager.AvviaDialogo(); // Avvia il dialogo audio
        }
    }

    private IEnumerator InteragisciRoutine()
    {
        animator.SetBool("Interagisci", true);
        yield return new WaitForSeconds(7f);
        animator.SetBool("Interagisci", false);
        NpcCelleInteractionManager.instance.ReturnToInitialPosition(transform);
    }

    private void PazienteLook()
    {
        npcHeadLookAtCelle.LookAtPosition(Camera.main.transform.position);
    }
}
