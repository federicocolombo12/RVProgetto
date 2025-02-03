// GlassBreaker.cs
using UnityEngine;

public class GlassBreaker : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject intactGlass;
    [SerializeField] private GameObject brokenGlass;
    [SerializeField] private float breakForce = 10f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private AudioClip breakSound;

    private bool isBroken = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        brokenGlass.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BreakGlass();
        }
    }
    public void BreakGlass()
    {
        if(isBroken) return;

        // Disattiva vetro intatto
        intactGlass.SetActive(false);

        // Attiva frammenti
        brokenGlass.SetActive(true);
        isBroken = true;

        // Applica forza esplosiva
        foreach(Rigidbody rb in brokenGlass.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddExplosionForce(breakForce, transform.position, explosionRadius);
        }

        // Suono
        if(breakSound != null)
        {
            audioSource.PlayOneShot(breakSound);
        }

        // Distruggi dopo 5 secondi
        Destroy(gameObject, 5f);
    }

    
}