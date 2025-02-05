using System.Collections;
using UnityEngine;

public class DoorOpenerStandard : MonoBehaviour
{
    public Animator doorAnimator;
    public PortaStandardAudio portaAudio; // Riferimento allo script audio
    private Transform player;
    public Camera mainCamera;

    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private LayerMask interactableLayer;

    private void Start()
    {
        player = mainCamera.transform;
    }

    void Update()
    {
        if (InfermieriaManager.instance.pastigliaTrovata)
        {
            DoorActivate();
        }
    }

    private IEnumerator WaitForAnimationStart()
    {
        doorAnimator.SetBool("DoorOpen", true);
        portaAudio.PlayAperturaSound(); // Riproduce il suono
        yield return new WaitUntil(() => doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Apertura"));

        if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Apertura"))
        {
            InfermieriaManager.instance.isInRow = true;
        }
    }

    private void DoorActivate()
    {
        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        Debug.DrawRay(player.position, player.forward * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(WaitForAnimationStart());
            }
        }
    }
}
