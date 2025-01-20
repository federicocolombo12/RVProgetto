using System.Collections.Generic;
using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 2f; // Distanza di interazione
    private Camera mainCamera;
    public bool isInteracting;
    private NpcHeadLookAt npcHeadLookAt;
    [SerializeField] private List<Collider> npcs = new List<Collider>(); // Lista degli NPC vicini
    [SerializeField] private List<Collider> interactingNpcs = new List<Collider>(); // Lista degli NPC con cui stai interagendo

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Trova tutti gli oggetti vicini
        Collider[] objectList = Physics.OverlapSphere(transform.position, interactionDistance);

        npcs.Clear(); // Svuota la lista prima di aggiornare gli NPC vicini
        foreach (Collider item in objectList)
        {
            // Controlla se è un NPC con tag valido
            if (item.CompareTag("NPC1") || item.CompareTag("NPC2"))
            {
                npcs.Add(item); // Aggiungi l'NPC alla lista
            }
        }

        // Gestisci l'interazione solo quando premi il tasto E
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Collider npc in npcs)
            {
                var npcScript = npc.GetComponent<NpcScript>();
                if (npcScript != null && !interactingNpcs.Contains(npc))
                {
                    isInteracting = true;

                    npcScript.Interact(mainCamera.transform);
                    interactingNpcs.Add(npc); // Aggiungi l'NPC alla lista degli interagiti
                }
            }
        }

        // Verifica se qualche NPC è uscito dal range
        for (int i = interactingNpcs.Count - 1; i >= 0; i--)
        {
            if (!npcs.Contains(interactingNpcs[i]))
            {
                // Se l'NPC non è più vicino, interrompi l'interazione
                var npcScript = interactingNpcs[i].GetComponent<NpcScript>();
                if (npcScript != null)
                {
                    npcScript.StopInteract();
                }
                interactingNpcs.RemoveAt(i);
            }
        }

        // Se non stai più interagendo con nessuno, aggiorna lo stato globale
        if (interactingNpcs.Count == 0)
        {
            isInteracting = false;
        }
    }
}
