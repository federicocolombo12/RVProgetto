using System.Collections.Generic;
using UnityEngine;



public class UiIdentifier : MonoBehaviour
{
    public new string tag;
}

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [System.Serializable]
    public class InteractableType
    {
        public string tag;
        public GameObject uiPrefab;
        public float uiHeight = 0.5f;
    }

    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] private float interactRadius = 1f;
    [SerializeField] private InteractableType[] interactableTypes; // Array di configurazioni per i tipi interagibili
    [SerializeField] private int poolSize = 10; // Dimensione del pool totale
    [SerializeField] private Collider[] colliders; // Array temporaneo per rilevare gli oggetti vicini

    [SerializeField] private float boxLength = 2f;
    [SerializeField] private float boxWidth = 0.2f;
    [SerializeField] private float boxHeight = 0.2f;

    [SerializeField] public Camera playerCamera; // Riferimento alla camera del giocatore

    private Dictionary<string, Queue<GameObject>> pools;
    private List<GameObject> activeUis;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        colliders = new Collider[5]; // Initialize the colliders array with a default size
        InitializePools();
    }

    // Inizializza i pool per ciascun tipo di UI
    private void InitializePools()
    {
        pools = new Dictionary<string, Queue<GameObject>>();
        activeUis = new List<GameObject>();

        foreach (var type in interactableTypes)
        {
            var queue = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject uiInstance = Instantiate(type.uiPrefab, transform);
                uiInstance.SetActive(false);
                queue.Enqueue(uiInstance);
            }
            pools[type.tag] = queue;
        }
    }

    // Ottieni un elemento UI dal pool specifico per il tag
    private GameObject GetUiFromPool(string tag)
    {
        if (pools.ContainsKey(tag) && pools[tag].Count > 0)
        {
            GameObject uiInstance = pools[tag].Dequeue();
            uiInstance.SetActive(true);

            // Assicurati che l'oggetto UI abbia un UiIdentifier e assegnagli il tag
            UiIdentifier identifier = uiInstance.GetComponent<UiIdentifier>();
            if (identifier == null)
            {
                identifier = uiInstance.AddComponent<UiIdentifier>(); // Se manca, lo aggiunge
            }
            identifier.tag = tag;

            activeUis.Add(uiInstance);
            return uiInstance;
        }
        else
        {
            Debug.LogWarning("Pool for " + tag + " is exhausted! Consider increasing the pool size.");
            return null;
        }
    }


    // Rilascia un elemento nel pool generico
    private void ReturnUiToPool(GameObject uiInstance, string tag)
    {
        if (activeUis.Contains(uiInstance))
        {
            activeUis.Remove(uiInstance);
            uiInstance.SetActive(false);
            if (pools.ContainsKey(tag))
            {
                pools[tag].Enqueue(uiInstance);
            }
        }
    }

    private void Update()
    {
        RilevaOggettiVicini();
    }

    // Rileva oggetti vicini e gestisce la UI
    private void RilevaOggettiVicini()
    {
        // Rileva tutti i collider entro il detectionRadius
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, detectionRadius);

        // Parametri per OverlapBoxNonAlloc
        Vector3 boxCenter = playerCamera.transform.position + playerCamera.transform.forward * (boxLength / 2);
        Vector3 halfExtents = new Vector3(boxWidth / 2, boxHeight / 2, boxLength / 2);
        Quaternion boxRotation = playerCamera.transform.rotation;

        // Secondo OverlapBoxNonAlloc: solo gli oggetti entro l'interactRadius
        int numColliders = Physics.OverlapBoxNonAlloc(boxCenter, halfExtents, colliders, boxRotation);

        // Creiamo un semplice HashSet per verificare velocemente se un collider è nell'interact radius
        HashSet<Collider> interactSet = new HashSet<Collider>();
        for (int i = 0; i < numColliders; i++)
        {
            interactSet.Add(colliders[i]);
        }

        // Disattiva tutte le UI attive prima di aggiornare
        foreach (var ui in new List<GameObject>(activeUis))
        {
            UiIdentifier identifier = ui.GetComponent<UiIdentifier>();
            if (identifier != null)
            {
                ReturnUiToPool(ui, identifier.tag);
            }
        }

        // Per ogni collider rilevato entro il detectionRadius
        foreach (Collider collider in detectedColliders)
        {
            foreach (var type in interactableTypes)
            {
                if (collider.CompareTag(type.tag))
                {
                    // Ottieni un elemento UI dal pool
                    GameObject uiElement = GetUiFromPool(type.tag);
                    if (uiElement != null)
                    {
                        if (interactSet.Contains(collider))
                        {
                            Debug.Log("Vicino: " + collider);

                            // Attiva "Interagisci" e disattiva "Indicatore_Vicinanza"
                            Transform interagisci = uiElement.transform.Find("Interagisci");
                            Transform background = uiElement.transform.Find("Background");
                            Transform indicatoreVicinanza = uiElement.transform.Find("Indicatore_Vicinanza");
                            if (interagisci != null) interagisci.gameObject.SetActive(true);
                            if (indicatoreVicinanza != null) indicatoreVicinanza.gameObject.SetActive(false);
                            if (background != null) background.gameObject.SetActive(true);
                        }
                        else
                        {
                            Debug.Log("Lontano: " + collider);

                            // Disattiva "Interagisci" e attiva "Indicatore_Vicinanza"
                            Transform background = uiElement.transform.Find("Background");
                            Transform interagisci = uiElement.transform.Find("Interagisci");
                            Transform indicatoreVicinanza = uiElement.transform.Find("Indicatore_Vicinanza");
                            if (interagisci != null) interagisci.gameObject.SetActive(false);
                            if (indicatoreVicinanza != null) indicatoreVicinanza.gameObject.SetActive(true);
                            if (background != null) background.gameObject.SetActive(false);
                        }

                        // Posiziona l'elemento UI sopra l'oggetto, aggiungendo l'altezza desiderata
                        Vector3 screenPosition = collider.transform.position + Vector3.up * type.uiHeight;
                        uiElement.GetComponent<RectTransform>().position = screenPosition;
                    }
                    break;
                }
            }
        }
    }

    private void HandleUIActivation(HashSet<Collider> interactSet, Collider collider, GameObject uiElement, float uiHeight)
    {
        bool isNear = interactSet.Contains(collider);

        // Qui puoi personalizzare ulteriormente il comportamento di attivazione/disattivazione basandoti su isNear, ecc.
        uiElement.transform.position = collider.transform.position + Vector3.up * uiHeight;
        uiElement.SetActive(isNear); // Attiva o disattiva l'elemento UI in base alla vicinanza
    }
}