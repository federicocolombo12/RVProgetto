using System.Collections;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 2f;
    public float moveTime = 3f; // Tempo di camminata avanti e indietro
    private bool isMoving = false;
    public float standingTimer = 2f; // Tempo di attesa in Idle
    AudioSource audioSource;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Recupera l'Animator se non assegnato
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void TriggerMovement()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveNPC());
        }
    }

    private IEnumerator MoveNPC()
    {
        isMoving = true;

        // Cammina avanti
        animator.SetTrigger("TriggerWalk");
        audioSource.Play();
        yield return Move(Vector3.forward);

        // Idle
        animator.SetTrigger("TriggerIdle");
        audioSource.Stop();
        yield return new WaitForSeconds(standingTimer);

        // Cammina indietro
        animator.SetTrigger("TriggerBackward");
        audioSource.Play();
        yield return Move(Vector3.back);

        // Torna in Idle alla fine
        animator.SetTrigger("TriggerIdle");
        audioSource.Stop();
        isMoving = false;
        Destroy(gameObject);
    }

    private IEnumerator Move(Vector3 direction)
    {
        float timer = 0f;
        while (timer < moveTime)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
