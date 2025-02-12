using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloccaMovimento : MonoBehaviour
{
    public ObjectInteraction objectInteraction; // Da assegnare manualmente nell'Inspector
    private FirstPersonController firstPersonController;

    void Start()
    {
        firstPersonController = GetComponent<FirstPersonController>();
        if (firstPersonController == null)
        {
            Debug.LogError("FirstPersonController non trovato su " + gameObject.name);
        }

        if (objectInteraction == null)
        {
            Debug.LogError("ObjectInteraction non è stato assegnato nell'Inspector.");
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Esempio di utilizzo del riferimento a ObjectInteraction
        if (objectInteraction != null && objectInteraction.blocca)
        {
            // Blocca il movimento del personaggio ma non la rotazione
            if (firstPersonController != null)
            {
                firstPersonController.playerCanMove=false;
            }
        }
    }
}




