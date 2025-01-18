using System.Collections.Generic;
using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 2f; // Distanza di interazione
    private Camera mainCamera;
    public bool isInteracting;
    private List<Collider> npcs = new List<Collider>(); // Usa una lista per gestire dinamicamente gli NPC

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
                var npcScript = item.GetComponent<NpcScript>();
                if (npcScript != null)
                {
                    isInteracting = true;
                    npcScript.Interact(mainCamera.transform);
                    npcs.Add(item); // Aggiungi l'NPC alla lista
                }
            }
        }

        // Se nessun NPC è vicino, interrompi l'interazione
        if (npcs.Count == 0)
        {
            isInteracting = false;
        }
    }
}
