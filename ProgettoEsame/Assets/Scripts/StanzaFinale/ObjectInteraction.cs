using System.Collections;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public Animator animator;
    public bool hasActivated = false;
    private bool isColliding = false;
    public float delayTime = 2f;
    public bool startVideo = false;

    private GiorgioCodaAudio audioScript; // Riferimento allo script audio

    void Start()
    {
        // Trova lo script GiorgioCodaAudio sullo stesso oggetto
        audioScript = GetComponent<GiorgioCodaAudio>();

        // Verifica che l'animator sia assegnato
        if (animator == null)
        {
            Debug.LogError("Animator non assegnato su " + gameObject.name);
        }
    }

    void Update()
    {
        if (isColliding && Input.GetKeyDown(KeyCode.E))
        {
            if (!hasActivated)
            {
                StartCoroutine(TriggerDisapprova());
            }
            else
            {
                StartCoroutine(TriggerActivateAction());
            }

            // Avvia il suono quando si interagisce
            if (audioScript != null)
            {
                audioScript.PlayInteractionSound();
            }
            else
            {
                Debug.LogWarning("GiorgioCodaAudio non trovato su " + gameObject.name);
            }
        }
    }

    private IEnumerator TriggerDisapprova()
    {
        Debug.Log("Attivazione trigger Disapprova");
        animator.SetTrigger("Disapprova");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("TornaIndietro");
        hasActivated = true;
    }

    private IEnumerator TriggerActivateAction()
    {
        Debug.Log("Attivazione trigger ActivateAction");
        animator.SetTrigger("ActivateAction");
        hasActivated = false;
        yield return new WaitForSeconds(47f);
        startVideo = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false;
        }
    }
}
