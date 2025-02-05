using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public static QueueManager instance { get; private set; }
    public List<GameObject> characters; // Lista dei personaggi in fila
    public Transform medicinePoint;     // Punto della medicina
    public Transform exitPoint;         // Punto di uscita
    public float delayBetweenTurns = 3f; // Tempo tra un turno e l'altro
    private int currentIndex = 0;       // Indice del personaggio attuale
    public bool lineFinished = false;   // Indica se la fila è finita
    private bool coroutineRunning = false; // Indica se la coroutine è in esecuzione
    [SerializeField] private PlayerLock playerLock;
    private DialogueManager dialogueManager; // Riferimento al DialogueManager
    private bool audioPlayed = false; // Variabile per tenere traccia se l'audio è stato riprodotto

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;

        // Trova l'oggetto DialogueManager nella scena
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if (InfermieriaManager.instance.isInRow && !coroutineRunning)
        {
            // Riproduci l'audio solo la prima volta che la fila inizia
            if (!audioPlayed)
            {
                dialogueManager?.PlayStartQueueAudio();
                audioPlayed = true; // Imposta audioPlayed a true per evitare che venga riprodotto nuovamente
            }

            StartCoroutine(ProcessQueue());
        }
    }

    private IEnumerator ProcessQueue()
    {
        playerLock.enabled = true;
        coroutineRunning = true;
        while (currentIndex < characters.Count)
        {
            GameObject currentCharacter = characters[currentIndex];

            // Inizia le azioni per il personaggio attuale
            CharacterBehavior behavior = currentCharacter.GetComponent<CharacterBehavior>();
            behavior.medicinePoint = medicinePoint;
            behavior.exitPoint = exitPoint;
            behavior.StartActions();

            // Aspetta che il personaggio completi le sue azioni
            yield return new WaitUntil(() => behavior.HasFinished);

            // Passa al prossimo personaggio
            currentIndex++;
            yield return new WaitForSeconds(delayBetweenTurns);
        }
        yield return new WaitForSeconds(2f);
        lineFinished = true;

        coroutineRunning = false;
    }
}
