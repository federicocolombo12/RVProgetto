using System.Collections;
using UnityEngine;

public class AnimationTimer : MonoBehaviour
{
    [SerializeField] private Paziente2ScriptNPC paziente2;
    [SerializeField] private Paziente3ScriptNPC paziente3;
    [SerializeField] private Animator doorAnimator;

    private void OnEnable()
    {
        CountdownTimer.OnTimerEnd += HandleTimerEnd;
    }

    private void OnDisable()
    {
        CountdownTimer.OnTimerEnd -= HandleTimerEnd;
    }

    private void HandleTimerEnd()
    {
        // Imposta i flag su true
        paziente2.SetShouldMoveToPosition(true);
        paziente3.SetShouldMoveToPosition(true);

        // Avvia l'animazione delle porte
        doorAnimator.SetTrigger("OpenDoor");
    }
}
