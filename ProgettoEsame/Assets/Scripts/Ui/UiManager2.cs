using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager2 : MonoBehaviour
{
    public static UiManager2 instance;

    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] private GameObject uiPrefab; // Prefab per gli elementi UI
    [SerializeField] private int poolSize = 10; // Dimensione del pool
    [SerializeField] private float altezza = 0.5f;
    [SerializeField] private Collider[] colliders; // Per rilevare gli oggetti vicini
    private Queue<GameObject> uiPool;
    private List<GameObject> activeUis; // Per tenere traccia degli elementi attivi

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

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
        colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        // Disattiva tutte le UI attive prima di aggiornare
        foreach (var ui in new List<GameObject>(activeUis))
        {
            ReturnUiToPool(ui);
        }

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("OggettoInteragibile2"))
            {
                // Ottieni un elemento dal pool
                GameObject uiElement = GetUiFromPool();
                if (uiElement != null)
                {
                    // Posiziona l'elemento UI sopra l'oggetto
                    Vector3 screenPosition = collider.transform.position + Vector3.up * altezza;
                    uiElement.GetComponent<RectTransform>().position = screenPosition;
                }
            }
        }
    }
}
