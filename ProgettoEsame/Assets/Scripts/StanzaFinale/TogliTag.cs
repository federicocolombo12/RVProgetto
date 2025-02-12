using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogliTag : MonoBehaviour
{
    [SerializeField] private ObjectInteraction objectInteraction; // Riferimento allo script ObjectInteraction
    private string originalTag; // Variabile per memorizzare il tag originale

    // Start is called before the first frame update
    void Start()
    {
        // Verifica che il riferimento sia assegnato
        if (objectInteraction == null)
        {
            Debug.LogError("ObjectInteraction non assegnato su " + gameObject.name);
        }

        // Memorizza il tag originale del GameObject
        originalTag = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        // Esempio di utilizzo del riferimento a ObjectInteraction
        if (objectInteraction != null)
        {
            if (objectInteraction.aspetta)
            {
                // Ripristina il tag originale
                gameObject.tag = originalTag;
                Debug.Log("Tag ripristinato a " + originalTag);
            }
            else
            {
                // Rimuovi il tag
                gameObject.tag = "Untagged";
                Debug.Log("Tag rimosso");
            }
        }
    }
}






