using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raccolta_Vedi_Oggetto : MonoBehaviour
{
    public float interactionDistance = 3f;
    public float transitionDuration = 1f; // Durata della transizione
    public Vector3 targetPositionOffset = new Vector3(0, 0, 1.0f); // Offset della posizione target rispetto alla camera
    public Vector3 targetRotationEuler = new Vector3(0, 180, 0); // Rotazione target in gradi (verticale)
    private bool isViewing = false;
    private Transform player;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Renderer objectRenderer;
    private FirstPersonController playerController; // Riferimento al FirstPersonController

    void Start()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            player = mainCamera.transform;
            playerController = player.GetComponentInParent<FirstPersonController>(); // Assumi che il FirstPersonController sia sul genitore della camera
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
    }

    void Update()
    {
        if (isViewing)
        {
            RotateObject();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(ExitView());
            }
        }
        else
        {
            CheckForPlayer();
        }
    }

    void CheckForPlayer()
    {
        if (player == null || objectRenderer == null)
        {
            return;
        }

        float distance = Vector3.Distance(player.position, transform.position);
        Debug.Log("Distanza dal giocatore: " + distance);

        if (distance <= interactionDistance)
        {
            Debug.Log("Giocatore entro la distanza di interazione.");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Tasto E premuto.");
                StartCoroutine(EnterView());
            }
        }
    }

    IEnumerator EnterView()
    {
        if (isViewing) yield break;

        isViewing = true;
        Debug.Log("Entrato in modalità visualizzazione.");

        // Disabilita il movimento del giocatore
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Salva la posizione e la rotazione originali dell'oggetto
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // Calcola la posizione e la rotazione target
        Vector3 targetPosition = player.position + player.TransformDirection(targetPositionOffset); // Posiziona l'oggetto molto vicino alla camera
        Quaternion targetRotation = Quaternion.LookRotation(player.forward) * Quaternion.Euler(targetRotationEuler);

        Debug.Log("Posizione target: " + targetPosition);
        Debug.Log("Rotazione target: " + targetRotation);

        // Assicurati che l'oggetto sia visibile
        objectRenderer.enabled = true;

        // Transizione fluida verso la posizione e la rotazione target
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / transitionDuration);
            transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assicurati che l'oggetto sia esattamente nella posizione e rotazione target
        transform.position = targetPosition;
        transform.rotation = targetRotation;

        Debug.Log("Oggetto posizionato davanti al giocatore.");
    }

    IEnumerator ExitView()
    {
        if (!isViewing) yield break;

        isViewing = false;
        Debug.Log("Uscito dalla modalità visualizzazione.");

        // Riabilita il movimento del giocatore
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // Transizione fluida verso la posizione e la rotazione originali
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, elapsedTime / transitionDuration);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assicurati che l'oggetto sia esattamente nella posizione e rotazione originali
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        Debug.Log("Oggetto riposizionato nella posizione originale.");
    }

    void RotateObject()
    {
        float rotationSpeed = 100f;
        float horizontal = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, -horizontal, Space.World);
        transform.Rotate(Vector3.right, vertical, Space.World);
    }
}