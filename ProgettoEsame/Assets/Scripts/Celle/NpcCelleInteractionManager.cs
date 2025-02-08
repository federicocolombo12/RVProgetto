using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCelleInteractionManager : MonoBehaviour
{
    public static NpcCelleInteractionManager instance;
    public Camera playerCamera;
    public float interactionDistance = 2f;
    public bool paziente0Interaction = false;
    public bool paziente1Interaction = false;
    public bool paziente2Interaction = false;
    public bool paziente3Interaction = false;
    private Paziente0Script paziente0Script;
    private List<Collider> npcs = new List<Collider>(); // Lista degli NPC vicini
    private List<Collider> interactingNpcs = new List<Collider>(); // Lista degli NPC con cui stai interagendo
    public bool isInteracting;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        paziente0Script = FindObjectOfType<Paziente0Script>();
        if (paziente0Script == null)
        {
            Debug.LogError("Paziente0Script non trovato nella scena!");
        }
        playerCamera = Camera.main;
    }

    void Update()
    {
        // Trova tutti gli oggetti vicini
        Collider[] objectList = Physics.OverlapSphere(transform.position, interactionDistance);

        npcs.Clear(); // Svuota la lista prima di aggiornare gli NPC vicini
        foreach (Collider item in objectList)
        {
            // Controlla se è un NPC con tag valido
            if (item.CompareTag("Paziente0") || item.CompareTag("Paziente2") || item.CompareTag("Paziente3"))
            {
                npcs.Add(item); // Aggiungi l'NPC alla lista
            }
        }

        // Gestisci l'interazione solo quando premi il tasto E
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Collider npc in npcs)
            {
                if (!interactingNpcs.Contains(npc))
                {
                    isInteracting = true;

                    LookAtPlayer(npc.transform);
                    interactingNpcs.Add(npc); // Aggiungi l'NPC alla lista degli interagiti

                    if (npc.CompareTag("Paziente0"))
                    {
                        Debug.Log("Giocatore ha interagito con il Paziente 0");
                        paziente0Interaction = true;
                    }
                    else if (npc.CompareTag("Paziente1"))
                    {
                        Debug.Log("Giocatore ha interagito con il Paziente 4");
                        paziente1Interaction = true;
                    }
                    else if (npc.CompareTag("Paziente2"))
                    {
                        Debug.Log("Giocatore ha interagito con il Paziente 2");
                        paziente2Interaction = true;
                    }
                    else if (npc.CompareTag("Paziente3"))
                    {
                        Debug.Log("Giocatore ha interagito con il Paziente 3");
                        paziente3Interaction = true;
                    }

                    // Cambia il tag dell'oggetto corrente
                    npc.gameObject.tag = "Untagged";

                    // Cambia il tag di tutti i figli
                    ChangeTagOfChildren(npc.gameObject, "Untagged");
                }
            }
        }

        // Verifica se qualche NPC è uscito dal range
        for (int i = interactingNpcs.Count - 1; i >= 0; i--)
        {
            if (!npcs.Contains(interactingNpcs[i]))
            {
                // Se l'NPC non è più vicino, interrompi l'interazione
                interactingNpcs.RemoveAt(i);
            }
        }

        // Se non stai più interagendo con nessuno, aggiorna lo stato globale
        if (interactingNpcs.Count == 0)
        {
            isInteracting = false;
        }
    }

    private void LookAtPlayer(Transform npcTransform)
    {
        StartCoroutine(RotateTowards(npcTransform, playerCamera.transform.position));
    }

    public void ReturnToInitialPosition(Transform npcTransform)
    {
        StartCoroutine(RotateToInitialPosition(npcTransform));
    }

    private IEnumerator RotateToInitialPosition(Transform npcTransform)
    {
        Quaternion initialRotation = npcTransform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, -90, 0);
        float elapsedTime = 0f;
        float duration = 1f; // Durata della rotazione in secondi

        while (elapsedTime < duration)
        {
            npcTransform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        npcTransform.rotation = targetRotation;
    }

    private IEnumerator RotateTowards(Transform npcTransform, Vector3 targetPosition)
    {
        Quaternion initialRotation = npcTransform.rotation;
        Vector3 direction = (targetPosition - npcTransform.position).normalized;
        direction.y = 0; // Mantieni la rotazione solo sull'asse Y
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        float elapsedTime = 0f;
        float duration = 1f; // Durata della rotazione in secondi

        while (elapsedTime < duration)
        {
            npcTransform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        npcTransform.rotation = targetRotation;
    }

    private void ChangeTagOfChildren(GameObject parent, string newTag)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.tag = newTag;
            ChangeTagOfChildren(child.gameObject, newTag); // Ricorsione per i figli dei figli
        }
    }
}
