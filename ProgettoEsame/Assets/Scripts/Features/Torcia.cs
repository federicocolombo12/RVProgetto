using System.Collections;
using UnityEngine;

public class Torcia : MonoBehaviour
{
    public GameObject flashlight;
    public Transform flashlightTransform;

    [SerializeField] private bool on;
    [SerializeField] private bool off;
    [SerializeField] private float flickerTime = 2.5f;
    [SerializeField] private float elapsedTime;

    private Camera mainCamera;
    private Vector3 defaultRotation = new Vector3(0f, 0f, 0f);

    [SerializeField] private TorciaAudio torciaAudio; // Riferimento allo script audio

    void Start()
    {
        on = true;
        off = false;
        flashlight.SetActive(false);
        mainCamera = Camera.main;

        // Controlla se lo script audio è assegnato
        if (torciaAudio == null)
        {
            torciaAudio = GetComponent<TorciaAudio>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic sinistro per accendere/spegnere la torcia
        {
            if (off)
            {
                flashlight.SetActive(true);
                off = false;
                on = true;
                Debug.Log("Torcia accesa");

                // Riproduce il suono di accensione
                if (torciaAudio != null)
                {
                    torciaAudio.PlayAccensioneSound();
                }
            }
            else if (on)
            {
                flashlight.SetActive(false);
                off = true;
                on = false;
                Debug.Log("Torcia spenta");

                // Riproduce il suono di spegnimento
                if (torciaAudio != null)
                {
                    torciaAudio.PlaySpegnimentoSound();
                }
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("OggettoInteragibile1"))
            {
                Vector3 directionToTarget = hit.point - flashlightTransform.position;
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                flashlightTransform.rotation = Quaternion.Slerp(flashlightTransform.rotation, targetRotation, Time.deltaTime * 5f);
            }
            else
            {
                flashlightTransform.localRotation = Quaternion.Euler(defaultRotation);
            }
        }
        else
        {
            flashlightTransform.localRotation = Quaternion.Euler(defaultRotation);
        }
    }

    public void Flickering()
    {
        StartCoroutine(FlickeringLight());
    }

    IEnumerator FlickeringLight()
    {
        elapsedTime = 0f;
        while (elapsedTime < flickerTime)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            flashlight.SetActive(!flashlight.activeSelf);
            elapsedTime += Time.deltaTime * 10;
        }
    }
}
