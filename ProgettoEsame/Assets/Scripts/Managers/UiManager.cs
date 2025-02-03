using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] private float interactRadius = 1f;
    [SerializeField] private GameObject uiPrefab; // Prefab per gli elementi UI
    [SerializeField] private int poolSize = 10; // Dimensione del pool
    [SerializeField] private float altezza = 0.5f;
    [SerializeField] private Collider[] colliders;// Per rilevare gli oggetti vicini
    
    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;
    private Queue<GameObject> uiPool;
    private List<GameObject> activeUis; // Per tenere traccia degli elementi attivi

    private void Awake()
    {
        

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

        // Secondo OverlapSphere: solo gli oggetti entro l'interactRadius
        Collider[] interactColliders = Physics.OverlapSphere(transform.position, interactRadius);

        // Creiamo un semplice HashSet per verificare velocemente se un collider è nell'interact radius
        HashSet<Collider> interactSet = new HashSet<Collider>(interactColliders);

        // Disattiva tutte le UI attive prima di aggiornare
        foreach (var ui in new List<GameObject>(activeUis))
        {
            ReturnUiToPool(ui);
        }

        // Per ogni collider rilevato entro il detectionRadius
        foreach (Collider collider in detectedColliders)
        {
            if (collider.CompareTag("OggettoInteragibile1"))
            {
                // Ottieni un elemento UI dal pool
                GameObject uiElement = GetUiFromPool();
                if (uiElement != null)
                {
                    // Recupera il componente Image dal prefab
                    Image imageComponent = uiElement.GetComponentInChildren<Image>();
                    if (imageComponent != null)
                    {
                        // Se il collider è anche presente nell'interact sphere, usa la sprite2,
                        // altrimenti usa la sprite1
                        Debug.Log("Collider: " + collider);
                        if (interactSet.Contains(collider))
                        {
                            Debug.Log("Vicino: " + collider);
                            imageComponent.sprite = sprite2;
                        }
                        else
                        {
                           Debug.Log("Lontano: " + collider);
                            imageComponent.sprite = sprite1;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Image component non trovato nell'elemento UI!");
                    }

                    // Posiziona l'elemento UI sopra l'oggetto, aggiungendo l'altezza desiderata
                    Vector3 screenPosition = collider.transform.position + Vector3.up * altezza;
                    uiElement.GetComponent<RectTransform>().position = screenPosition;
                }
            }
        }
    }

}
