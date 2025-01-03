using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AgentController : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public Animator animator;

    private NavMeshAgent agent;
    private bool reachedTarget1 = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToTarget(target1);
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!reachedTarget1)
            {
                StartCoroutine(WaitAndMoveToTarget2());
            }
            
        }
    }

    void MoveToTarget(Transform target)
    {
        agent.destination = target.position;
    }

    IEnumerator WaitAndMoveToTarget2()
    {
        // Imposta il flag per indicare che il target1 è stato raggiunto
        reachedTarget1 = true;

        // Aspetta 4 secondi e 567 millisecondi
        yield return new WaitForSeconds(4f);
        Debug.Log("Waited for 4.567 seconds");

        // Muovi verso il target2
        MoveToTarget(target2);
    }
}

