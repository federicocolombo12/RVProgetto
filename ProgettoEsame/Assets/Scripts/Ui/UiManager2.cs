using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager2 : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] private float interactRadius = 1f;
    [SerializeField] private GameObject uiPrefab; // Prefab per gli elementi UI
    [SerializeField] private int poolSize = 10; // Dimensione del pool
    [SerializeField] private float altezza = 0.5f;
    [SerializeField] private Collider[] colliders; // Per rilevare gli oggetti vicini

    [SerializeField] private float boxLength = 2f;
    [SerializeField] private float boxWidth = 0.2f;
    [SerializeField] private float boxHeight = 0.2f;


    [SerializeField] public Camera playerCamera; // Riferimento alla camera del giocatore

    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;
    private Queue<GameObject> uiPool;
    private List<GameObject> activeUis; // Per tenere traccia degli elementi attivi

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        InitializePool();
    }

    // Inizializza il pool
    private void InitializePool()
    {
        uiPool = new Queue<GameObject>();
        activeUis = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject uiInstance = Instantiate(uiPrefab, transform);
            uiInstance.SetActive(false);
            uiPool.Enqueue(uiInstance);
        }
    }

    // Ottieni un elemento dal pool
    private GameObject GetUiFromPool()
    {
        if (uiPool.Count > 0)
        {
            GameObject uiInstance = uiPool.Dequeue();
            uiInstance.SetActive(true);
            activeUis.Add(uiInstance);
            return uiInstance;
        }
        else
        {
            Debug.LogWarning("Pool esaurito! Aumenta la dimensione del pool.");
            return null;
        }
    }

    // Rilascia un elemento nel pool
    private void ReturnUiToPool(GameObject uiInstance)
    {
        if (activeUis.Contains(uiInstance))
        {
            activeUis.Remove(uiInstance);
            uiInstance.SetActive(false);
            uiPool.Enqueue(uiInstance);
        }
    }

    private void Update()
    {
        // Ad esempio, attiva gli UI in base alla vicinanza
        RilevaOggettiVicini();
    }

    // Rileva oggetti vicini e gestisce la UI
    private void RilevaOggettiVicini()
    {
        // Primo OverlapSphere: tutti gli oggetti entro il detectionRadius
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
            ReturnUiToPool(ui);
        }

        // Per ogni collider rilevato entro il detectionRadius
        foreach (Collider collider in detectedColliders)
        {
            if (collider.CompareTag("OggettoInteragibile2"))
            {
                // Ottieni un elemento UI dal pool
                GameObject uiElement = GetUiFromPool();
                if (uiElement != null)
                {
                    // Recupera il componente Image dal prefab
                    if (interactSet.Contains(collider))
                    {
                        Debug.Log("Vicino: " + collider);

                        // Attiva il GameObject "Interagisci" e disattiva "Indicatore_Vicinanza"
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

                        // Disattiva il GameObject "Interagisci" e attiva "Indicatore_Vicinanza"
                        Transform background = uiElement.transform.Find("Background");
                        Transform interagisci = uiElement.transform.Find("Interagisci");
                        Transform indicatoreVicinanza = uiElement.transform.Find("Indicatore_Vicinanza");
                        if (interagisci != null) interagisci.gameObject.SetActive(false);
                        if (indicatoreVicinanza != null) indicatoreVicinanza.gameObject.SetActive(true);
                        if (background != null) background.gameObject.SetActive(false);
                    }

                    // Posiziona l'elemento UI sopra l'oggetto, aggiungendo l'altezza desiderata
                    Vector3 screenPosition = collider.transform.position + Vector3.up * altezza;
                    uiElement.GetComponent<RectTransform>().position = screenPosition;
                }
            }
        }
    }

    // Metodo per visualizzare la OverlapBox
    private void OnDrawGizmosSelected()
    {
        if (playerCamera != null)
        {
            Vector3 boxCenter = playerCamera.transform.position + playerCamera.transform.forward * (boxLength / 2);
            Vector3 halfExtents = new Vector3(boxWidth / 2, boxHeight / 2, boxLength / 2);
            Quaternion boxRotation = playerCamera.transform.rotation;
            Gizmos.color = Color.green;
            Gizmos.matrix = Matrix4x4.TRS(boxCenter, boxRotation, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2);
        }
    }
}
