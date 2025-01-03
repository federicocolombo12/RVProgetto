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
        animator = GetComponent<Animator>();
        MoveToTarget(target1);
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!reachedTarget1)
            {
                StartCoroutine(WaitForPickObjectAnimation());
            }
            else if (reachedTarget1 && agent.destination != target2.position)
            {
                MoveToTarget(target2);
            }
        }
    }

    void MoveToTarget(Transform target)
    {
        agent.destination = target.position;
    }

    IEnumerator WaitForPickObjectAnimation()
    {
        // Imposta il flag per indicare che il target1 è stato raggiunto
        reachedTarget1 = true;

        // Aspetta che l'animazione "Pick Object" inizi
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Pick Object"))
        {
            yield return null;
        }

        // Aspetta che l'animazione "Pick Object" termini
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Pick Object"))
        {
            yield return null;
        }

        // Muovi verso il target2
        MoveToTarget(target2);
    }
}
