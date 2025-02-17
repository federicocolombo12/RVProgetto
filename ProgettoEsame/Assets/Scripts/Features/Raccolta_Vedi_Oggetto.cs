using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raccolta_Vedi_Oggetto : MonoBehaviour
{
    public float transitionDuration = 1f; // Durata della transizione
    public bool isFlat = false; // Indica se l'oggetto è piatto
    [SerializeField] private bool isViewing = false;
    private Transform player;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Renderer objectRenderer;
    private FirstPersonController playerController; // Riferimento al FirstPersonController
    private Collider objectCollider;
    public bool playAudio; // Booleano per attivare/disattivare l'audio
    public AttivaUi attivaUi;
    private bool isInteracting = false;
    public bool rotazione = false;
    [SerializeField] private float vicinanza = 0.6f;
    [SerializeField] private float customRotation = 0;

    // Variabili per limitare la rotazione indipendentemente dal tipo di oggetto
    public bool limitRotation = false;
    public float minRotationLimit = -45f; // Limite inferiore (in gradi)
    public float maxRotationLimit = 45f;  // Limite superiore (in gradi)

    void Start()
    {
        gameObject.isStatic = false; // Rende l'oggetto non statico
        playAudio = false; // Inizializza l'audio come disattivato
        attivaUi = FindObjectOfType<AttivaUi>();

        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            player = mainCamera.transform;
            playerController = player.GetComponentInParent<FirstPersonController>();
        }
        else
        {
            Debug.LogError("Main Camera non trovata. Assicurati che la tua scena abbia una camera con il tag 'MainCamera'.");
        }

        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogError("Renderer non trovato sull'oggetto. Assicurati che l'oggetto abbia un componente Renderer.");
        }

        objectCollider = GetComponent<Collider>();
        if (objectCollider == null)
        {
            Debug.LogError("Collider non trovato sull'oggetto. Assicurati che l'oggetto abbia un componente Collider.");
        }
    }

    // Metodo pubblico per avviare la visualizzazione
    public void StartViewing()
    {
        StartCoroutine(EnterView());
    }

    public void StopViewing()
    {
        StartCoroutine(ExitView());
    }

    IEnumerator EnterView()
    {
        if (isViewing) yield break;

        isViewing = true;

        // Disabilita il movimento del giocatore
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Disattiva il collider dell'oggetto
        if (objectCollider != null)
        {
            objectCollider.enabled = false;
            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders)
            {
                col.enabled = false;
            }
        }

        // Salva la posizione e la rotazione originali dell'oggetto
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation;

        // Calcola la posizione target: posiziona l'oggetto vicino alla camera
        Vector3 targetPosition = player.position + player.forward * vicinanza;

        // Calcola la rotazione target per far guardare l'oggetto verso la camera
        Quaternion targetRotation = Quaternion.LookRotation(player.position - this.transform.position);

        // Applica un offset in base all'orientamento dell'oggetto
        if (isFlat)
        {
            targetRotation *= Quaternion.Euler(90, customRotation, 0);
        }
        else
        {
            targetRotation *= Quaternion.Euler(0, customRotation, 0);
        }

        // Assicurati che l'oggetto sia visibile
        objectRenderer.enabled = true;

        // Transizione fluida verso la posizione e la rotazione target
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            this.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / transitionDuration);
            this.transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assicurati che l'oggetto sia esattamente nella posizione e rotazione target
        this.transform.position = targetPosition;
        this.transform.rotation = targetRotation;

        rotazione = true;

        // Attiva l'audio
        playAudio = true;
    }

    IEnumerator ExitView()
    {
        Debug.Log("Uscita dalla visualizzazione");
        if (!isViewing) yield break;

        isViewing = false;
        rotazione = false;

        // Disattiva l'audio
        playAudio = false;

        // Riabilita il movimento del giocatore
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // Transizione fluida verso la posizione e la rotazione originali
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, originalPosition, elapsedTime / transitionDuration);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, originalRotation, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assicurati che l'oggetto sia esattamente nella posizione e rotazione originali
        this.transform.position = originalPosition;
        this.transform.rotation = originalRotation;

        // Riattiva il collider dell'oggetto
        if (objectCollider != null)
        {
            objectCollider.enabled = true;
            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders)
            {
                col.enabled = true;
            }
        }
    }

    public void RotateObject()
    {
        float rotationSpeed = 100f;
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        if (limitRotation)
        {
            if (isFlat)
            {
                // Per gli oggetti piatti, limitiamo la rotazione attorno all'asse Z.
                float currentZ = transform.localEulerAngles.z;
                if (currentZ > 180)
                    currentZ -= 360; // Porta l'angolo nel range [-180, 180]
                float newZ = currentZ + mouseX;
                newZ = Mathf.Clamp(newZ, minRotationLimit, maxRotationLimit);

                float clampedMouseX = newZ - currentZ;
                transform.Rotate(Vector3.forward, clampedMouseX, Space.Self);
            }
            else
            {
                // Per gli altri oggetti, limitiamo la rotazione attorno all'asse Y.
                float currentY = transform.localEulerAngles.y;
                if (currentY > 180)
                    currentY -= 360; // Porta l'angolo nel range [-180, 180]
                float newY = currentY + mouseX;
                newY = Mathf.Clamp(newY, minRotationLimit, maxRotationLimit);

                float clampedMouseX = newY - currentY;
                transform.Rotate(Vector3.up, clampedMouseX, Space.Self);
            }
        }
        else
        {
            // Rotazione libera: usa l'asse in base al valore di isFlat.
            if (isFlat)
            {
                transform.Rotate(Vector3.forward, mouseX, Space.Self);
            }
            else
            {
                transform.Rotate(Vector3.up, mouseX, Space.Self);
            }
        }
    }



}
