using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UiManager instance;
    
    [SerializeField] private float detectionRadius = 2f;
    


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
    public void AttivaVicinanza(GameObject canvas, GameObject player)

    {

        if (canvas == null) return;

        // Rileva i collider nell'area
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, detectionRadius);

        bool oggettoTrovato = false;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("OggettoInteragibile1") || collider.CompareTag("NPC1") || collider.CompareTag("NPC2"))
            {
                // Calcola la posizione dello schermo
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(collider.transform.position);
                canvas.SetActive(true);
                canvas.GetComponent<RectTransform>().position = screenPosition;
                oggettoTrovato = true;
                break;
            }
        }

        // Disattiva il canvas se non ci sono oggetti validi
        if (!oggettoTrovato)
        {
            canvas.SetActive(false);
        }







    }
    public void AttivaVedi(GameObject canvas)
    {
        canvas.SetActive(true);

    }
    public void AttivaEsci(GameObject canvas)
    {
        canvas.SetActive(true);
    }

}
