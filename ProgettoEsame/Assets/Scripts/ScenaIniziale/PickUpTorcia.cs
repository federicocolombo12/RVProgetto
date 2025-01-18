using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpTorcia : MonoBehaviour
{
    /*public float interactionDistance = 2f;
    public float transitionDuration = 1f; // Durata della transizione
    // public Vector3 targetPositionOffset = new Vector3(0, 0, 0.01f); // Offset della posizione target rispetto alla camera
    public bool isFlat = false; // Variabile per indicare se l'oggetto � coricato
    private bool isViewing = false;
    private Transform player;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Renderer objectRenderer;
    private FirstPersonController playerController; // Riferimento al FirstPersonController
    public LayerMask interactableLayer; // Layer per gli oggetti interagibili
    [SerializeField] private CassettoOpener cassettoScript;

    void Start()
    {
        // Assicurati che l'oggetto non sia statico
        gameObject.isStatic = false; // per ora fai cosi, ma poi basta levare static al prefab dell'oggetto e questa riga si pu� eliminare

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
            if (Input.GetKeyDown(KeyCode.E))
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

        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        // Disegna il raggio nel Scene View per il debug
        Debug.DrawRay(player.position, player.forward * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer)) // ricordati di mettere il layer Interaclable agli oggetti su unity
        {
            Debug.Log("Raycast ha colpito: " + hit.transform.name);
            if (hit.transform == this.transform)
            {
                Debug.Log("Giocatore sta guardando l'oggetto.");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Tasto E premuto.");
                    StartCoroutine(EnterView());
                }
            }
        }
        
    }

    IEnumerator EnterView()
    {
        if (isViewing) yield break;
        cassettoScript.enabled =false;

        isViewing = true;
        Debug.Log("Entrato in modalit� visualizzazione.");

        // Disabilita il movimento del giocatore
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Salva la posizione e la rotazione originali dell'oggetto
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation;
        gameObject.GetComponent<Collider>().enabled = false;

        // Calcola la posizione target
        Vector3 targetPosition = player.position + player.forward * 0.6f; // Posiziona l'oggetto molto vicino alla camera

        // Calcola la rotazione target per far guardare l'oggetto verso la camera
        Quaternion targetRotation = Quaternion.LookRotation(player.position - this.transform.position);

        // Aggiungi un offset di rotazione in base all'orientamento dell'oggetto
        if (isFlat)
        {
            // L'oggetto � coricato
            targetRotation *= Quaternion.Euler(90, 0, 0);
        }
        else
        {
            // L'oggetto � in piedi
            targetRotation *= Quaternion.Euler(0, 180, 0);
        }

        Debug.Log("Posizione target: " + targetPosition);
        Debug.Log("Rotazione target: " + targetRotation);

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

        Debug.Log("Oggetto posizionato davanti al giocatore.");
    }

    IEnumerator ExitView()
    {
        if (!isViewing) yield break;
        cassettoScript.enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;

        isViewing = false;
        Debug.Log("Uscito dalla modalità visualizzazione.");

        // Calculate the target position to be in the bottom right of the view
        Vector3 targetPosition = player.position + player.forward * 0.6f + player.right * 0.5f - player.up * 0.5f;

        // Calculate the target rotation to face the camera
        Quaternion targetRotation = Quaternion.LookRotation(player.forward);



        // Re-enable player movement
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, elapsedTime / transitionDuration);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object is exactly at the target position and rotation
        this.transform.position = targetPosition;
        this.transform.rotation = targetRotation;

        Destroy(this.gameObject);
        FirstSceneManager.instance.doorOpenable = true;
    }

    void RotateObject()
    {
        float rotationSpeed = 100f;
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        if (isFlat)
        {
            // Ruota l'oggetto coricato sull'asse Z
            this.transform.Rotate(Vector3.forward, mouseX, Space.Self);
        }
        else
        {
            // Ruota l'oggetto in piedi sull'asse Y
            this.transform.Rotate(Vector3.up, mouseX, Space.Self);
        }
    }*/
    private GameObject pickedObject = null;
    public Transform holdPosition;
    public LayerMask interactableLayer; // LayerMask per gli oggetti interagibili
    private float animationSpeed = 10.0f; // Velocità di animazione per raccogliere e posare l'oggetto
    private float maxPickupDistance = 3.0f; // Distanza massima per raccogliere l'oggetto
    private float maxDropDistance = 3.0f; // Distanza massima per posare l'oggetto
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isViewing = false;
    private bool isHolding = false;
    private FirstPersonController playerController; // Riferimento al FirstPersonController
    public bool isFlatObject = false; // Flag per determinare se l'oggetto è piatto

    void Start()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            playerController = mainCamera.GetComponentInParent<FirstPersonController>(); // Assumi che il FirstPersonController sia sul genitore della camera
        }
        else
        {
            Debug.LogError("Main Camera non trovata. Assicurati che la tua scena abbia una camera con il tag 'MainCamera'.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (pickedObject == null)
            {
                // Prova a raccogliere un oggetto
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxPickupDistance, interactableLayer))
                {
                    Debug.Log("Raycast hit: " + hit.transform.name);
                    pickedObject = hit.transform.gameObject;

                    // Memorizza la posizione e la rotazione originale
                    originalPosition = pickedObject.transform.position;
                    originalRotation = pickedObject.transform.rotation;

                    // Disabilita il MeshCollider per evitare problemi di fisica
                    MeshCollider meshCollider = pickedObject.GetComponent<MeshCollider>();
                    if (meshCollider != null)
                    {
                        meshCollider.enabled = false;
                    }

                    // Disabilita il Rigidbody per evitare che cada mentre è tenuto
                    Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }

                    // Inizia la visualizzazione dell'oggetto
                    StartCoroutine(EnterView());

                    Debug.Log("Picked up: " + pickedObject.name);
                }
                else
                {
                    Debug.Log("Raycast did not hit any object");
                }
            }
            else if (isViewing)
            {
                // Esci dalla visualizzazione e metti l'oggetto in mano
                StartCoroutine(ExitView());
                isHolding = true;
            }
            else if (isHolding)
            {
                // Trova la posizione in cui stai guardando
                RaycastHit hit;
                Vector3 dropPosition = pickedObject.transform.position;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDropDistance))
                {
                    dropPosition = hit.point;
                }

                // Verifica se la distanza di rilascio è entro il limite
                if (Vector3.Distance(transform.position, dropPosition) <= maxDropDistance)
                {
                    // Rilascia l'oggetto
                    pickedObject.transform.parent = null;

                    // Inizia la coroutine per animare l'oggetto verso la posizione di rilascio
                    StartCoroutine(DropObject(pickedObject, dropPosition, originalRotation));

                    Debug.Log("Dropped: " + pickedObject.name);
                    pickedObject = null;
                    isHolding = false;
                }
                else
                {
                    Debug.Log("Drop position is too far away");
                }
            }
        }

        if (isViewing && pickedObject != null)
        {
            RotateObjectWithMouse();
        }
    }

    private IEnumerator PickupObject(GameObject obj, Vector3 targetPosition, Quaternion targetRotation)
    {
        // Anima l'oggetto verso la posizione di raccolta
        while (Vector3.Distance(obj.transform.position, targetPosition) > 0.1f)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetPosition, animationSpeed * Time.deltaTime);
            obj.transform.rotation = Quaternion.RotateTowards(obj.transform.rotation, targetRotation, animationSpeed * Time.deltaTime * 100);
            yield return null;
        }

        // Imposta la posizione finale e il parent
        obj.transform.position = targetPosition;
        obj.transform.rotation = targetRotation;
        obj.transform.parent = holdPosition;
    }

    private IEnumerator DropObject(GameObject obj, Vector3 targetPosition, Quaternion targetRotation)
    {
        // Riabilita il MeshCollider
        MeshCollider meshCollider = obj.GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            meshCollider.enabled = true;
            meshCollider.convex = true; // Rendi il MeshCollider convesso
        }

        // Aggiungi un Rigidbody per far cadere l'oggetto
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true; // Rendi il Rigidbody cinematico per l'animazione

        // Anima l'oggetto verso la posizione di rilascio
        while (Vector3.Distance(obj.transform.position, targetPosition) > 0.1f || Quaternion.Angle(obj.transform.rotation, targetRotation) > 1.0f)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetPosition, animationSpeed * Time.deltaTime);
            obj.transform.rotation = Quaternion.RotateTowards(obj.transform.rotation, targetRotation, animationSpeed * Time.deltaTime * 100);
            yield return null;
        }

        // Disabilita il Rigidbody per far cadere l'oggetto
        rb.isKinematic = false;
    }

    private IEnumerator EnterView()
    {
        if (isViewing || pickedObject == null) yield break;

        isViewing = true;
        pickedObject.GetComponent<Collider>().enabled = false;

        Debug.Log("Entrato in modalità visualizzazione.");

        // Disabilita il movimento del giocatore
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Salva la posizione e la rotazione originali dell'oggetto
        originalPosition = pickedObject.transform.position;
        originalRotation = pickedObject.transform.rotation;

        // Calcola la posizione target
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * 0.6f; // Posiziona l'oggetto molto vicino alla camera

        // Calcola la rotazione target per far guardare l'oggetto verso la camera
        Quaternion targetRotation = Quaternion.LookRotation(Camera.main.transform.position - pickedObject.transform.position);

        // Aggiungi un offset di rotazione in base all'orientamento dell'oggetto
        if (isFlatObject)
        {
            // L'oggetto è coricato
            targetRotation *= Quaternion.Euler(90, 0, 0);
        }
        else
        {
            // L'oggetto è in piedi
            targetRotation *= Quaternion.Euler(0, 180, 0);
        }

        // Transizione fluida verso la posizione e la rotazione target
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            pickedObject.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / 1f);
            pickedObject.transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assicurati che l'oggetto sia esattamente nella posizione e rotazione target
        pickedObject.transform.position = targetPosition;
        pickedObject.transform.rotation = targetRotation;

        Debug.Log("Oggetto posizionato davanti al giocatore.");
    }

    private IEnumerator ExitView()
    {
        if (!isViewing || pickedObject == null) yield break;

        isViewing = false;
        Debug.Log("Uscito dalla modalità visualizzazione.");

        // Riabilita il movimento del giocatore
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // Transizione fluida verso la posizione e la rotazione originali
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            if (pickedObject != null)
            {
                pickedObject.transform.position = Vector3.Lerp(pickedObject.transform.position, holdPosition.position, elapsedTime / 1f);
                pickedObject.transform.rotation = Quaternion.Lerp(pickedObject.transform.rotation, holdPosition.rotation, elapsedTime / 1f);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(pickedObject);
        FirstSceneManager.instance.doorOpenable = true;
        // Assicurati che l'oggetto sia esattamente nella posizione e rotazione finali
        if (pickedObject != null)
        {
            pickedObject.transform.position = holdPosition.position;
            pickedObject.transform.rotation = holdPosition.rotation;
            pickedObject.transform.parent = holdPosition;
        }

        Debug.Log("Oggetto posizionato in mano.");
    }

    private void RotateObjectWithMouse()
    {
        float rotationSpeed = 100f;
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        if (isFlatObject)
        {
            // Ruota l'oggetto coricato sull'asse Z
            pickedObject.transform.Rotate(Vector3.forward, mouseX, Space.Self);
        }
        else
        {
            // Ruota l'oggetto in piedi sull'asse Y
            pickedObject.transform.Rotate(Vector3.up, mouseX, Space.Self);
        }
    }
}