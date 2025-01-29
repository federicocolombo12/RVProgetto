using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class InfermieraBlocking : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent navMeshAgent;
    [SerializeField] Animator animator;
    [SerializeField] Transform firstDestination;
    [SerializeField] DoorOpenerStandard doorOpenerStandard;
    [SerializeField] InteractWithNurse1 interactWithNurse1;
    [SerializeField] NpcHeadLookAt npcHeadLookAt;
    [SerializeField] private InfermieraBlockingAudio infermieraAudio; // Riferimento allo script audio

    bool coroutineStarted=false;
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        npcHeadLookAt = GetComponent<NpcHeadLookAt>();

    }

    // Update is called once per frame
    void Update()
    {
        if (InfermieriaManager.instance.pastigliaTrovata&&!coroutineStarted)
        {
           StartCoroutine(ReachDestination());
            coroutineStarted=true;
            
        }
        else {
            NurseLook();
        }
        
        
    }
    public void NurseTalk()
    {
        animator.SetTrigger("Blocked");

        // Avvia il suono se lo script audio è presente
        if (infermieraAudio != null)
        {
            infermieraAudio.PlayNurseTalk();
        }

    }

    public void EndInteraction()
    {
        if (infermieraAudio != null)
        {
            infermieraAudio.StopNurseTalk();
        }
    }

    IEnumerator ReachDestination()
    {
        interactWithNurse1.enabled = false;
        npcHeadLookAt.enabled = false;
        doorOpenerStandard.enabled = true;
        
        navMeshAgent.SetDestination(firstDestination.position);
        navMeshAgent.isStopped = false;
        
        animator.SetTrigger("Walk");
        yield return new WaitUntil(()=>!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f);
        animator.SetTrigger("Stop");
        
    }
    void NurseLook()
    {
        npcHeadLookAt.LookAtPosition(Camera.main.transform.position);
    }
}
