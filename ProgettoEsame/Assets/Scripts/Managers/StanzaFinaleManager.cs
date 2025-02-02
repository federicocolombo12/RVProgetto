using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StanzaFinaleManager : MonoBehaviour
{
    public static StanzaFinaleManager instance { get; private set; }

    [Header("Oggetti Interagibili")]
    public GameObject oggetto1;
    public GameObject oggetto2;
    public GameObject oggetto3;
    public GameObject oggetto4;
    public GameObject quintoOggetto; // Il quinto oggetto che avvia il video

    [Header("Stato Interazione")]
    public bool Oggetto1 = false;
    public bool Oggetto2 = false;
    public bool Oggetto3 = false;
    public bool Oggetto4 = false;
    public bool ultimoOggetto = false; // Stato del quinto oggetto


    [Header("Trigger Animazioni")]
    [SerializeField] private string triggerWalk = "TriggerWalk";
    [SerializeField] private string triggerIdle = "TriggerIdle";
    [SerializeField] private string triggerBackward = "TriggerBackward";

    [SerializeField] private Animator animator; // Riferimento all'Animator

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        

        if (animator == null)
        {
            Debug.LogError("Nessun componente Animator assegnato.");
        }
        else
        {
            StartCoroutine(TriggerSequence());
        }
    }

    private void Update()
    {
        

        
    }

    // Metodo per avviare il video
    public void LoadStanzaPassato()
    {
        MySceneManager.instance.LoadNextScene("ScenaFinaleElettroshockPassato", UnityEngine.SceneManagement.LoadSceneMode.Single, () =>
        {
            Debug.Log("StanzaPassato caricata con successo!");
        });
    }

    // Metodo per controllare se un oggetto è stato interagito
    public void CheckInteraction(GameObject interactedObject)
    {
        if (interactedObject == oggetto1)
        {
            Oggetto1 = true;
            Debug.Log("Interagito con Oggetto1.");
        }
        else if (interactedObject == oggetto2)
        {
            Oggetto2 = true;
            Debug.Log("Interagito con Oggetto2.");
        }
        else if (interactedObject == oggetto3)
        {
            Oggetto3 = true;
            Debug.Log("Interagito con Oggetto3.");
        }
        else if (interactedObject == oggetto4)
        {
            Oggetto4 = true;
            Debug.Log("Interagito con Oggetto4.");
        }
        else if (interactedObject == quintoOggetto)
        {
            ultimoOggetto = true;
            Debug.Log("Interagito con l'ultimo oggetto. Video verrà avviato.");
            LoadStanzaPassato();
        }
        else
        {
            Debug.LogWarning("Oggetto non riconosciuto o interazione non consentita: " + interactedObject.name);
        }
    }

    // Coroutine per attivare i trigger in sequenza
    private IEnumerator TriggerSequence()
    {
        yield return new WaitForSeconds(2f);
        animator.SetTrigger(triggerWalk);
        Debug.Log("TriggerWalk attivato.");

        yield return new WaitForSeconds(10f);
        animator.SetTrigger(triggerIdle);
        Debug.Log("TriggerIdle attivato.");
        /*
        yield return new WaitForSeconds(2f);
        animator.SetTrigger(triggerBackward);
        Debug.Log("TriggerBackward attivato.");
        */
    }
}

