using System.Collections;
using UnityEngine;

public class KnifePickUpandPlace : MonoBehaviour
{
    private GameObject pickedObject = null;
    public Transform holdPosition;
    public LayerMask interactableLayer;
    private float animationSpeed = 5.0f;
    private float maxPickupDistance = 3.0f;
    private float maxDropDistance = 3.0f;

    // Cooldown variables
    public float interactionCooldown = 3f;
    private float currentCooldown;

    // Audio sources and clips
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioClip knifePickupClip;
    public AudioClip secondAudioClip;

    // Audio playback state
    public bool isKnifePickupAudioPlaying = false;

    private enum InteractionState { Idle, Interact, StopInteract }
    private InteractionState currentState = InteractionState.Idle;

    // Delays
    public float secondAudioDelay = 2f;
    public float guardAudioDelay = 5f;

    // Player trigger area state
    private bool isPlayerInTriggerArea = false;
    FirstPersonController player;

    void Start()
    {
        // Ensure audio sources are assigned
        if (audioSource1 == null)
        {
            audioSource1 = GetComponents<AudioSource>()[0];
        }
        if (audioSource2 == null)
        {
            audioSource2 = GetComponents<AudioSource>()[1];
        }
        player = FindObjectOfType<FirstPersonController>();
    }

    void Update()
    {
        switch (currentState)
        {
            case InteractionState.Idle:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (pickedObject == null)
                    {
                        TryPickUpObject();
                    }
                    else
                    {
                        TryDropObject();
                    }
                }
                break;

            case InteractionState.Interact:
                currentCooldown -= Time.deltaTime;
                if (currentCooldown <= 0 && Input.GetKeyDown(KeyCode.F))
                {
                    ChangeState(InteractionState.StopInteract);
                }
                break;

            case InteractionState.StopInteract:
                ChangeState(InteractionState.Idle);
                break;
        }
    }

    void ChangeState(InteractionState newState)
    {
        if (newState == InteractionState.Interact)
        {
            currentCooldown = interactionCooldown;
        }
        currentState = newState;
    }

    void TryPickUpObject()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, maxPickupDistance, interactableLayer))
        {
            pickedObject = hit.transform.gameObject;

            if (pickedObject.TryGetComponent(out Collider collider))
            {
                collider.enabled = false;
            }

            if (pickedObject.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }

            PlayKnifePickupAudio();
            StartCoroutine(PlaySecondAudioWithDelay(secondAudioDelay + guardAudioDelay));
            StartCoroutine(PickupObject(pickedObject, holdPosition.position));
            ChangeState(InteractionState.Interact);
        }
    }

    void TryDropObject()
    {
        if (!isPlayerInTriggerArea)
        {
            Debug.Log("Il player non è nell'area del trigger, non è possibile posare il coltello.");
            return;
        }

        Vector3 dropPosition = pickedObject.transform.position;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, maxDropDistance))
        {
            dropPosition = hit.point;
        }

        if (Vector3.Distance(transform.position, dropPosition) <= maxDropDistance)
        {
            pickedObject.transform.parent = null;
            StartCoroutine(DropObject(pickedObject, dropPosition, hit));
            pickedObject = null;
            ChangeState(InteractionState.Interact);
        }
    }

    private IEnumerator PlaySecondAudioWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySecondAudio();
    }

    private void PlayKnifePickupAudio()
    {
        if (audioSource1 != null && knifePickupClip != null && !isKnifePickupAudioPlaying)
        {
            audioSource1.clip = knifePickupClip;
            audioSource1.Play();
            isKnifePickupAudioPlaying = true;
        }
    }

    private void PlaySecondAudio()
    {
        if (audioSource2 != null && secondAudioClip != null)
        {
            audioSource2.clip = secondAudioClip;
            audioSource2.Play();
        }
    }

    private IEnumerator PickupObject(GameObject obj, Vector3 targetPosition)
    {
        while (Vector3.Distance(obj.transform.position, targetPosition) > 0.1f)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, animationSpeed * Time.deltaTime);
            yield return null;
        }

        obj.transform.position = targetPosition;
        obj.transform.parent = holdPosition;

        CellaManager.instance.coltelloPreso = true;
        CellaManager.instance.attivaGuardRoutine = true;

        obj.tag = "Untagged";
        ChangeTagOfChildren(obj, "Untagged");

        yield return new WaitForSeconds(5f);
        audioSource1.Stop();
        audioSource2.Stop();
        isKnifePickupAudioPlaying = false;

       
        player.playerCanMove = true;
    }

    private IEnumerator DropObject(GameObject obj, Vector3 targetPosition, RaycastHit hit)
    {
        if (obj == null)
        {
            Debug.LogError("L'oggetto da rilasciare è null!");
            yield break;
        }

        if (obj.TryGetComponent(out MeshCollider meshCollider))
        {
            meshCollider.enabled = true;
            meshCollider.convex = true;
        }
        else
        {
            Debug.LogWarning("MeshCollider non trovato su " + obj.name);
        }

        if (!obj.TryGetComponent(out Rigidbody rb))
        {
            Debug.LogWarning("Rigidbody non trovato su " + obj.name + ", aggiungo un nuovo Rigidbody.");
            rb = obj.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true;

        while (Vector3.Distance(obj.transform.position, targetPosition) > 0.1f)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, animationSpeed * Time.deltaTime);
            yield return null;
        }

        rb.isKinematic = false;

        if (hit.transform != null && hit.transform.CompareTag("Drawer"))
        {
            obj.transform.parent = hit.transform;
        }

        CellaManager.instance.coltelloNascosto = true;
        player.playerCanMove = false;
        DisactivateKnifeTag();
    }

    private void ChangeTagOfChildren(GameObject parent, string newTag)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.tag = newTag;
            ChangeTagOfChildren(child.gameObject, newTag);
        }
    }

    public void SetPlayerInTriggerArea(bool isInTriggerArea)
    {
        isPlayerInTriggerArea = isInTriggerArea;
    }

    public void ActivateKnifeTag()
    {
        gameObject.tag = "OggettoInteragibile2";
    }

    public void ActivateKnifeTagTrigger()
    {
        gameObject.tag = "PosaColtello";
    }

    public void DisactivateKnifeTag()
    {
        gameObject.tag = "Untagged";
    }

   
}
