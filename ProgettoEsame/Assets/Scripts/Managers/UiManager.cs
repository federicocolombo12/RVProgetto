using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UiManager instance;
    
    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] GameObject uiEntra;
    [SerializeField] GameObject uiVicinanza;
    [SerializeField] GameObject uiEsci;
    public bool vedi = false;
    public bool esci = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        
    }
    public void AttivaVicinanza(GameObject player)

    {

        

        // Rileva i collider nell'area
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, detectionRadius);

        bool oggettoTrovato = false;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("OggettoInteragibile1") || collider.CompareTag("NPC1") || collider.CompareTag("NPC2"))
            {
                // Calcola la posizione dello schermo
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(collider.transform.position);
                uiVicinanza.SetActive(true);
                uiVicinanza.GetComponent<Image>().transform.position = screenPosition;
                oggettoTrovato = true;
                break;
            }
        }

        // Disattiva il canvas se non ci sono oggetti validi
        if (!oggettoTrovato || uiEntra.activeSelf || uiEsci.activeSelf)
        {
            uiVicinanza.SetActive(false);
        }
       






    }
    public void AttivaVedi()
    {
        if (vedi) 
        {
            uiEntra.SetActive(true);
            uiEsci.SetActive(false);
        }
        else if (!vedi)
        {
               uiEntra.SetActive(false);
            
        
        }
        


    }
    public void AttivaEsci()
    {
        if (esci) {
            uiEntra.SetActive(false);
            uiEsci.SetActive(true);
        }
        else if (!esci)
           {
            
            
            uiEsci.SetActive(false);
        }
       
    }

}
